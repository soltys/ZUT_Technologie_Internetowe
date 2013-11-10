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
                Name = "Administrator",
                PermissionLevel = 1000
            };
            context.Roles.Add(administrator);

            User admin = new User
            {
                UserName = "admin",
                Password = SHA.CreateSHA1Hash("milk"),
                Role = administrator
            };
            context.Users.Add(admin);

            Tag tag = new Tag()
            {
                Name = "mytag"
            };
            context.Tags.Add(tag);


            Post post = new Post
            {
                Title = "Default post title",
                Content = "<p>asd</p>",
                DatePosted = DateTime.Now,
                Tags = new[] { tag }
            };
            context.Posts.Add(post);

            Blog defaultBlog = new Blog
            {
                Title = "Default blog Title",
                Owner = admin,
                Posts = new[]{post}
            };
            defaultBlog.UrlSlug = Slug.GenerateSlug(defaultBlog.Title);

            context.Blogs.Add(defaultBlog);

            context.SaveChanges();
        }
    }
}