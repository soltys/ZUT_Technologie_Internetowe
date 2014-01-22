using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSBlog.Models;
namespace PSBlog.ViewModels
{
    public class CreateOrEditPostModel
    {
        public Blog Blog { get; set; }
        public Post Post { get; set; }
    }
}