using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSBlog.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUserName(string username);
        bool IsUserNameTaken(string userName);
        bool IsUserHaveBlog(string userName);
        Blog GetUserBlog(string userName);

        IEnumerable<string> GetRolesForUser(string username);
    }
}
