using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSBlog.ViewModels
{
    public class CreateCommentModel
    {
        public string Content { get; set; }
        public string UserName { get; set; }
        public string ReturnBlogSlug { get; set; }
        public string ReturnPostSlug { get; set; }
    }
}