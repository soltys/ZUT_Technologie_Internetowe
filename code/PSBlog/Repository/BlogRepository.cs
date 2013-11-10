using PSBlog.Common;
using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSBlog.Repository
{
    internal class BlogRepository:RepositoryBase<Blog>,IBlogRepository
    {
        public BlogRepository(PSBlogContext context):base(context)
        {
        }
    }
}