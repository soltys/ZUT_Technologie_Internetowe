using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSBlog.Models
{
    public class Role : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }        
    }
}
