using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class ViewModelPostComments
    {
        public Post Post { get; set; }
        public Comment NewComment
        { get; set; }
    }
}