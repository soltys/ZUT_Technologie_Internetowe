using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSBlog.Repository
{
    public interface IRepository<T> : IDisposable where T : IIdentifiable
    {
        IList<T> FetchAll();
        T FindById(int id);
        void Add(T entity);
        void Edit(T entity);
        void Remove(int id);
        void Save();
    }
}
