using PSBlog.Models;
using PSBlog.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PSBlog.Common
{
    public class PSBlogContextInitializer : DropCreateDatabaseAlways<PSBlogContext>
    {
        protected override void Seed(PSBlogContext context)
        {
            Role administrator = new Role()
            {
                Name = "admin",                
            };
            context.Roles.Add(administrator);

            User admin = new User
            {
                UserName = "admin",
                Password = SHA.CreateSHA1Hash("milk"),
                Roles = new[] { administrator }
            };
            context.Users.Add(admin);

            Tag tag = new Tag()
            {
                Name = "mytag"
            };
            context.Tags.Add(tag);


            Post post = new Post
            {
                Title = "Hello, World",
                Content = "<p>This is my new blog!</p>",
                DatePosted = DateTime.Now,
                Tags = new[] { tag }
            };
            post.UrlSlug = Slug.GenerateSlug(post.Title);
            context.Posts.Add(post);

            Blog defaultBlog = new Blog
            {
                Title = "I <3 Blogs",
                Owner = admin,
                Posts = new[]{post}
            };
            defaultBlog.UrlSlug = Slug.GenerateSlug(defaultBlog.Title);

            context.Blogs.Add(defaultBlog);

            context.SaveChanges();
        }
    }
}