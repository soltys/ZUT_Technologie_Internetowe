using PSBlog.Common;
using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace PSBlog.Repository
{
    internal class BlogRepository : UrlSlugRepository<Blog>, IBlogRepository
    {
        public BlogRepository()
        {
        }

        public void AddToUser(Blog entity,User user)
        {
             using (PSBlogContext db = new PSBlogContext())
            {
                
                entity.UrlSlug = GenerateUniqueSlug<Blog>(entity.UrlSlug);
                entity.Owner = db.Users.Attach(user);
                db.Blogs.Add(entity);
                db.SaveChanges();
            }
            
        }

        public Blog GetBlogBySlugUrl(string slugUrl)
        {
            using (PSBlogContext db = new PSBlogContext())
            {                
                return db.Blogs
                    .Include(x => x.Posts)
                    .Include(x => x.Posts.Select(p => p.Tags))
                    .Include(x => x.Owner)
                    .Single(b => b.UrlSlug == slugUrl);
            }
        }

        public void AddPost(int blogId, Post post)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                Blog blog = db.Blogs.First(b => b.Id == blogId);
                blog.Posts.Add(post);
                int rowsChanged = db.SaveChanges();
                if (rowsChanged == 0)
                {
                    //no rows are changed
                    System.Diagnostics.Debugger.Break();
                }
            }
        }

        public override void Remove(int id)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                Blog blog = db.Blogs.First(b => b.Id == id);
                blog.Posts.RemoveAll(p => true);
                db.Entry(blog).State = EntityState.Deleted;
                db.SaveChanges();
            }
            
        }
    }
}