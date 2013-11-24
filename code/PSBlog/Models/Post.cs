using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PSBlog.Models
{
    public class Post : IIdentifiable, IUrlSlug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string  Content { get; set; }
        public string UrlSlug { get; set; }
        public DateTime DatePosted { get; set; }

        public virtual IList<Tag> Tags { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}
