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
        [Display(Name = "Posted date")]
        public DateTime DatePosted { get; set; }

        private List<Tag> _tags;
        public virtual List<Tag> Tags
        {
            get { return _tags ?? (_tags = new List<Tag>()); }
            set { _tags = value; }
        }

        private List<Comment> _comments;
        public virtual List<Comment> Comments
        {
            get { return _comments ?? (_comments = new List<Comment>()); }
            set { _comments = value; }
        } 
    }
}
