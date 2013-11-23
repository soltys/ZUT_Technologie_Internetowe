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



    }
}