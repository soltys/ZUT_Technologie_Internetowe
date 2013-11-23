using System.Data.Entity;
using PSBlog.Common;
using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSBlog.Properties;
using System.Web.Http;

namespace PSBlog.Repository
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PSBlogContext context)
            : base(context)
        {

        }

        public override IList<User> FetchAll()
        {
            return _db.Users.Include(u => u.Roles).ToList();
        }

        public User FindByUserName(string username)
        {
            return _db.Users.Include(u => u.Roles).FirstOrDefault(user => user.UserName == username);
        }

        public override User FindById(int id)
        {
            return _db.Users.Include(u => u.Roles).First(user => user.Id == id);
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

        public IEnumerable<string> GetRolesForUser(string username)
        {
            var selectedUser = _db.Users.Include(u => u.Roles).FirstOrDefault(user => user.UserName == username);
            _db.Entry(selectedUser).Reload();
            var roles = selectedUser.Roles.Select(role => role.Name);

            var onemoretime = FindById(selectedUser.Id);
            return roles;
        }

        
        public void GrantAdminRole(User user)
        {
            var adminRole = _db.Roles.First(role => role.Name == Settings.Default.SuperAdminRole);
            if (!user.Roles.Any(role => role.Name == Settings.Default.SuperAdminRole))
            {
                user.Roles.Add(new Role { Name = Settings.Default.SuperAdminRole });
            }
            int count = _db.SaveChanges();

        }

        public void TakeAwayAdminRole(User user)
        {
            var adminRole = user.Roles.First(r => r.Name == Settings.Default.SuperAdminRole);
            bool status = user.Roles.Remove(adminRole);
            _db.Roles.Remove(adminRole);
            //_db.Entry(user).State = EntityState.Modified;
            int count = _db.SaveChanges();
            //var adminRole = user.Roles.First(role => role.Name == Settings.Default.SuperAdminRole); 

            //_db.Entry(adminRole).State = EntityState.Deleted;
            //count = _db.SaveChanges();

        }
    }
}