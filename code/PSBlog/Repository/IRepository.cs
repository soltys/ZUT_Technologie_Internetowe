using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSBlog.Repository
{
    public interface IRepository<T> where T : IIdentifiable
    {
        IList<T> FetchAll();
        void Add(T entity);
        void Save();
    }
}
