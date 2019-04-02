using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class PostCommentViewModel
    {
        public Post post { get; set; }
        public Comment newComment { get; set; }

    }
}