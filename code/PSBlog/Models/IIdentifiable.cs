using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSBlog.Models
{
    public interface IIdentifiable
    {
        int Id { get; set; }
    }
}
