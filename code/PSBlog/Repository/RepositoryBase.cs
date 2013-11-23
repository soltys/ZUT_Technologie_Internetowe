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

        protected PSBlogContext _db;
        private bool _disposed;
        private readonly ILogger _log;
        protected RepositoryBase(PSBlogContext db)
        {
            _db = db;
            var logfac = DependencyResolver.Current.GetService<ILoggerFactory>();
            _log = logfac.GetCurrentClassLogger();
        }

        public virtual T FindById(int id)
        {
            return _db.Set<T>().First(el => el.Id == id);
        }

        public virtual IList<T> FetchAll()
        {
            return _db.Set<T>().ToList();
        }

        public virtual void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
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
                    if (_db != null)
                    {
                        _db.Dispose();
                        _log.Info("Repository Disposed " + ToString());
                    }
                }

                _db = null;
                // Indicate that the instance has been disposed.
                _disposed = true;
            }
        }
    }
}