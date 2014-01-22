using PSBlog.Common;
using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PSBlog.ViewModels;
namespace PSBlog.Repository
{
    internal class PostRepository : UrlSlugRepository<Post>, IPostRepository
    {
        public PostRepository()
        {
        }
        public override void Remove(int id)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                Post entity = db.Posts.Include(p => p.Tags)
                    .Include(p => p.Comments).First(el => el.Id == id);
                entity.Tags.RemoveAll(p => true);
                entity.Comments.RemoveAll(c => true);
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public override void Add(Post entity)
        {
            entity.DatePosted = DateTime.Now;
            base.Add(entity);
        }

        public List<Post> GetAllPosts(int blogId)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Blogs.Include(b => b.Posts).First(b => b.Id == blogId).Posts.ToList();
            }
        }

        public PostDetailsModel GetPostDetails(string blogSlug, string postSlug)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                Blog selectedBlog = db.Blogs
                    .Include(b => b.Posts)
                    .Include(b => b.Posts.Select(p => p.Comments))
                    .Include(b => b.Posts.Select(p => p.Comments.Select(c => c.User)))
                    .First(b => b.UrlSlug == blogSlug);
                Post selectedPost = selectedBlog.Posts.First(p => p.UrlSlug == postSlug);
                return new PostDetailsModel { Blog = selectedBlog, Post = selectedPost };
            }
        }

        public void AddComment(string commentContent, string commentUserName, string blogSlug, string postSlug)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                var selectedPost = db.Blogs
                    .Include(b => b.Posts)
                    .First(b => b.UrlSlug == blogSlug)
                    .Posts
                    .First(p => p.UrlSlug == postSlug);
                var selectedUser = db.Users.First(u => u.UserName == commentUserName);
                selectedPost.Comments.Add(new Comment { Content = commentContent, User = selectedUser });
                db.SaveChanges();
            }
        }

        public override void Edit(Post entity)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                Post post = db.Posts.First(p => p.Id == entity.Id);
                post.Title = entity.Title;
                post.Content = entity.Content;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
