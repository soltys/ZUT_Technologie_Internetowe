using System.Data.Entity;
using PSBlog.Common;
using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSBlog.Properties;

namespace PSBlog.Repository
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PSBlogContext context)
            : base(context)
        {

        }

        public User FindByUserName(string username)
        {
            return _db.Users.FirstOrDefault(user => user.UserName == username);
        }

        public bool IsUserNameTaken(string username)
        {
            return _db.Users.Any() && _db.Users.ToList().Any(x => x.UserName == username);
        }

        public bool IsUserHaveBlog(string userName)
        {
            return _db.Blogs.Any(blog => blog.Owner.UserName == userName);
        }

        public Blog GetUserBlog(string userName)
        {
            return _db.Blogs.First(blog => blog.Owner.UserName == userName);
        }


        public void GrantAdminRole(User user)
        {
            var adminRole = _db.Roles.First(role => role.Name == Settings.Default.SuperAdminRole);
            user.Roles.Add(adminRole);
            _db.Entry(user).State = EntityState.Modified;
        }

        public void TakeAwayAdminRole(User user)
        {
            var adminRole = user.Roles.First(role => role.Name == Settings.Default.SuperAdminRole);
            user.Roles.Remove(adminRole);
            _db.Entry(user).State = EntityState.Modified;
        }
    }
}