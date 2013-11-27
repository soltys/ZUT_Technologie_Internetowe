using PSBlog.Models;
using PSBlog.Properties;
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
                Name = Settings.Default.SuperAdminRole,
            };
            context.Roles.Add(administrator);

            User admin = new User
            {
                UserName = Settings.Default.SuperAdminName,
                Password = SHA.CreateSHA1Hash("milk"),
                Roles = new List<Role>() { administrator }
            };
            context.Users.Add(admin);

            User normalUser = new User
            {
                UserName = "normal",
                Password = SHA.CreateSHA1Hash("milk"),
            };
            context.Users.Add(normalUser);

            Tag tag = new Tag()
            {
                Name = "mytag"
            };
            context.Tags.Add(tag);

            Comment comment1 = new Comment()
            {
                Content = "Comment#1",
                User = admin
            };

            Comment comment2 = new Comment()
            {
                Content = "Comment#2",
                User = normalUser
            };

            context.Comments.Add(comment1);
            context.Comments.Add(comment2);

            string paragraph1 = string.Format("<p>{0}</p>", Ipsum.GetPhrase(50));
            string paragraph2 = string.Format("<p>{0}</p>", Ipsum.GetPhrase(75));
            Post post = new Post
            {
                Title = "Hello, World",
                Content = paragraph1 + paragraph2,
                DatePosted = DateTime.Now,
                Tags = new List<Tag> { tag },
                Comments = new List<Comment> { comment1, comment2 }
            };
            post.UrlSlug = Slug.GenerateSlug(post.Title);
            context.Posts.Add(post);

            Blog defaultBlog = new Blog
            {
                Title = "I <3 Blogs",
                Owner = admin,
                Posts = new List<Post> { post }
            };
            defaultBlog.UrlSlug = Slug.GenerateSlug(defaultBlog.Title);

            context.Blogs.Add(defaultBlog);

            context.SaveChanges();
        }
    }
}