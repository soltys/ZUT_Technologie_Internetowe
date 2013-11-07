using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string  Title { get; set; }
        public virtual User Owner { get; set; }
        public virtual IList<Post> Posts { get; set; }
    }
}