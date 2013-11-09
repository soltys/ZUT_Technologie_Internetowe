using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSBlog.Models;
namespace PSBlog.ViewModels
{
    public class RegisterModel
    {
        public User User { get; set; }
        public string PasswordAgain { get; set; }
    }
}