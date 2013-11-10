using Messim.UI.Authentication;
using PSBlog.Authentication;
using PSBlog.Models;
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
            return View();
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

            FormsAuthentication.SetAuthCookie(registerModel.User.UserName,false);

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

        public ActionResult List()
        {
            return View(_userRepository.FetchAll());
     
        
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _userRepository.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}
