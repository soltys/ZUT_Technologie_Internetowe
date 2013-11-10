using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSBlog.Models
{
    public class Blog : IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public virtual User Owner { get; set; }
        public virtual IList<Post> Posts { get; set; }
    }
}