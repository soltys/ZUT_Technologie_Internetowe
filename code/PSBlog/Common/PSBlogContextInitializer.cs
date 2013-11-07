using PSBlog.Models;
using PSBlog.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PSBlog.Common
{
    public class PSBlogContextInitializer:DropCreateDatabaseAlways<PSBlogContext>
    {
        protected override async void Seed(PSBlogContext context)
        {
            Role administrator = new Role();
            administrator.Name = "Administrator";
            administrator.PermissionLevel = 1000;

            context.Roles.Add(administrator);


            User admin = new User
            {
                UserName = "admin",
                Password = SHA.CreateSHA1Hash("milk"),
                Role = administrator               
            };

            context.Users.Add(admin);

            context.SaveChanges();            
        }
    }
}