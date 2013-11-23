using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSBlog.Models
{
    public class User : IIdentifiable
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }       

        private List<Role> _roles;
        public virtual List<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        } 
    }
}