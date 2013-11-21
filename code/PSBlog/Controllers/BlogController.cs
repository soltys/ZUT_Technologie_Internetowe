﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSBlog.Models;
using PSBlog.Common;
using Ninject.Extensions.Logging;
using PSBlog.Repository;
using PSBlog.Util;

namespace PSBlog.Controllers
{
    public class BlogController : Controller
    {

        private readonly ILogger _logger;
        private IBlogRepository _blogRepository;
        private IUserRepository _userRepository;
        public BlogController(ILogger logger, IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }
        // GET: /Blog/
        public ActionResult Index()
        {
            return View(_blogRepository.FetchAll());
        }

        public ActionResult List()
        {
            return View(_blogRepository.FetchAll());
        }


        // GET: /Blog/Create
        public ActionResult Create()
        {
            if (_userRepository.IsUserHaveBlog(User.Identity.Name))
            {
                return RedirectToAction("Details");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title")] Blog blog)
        {
           
            if (ModelState.IsValid)
            {
                blog.UrlSlug = Slug.GenerateSlug(blog.Title);
                blog.Owner = _userRepository.FindByUserName(User.Identity.Name);
                _blogRepository.Add(blog);
                _blogRepository.Save();

                return RedirectToAction("Details", new { blogSlug = blog.UrlSlug });
            }

            return View(blog);
        }



        public ActionResult Details(string blogSlug)
        {
            if (string.IsNullOrWhiteSpace(blogSlug))
            {
                if (User.Identity.IsAuthenticated && _userRepository.IsUserHaveBlog(User.Identity.Name))
                {
                    Blog userBlog = _userRepository.GetUserBlog(User.Identity.Name);
                    return View(userBlog);
                }
                return RedirectToAction("List");
            }
            Blog blog = _blogRepository.GetBlogBySlugUrl(blogSlug);
            return View(blog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _logger.Info("Disposing repositories");
                _blogRepository.Dispose();
                _userRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
