﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSBlog.Models
{
    public class Comment : IIdentifiable
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User Creator { get; set; }
    }
}
