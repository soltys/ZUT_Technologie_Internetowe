using PSBlog.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PSBlog.Repository
{
    internal abstract class RepositoryBase<T>  where T:class
    {
        protected PSBlogContext _db;
        public RepositoryBase(PSBlogContext db)
        {
            _db = db;
        }

        public IList<T> FetchAll()
        {
            return _db.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }      

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}