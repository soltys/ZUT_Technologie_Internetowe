using PSBlog.Common;
using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace PSBlog.Repository
{
    internal class PostRepository : UrlSlugRepository<Post>, IPostRepository
    {
        public PostRepository()
        {
        }

        public override void Add(Post entity)
        {
            entity.DatePosted = DateTime.Now;
            base.Add(entity);
        }

        public List<Post> GetAllPosts(int blogId)
        {
            using(PSBlogContext db =new PSBlogContext())
            {
                return db.Blogs.Include(b => b.Posts).First(b => b.Id == blogId).Posts.ToList();
            }
        }

        public Post GetPost(string blogSlug, string postSlug)
        {
            using(PSBlogContext db = new PSBlogContext())
            {
                Blog selectedBlog = db.Blogs.Include(b => b.Posts).First(b => b.UrlSlug == blogSlug);
                Post selectedPost = selectedBlog.Posts.First(p => p.UrlSlug == postSlug);
                return selectedPost;
            }
        }
    }
}
