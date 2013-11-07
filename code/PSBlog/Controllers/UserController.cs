using Messim.UI.Authentication;
using PSBlog.Authentication;
using PSBlog.Repository;
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

        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(_userRepository.FetchAll());
        }
    }
}
