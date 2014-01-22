using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Extensions;
using PSBlog.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSBlog.Models;


namespace PSBlog.Repository
{
    public abstract class RepositoryBase<T> where T : class,IIdentifiable
    {
        private bool _disposed;
        private readonly ILogger _log;
        protected RepositoryBase()
        {
            var logfac = DependencyResolver.Current.GetService<ILoggerFactory>();
            _log = logfac.GetCurrentClassLogger();
        }

        public virtual T FindById(int id)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Set<T>().First(el => el.Id == id);

            }

        }

        public virtual IList<T> FetchAll()
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Set<T>().ToList();
                
            }

        }

        public virtual void Add(T entity)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
            }

        }

        public virtual void Edit(T entity)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public virtual void Remove(int id)
        {
            using (PSBlogContext db = new PSBlogContext())
            {                
                T entity =  db.Set<T>().First(el => el.Id == id);
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }

        }

        public void Save()
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                db.SaveChanges();
            }

        }

        public void Dispose()
        {
            Dispose(true);

            // Call SupressFinalize in case a subclass implements a finalizer.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }


                _disposed = true;
            }
        }
    }
}