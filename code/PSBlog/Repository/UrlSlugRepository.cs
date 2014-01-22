using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using PSBlog.Models;
using PSBlog.Common;

namespace PSBlog.Repository
{
    abstract internal class UrlSlugRepository<T> : RepositoryBase<T> where T : class,IUrlSlug, IIdentifiable
    {
        protected UrlSlugRepository()   
        {

        }

        public override void Add(T entity)
        {            
            entity.UrlSlug = GenerateUniqueSlug<T>(entity.UrlSlug);
            base.Add(entity);
        }

        

        protected string GenerateUniqueSlug<T>(string urlSlug) where T : class ,IUrlSlug
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                bool unique = !db.Set<T>().Any(x => x.UrlSlug == urlSlug);
                if (!unique)
                {
                    return GenerateUniqueSlug<T>(urlSlug, 1);
                }
                return urlSlug;
            }
        }

        private string GenerateUniqueSlug<T>(string urlSlug, int numberTries) where T : class ,IUrlSlug
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                string newSlug = urlSlug + numberTries;
                bool unique = !db.Set<T>().Any(x => x.UrlSlug == newSlug);
                if (!unique)
                {
                    newSlug = GenerateUniqueSlug<T>(newSlug, ++numberTries);
                }
                return newSlug;
            }
        }
    }
}
