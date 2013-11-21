﻿using System;
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
        public virtual IList<Role> Roles { get; set; }
    }
}