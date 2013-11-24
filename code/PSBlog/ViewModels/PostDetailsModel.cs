using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSBlog.ViewModels
{
    public class PostDetailsModel
    {
        public Blog Blog { get; set; }
        public Post Post { get; set; }
    }
}