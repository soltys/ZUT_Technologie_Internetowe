using PSBlog.Authentication;
using PSBlog.Models;
using PSBlog.Properties;
using PSBlog.Repository;
using PSBlog.Util;
using PSBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PSBlogs.Controllers
{
    public class UserController : Controller
    {
        private readonly IPSBlogMembershipProvider _provider;
        private IUserRepository _userRepository;

        public UserController(IPSBlogMembershipProvider provider, IUserRepository userRepository)
        {
            _provider = provider;
            _userRepository = userRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            UserProfileModel model = new UserProfileModel();
            model.IsAdministrator = User.IsInRole("admin");
            model.IsUserHaveBlog = _userRepository.IsUserHaveBlog(User.Identity.Name);

            if (model.IsUserHaveBlog)
            {
                var userBlog = _userRepository.GetUserBlog(User.Identity.Name);
                model.UserBlogId = userBlog.Id;
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ValidateLogOn(model.User.UserName, model.User.Password))
            {
                return View();
            }

            FormsAuthentication.SetAuthCookie(model.User.UserName, createPersistentCookie: model.RememberMe);
            if (!String.IsNullOrEmpty(returnUrl) && returnUrl != "/")
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool ValidateLogOn(string username, string password)
        {
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("User.UserName", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("User.Password", "You must specify a password.");
            }
            if (!_provider.ValidateUser(username, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }
            return ModelState.IsValid;
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (!ValidateRegister(registerModel.User.UserName, registerModel.User.Password, registerModel.PasswordAgain))
            {
                return View();
            }

            registerModel.User.Password = SHA.CreateSHA1Hash(registerModel.User.Password);
            _userRepository.Add(registerModel.User);
            _userRepository.Save();

            FormsAuthentication.SetAuthCookie(registerModel.User.UserName, false);

            return RedirectToAction("Index", "Home");
        }

        private bool ValidateRegister(string username, string password, string passwordAgain)
        {
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "You must enter user name");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must enter password");
            }
            if (password != passwordAgain)
            {
                ModelState.AddModelError("_FORM", "Both password must match");
            }

            if (_userRepository.IsUserNameTaken(username))
            {
                ModelState.AddModelError("username", "This user already exists");
            }

            return ModelState.IsValid;
        }

        [Authorize(Roles = "admin")]
        public ActionResult List()
        {
            return View(_userRepository.FetchAll().ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult GrantAdminRole(int id)
        {
            User selectedUser = _userRepository.FindById(id);
            _userRepository.GrantAdminRole(selectedUser);
            _userRepository.Save();
            return RedirectToAction("List");
        }

        [Authorize(Roles = "admin")]
        public ActionResult TakeAwayAdminRole(int id)
        {
            User selectedUser = _userRepository.FindById(id);

            if (selectedUser.UserName != Settings.Default.SuperAdminName)
            {
                _userRepository.TakeAwayAdminRole(selectedUser);
                _userRepository.Save();
            }

            return RedirectToAction("List");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {

            User selectedUser = _userRepository.FindById(id);
            if (selectedUser.UserName != Settings.Default.SuperAdminName)
            {
                _userRepository.Remove(id);

            }
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
