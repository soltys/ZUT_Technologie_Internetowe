using PSBlog.Repository;
using PSBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSBlog.Controllers
{
    public class CommentController : Controller
    {
        IPostRepository _postRepository;
        public CommentController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPost]
        public ActionResult Create(CreateCommentModel model)
        {
            _postRepository.AddComment(
                model.Content, model.UserName,
                model.ReturnBlogSlug, model.ReturnPostSlug
                );

            return RedirectToAction("Details", "Post",
                new
                {
                    blogSlug = model.ReturnBlogSlug,
                    postSlug = model.ReturnPostSlug
                });
        }
    }
}