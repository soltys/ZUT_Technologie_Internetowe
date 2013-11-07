using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }
        public virtual IList<Tag> Tags { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}
