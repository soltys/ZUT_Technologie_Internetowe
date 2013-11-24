using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSBlog.Models
{
    public class Blog : IIdentifiable, PSBlog.Models.IUrlSlug
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public virtual User Owner { get; set; }

        private List<Post> _posts;
        public virtual List<Post> Posts
        {
            get { return _posts ?? (_posts = new List<Post>()); }
            set { _posts = value; }
        }
    }
}