using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class PostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public Category Category { get; set; }
    }
}