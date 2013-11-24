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
    }
}
