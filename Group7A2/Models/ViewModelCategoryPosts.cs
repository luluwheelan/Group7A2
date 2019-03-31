using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class ViewModelCategoryPosts
    {
        public Category category { get; set; }
        public List<Post> posts
        { get; set; }
    }
}