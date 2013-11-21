using Ninject;
using Ninject.Extensions.Logging;
using PSBlog.Models;
using PSBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSBlog.Controllers
{
    public class PostController : Controller
    {
        [Inject]
        public ILogger _logger { get; set; }

        private IBlogRepository _blogRepository;
        private IUserRepository _userRepository;
        public PostController(IBlogRepository blogRepository, IUserRepository userRepository)
        {

            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        //
        // GET: /Post/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Details", "Blog");
            }
            return RedirectToAction("List", "Blog");
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Post post)
        {
            return View("Details", "Blog");
        }


    }
}