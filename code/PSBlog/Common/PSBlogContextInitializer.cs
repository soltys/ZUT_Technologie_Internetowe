using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PSBlog.Common
{
    public class PSBlogContextInitializer:DropCreateDatabaseIfModelChanges<PSBlogContext>
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
            };

            await context.SaveChangesAsync();            
        }
    }
}