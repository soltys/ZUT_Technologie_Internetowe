using PSBlog.Common;
using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSBlog.Repository
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository 
    {
        public UserRepository(PSBlogContext context):base(context)
        {

        }
        public User FindByUserName(string username)
        {
            return _db.Users.FirstOrDefault(user => user.UserName == username);
        }        
    }
}