using Ninject;
using Ninject.Extensions.Logging;
using PSBlog.Models;
using PSBlog.Repository;
using PSBlog.Util;
using PSBlog.ViewModels;
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
        private IPostRepository _postRepository;
        public PostController(IBlogRepository blogRepository, IUserRepository userRepository, IPostRepository postRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
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
            if (User.Identity.IsAuthenticated)
            {
                Blog userBlog = _userRepository.GetUserBlog(User.Identity.Name);
                if (userBlog != null)
                {
                    return View(new CreateOrEditPostModel { Blog = userBlog, Post = new Post() });
                }
            }
            return RedirectToAction("List", "Blog");
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Create([ModelBinder(typeof(CreateOrEditPostCustomDataBinder))] CreateOrEditPostModel model)
        {
            model.Post.UrlSlug = Slug.GenerateSlug(model.Post.Title);

            _postRepository.Add(model.Post);
            _blogRepository.AddPost(model.Blog.Id, model.Post);
            return RedirectToAction("Details", "Blog");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {

            if (!_userRepository.IsUserHaveBlog(User.Identity.Name))
            {
                return RedirectToAction("Details", "Blog");
            }

            Blog selectedBlog = _userRepository.GetUserBlog(User.Identity.Name);
            if (selectedBlog.Posts.Any(p => p.Id == id))
            {
                _postRepository.Remove(id);
            }

            return RedirectToAction("Details", "Blog");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            if (!_userRepository.IsUserHaveBlog(User.Identity.Name))
            {
                return RedirectToAction("Details", "Blog");
            }

            Blog selectedBlog = _userRepository.GetUserBlog(User.Identity.Name);
            if (!selectedBlog.Posts.Any(p => p.Id == id))
            {
                return RedirectToAction("Details", "Blog");
            }

            return View(new CreateOrEditPostModel { Blog = selectedBlog, Post = selectedBlog.Posts.First(p => p.Id == id) });

        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit([ModelBinder(typeof(CreateOrEditPostCustomDataBinder))] CreateOrEditPostModel model)
        {
            _postRepository.Edit(model.Post);
            return RedirectToAction("Details", "Blog");
        }



        public ActionResult Details(string blogSlug, string postSlug)
        {
            PostDetailsModel model = _postRepository.GetPostDetails(blogSlug, postSlug);
            return View(model);
        }

    }
}