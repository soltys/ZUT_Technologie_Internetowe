using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PSBlog.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Text;
using System.Reflection;
using System.Data.Entity.Core.EntityClient;
using System.Globalization;
using System.Data.Common;
using Ninject;
using Ninject.Extensions.Logging;
using System.Data.SqlClient;
namespace PSBlog.Common
{
    public class PSBlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        [Inject]
        public ILogger Logger { get; set; }
        public PSBlogContext()
        {

        }


    }
}