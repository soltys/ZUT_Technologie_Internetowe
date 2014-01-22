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
        public UserRepository()
        {

        }

        public override IList<User> FetchAll()
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Users.Include(u => u.Roles).ToList();
            }
        }

        public User FindByUserName(string username)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Users.Include(u => u.Roles).FirstOrDefault(user => user.UserName == username);
            }
        }

        public override User FindById(int id)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Users.Include(u => u.Roles).First(user => user.Id == id);
            }
        }

        public override void Remove(int id)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                var userBlog = db.Blogs.Include(b => b.Owner).FirstOrDefault(blog => blog.Owner.Id == id);
                if (userBlog != null)
                {
                    db.Entry(userBlog).State = EntityState.Deleted;
                }

                var userComments = db.Comments.Where(c=>c.User.Id == id);
                foreach(var userComment in userComments)
                {
                    db.Entry(userComment).State = EntityState.Deleted;
                }

                var selectedUser = db.Users.Include(u => u.Roles).FirstOrDefault(user => user.Id == id);
                if(selectedUser != null)
                {
                    db.Entry(selectedUser).State = EntityState.Deleted;
                }
                
                db.SaveChanges();
            }

        }

        public bool IsUserNameTaken(string username)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Users.Any() && db.Users.ToList().Any(x => x.UserName == username);
            }
        }

        public bool IsUserHaveBlog(string userName)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Blogs.Any(blog => blog.Owner.UserName == userName);
            }
        }

        public Blog GetUserBlog(string userName)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                return db.Blogs
                    .Include(b => b.Posts)
                    .Include(b => b.Posts.Select(p => p.Tags))
                    .Include(b => b.Owner)
                    .FirstOrDefault(blog => blog.Owner.UserName == userName);
            }
        }

        public IEnumerable<string> GetRolesForUser(string username)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                var selectedUser = db.Users.Include(u => u.Roles).FirstOrDefault(user => user.UserName == username);
                if (selectedUser == null)
                {
                    return null;
                }
                var roles = selectedUser.Roles.Select(role => role.Name);

                var onemoretime = FindById(selectedUser.Id);
                return roles;
            }
        }


        public void GrantAdminRole(User user)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                var adminRole = new Role { Name = Settings.Default.SuperAdminRole };
                db.Roles.Add(adminRole);
                if (!user.Roles.Any(role => role.Name == Settings.Default.SuperAdminRole))
                {
                    user.Roles.Add(adminRole);
                }

                db.Entry(user).State = EntityState.Modified;
                int count = db.SaveChanges();
            }
        }

        public void TakeAwayAdminRole(User user)
        {
            using (PSBlogContext db = new PSBlogContext())
            {
                var adminRole = user.Roles.First(r => r.Name == Settings.Default.SuperAdminRole);
                bool status = user.Roles.Remove(adminRole);
                //db.Roles.Remove(adminRole);
                //_db.Entry(user).State = EntityState.Modified;
                int count = db.SaveChanges();
                //var adminRole = user.Roles.First(role => role.Name == Settings.Default.SuperAdminRole); 

                db.Entry(adminRole).State = EntityState.Deleted;
                count = db.SaveChanges();

            }
        }
    }
}