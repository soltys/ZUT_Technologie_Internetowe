using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSBlog.Models;
namespace PSBlog.ViewModels
{
    public class LoginModel
    {
        public User User { get; set; }
        public bool RememberMe { get; set; }
    }
}