using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSBlog.ViewModels
{
    public class UserProfileModel
    {
        public bool IsAdministrator { get; set; }
        public bool IsUserHaveBlog { get; set; }
        public int UserBlogId { get; set; }
    }
}