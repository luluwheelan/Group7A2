using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class Category
    {
        public virtual int CategoryId { get; set; }

        [Required]
        [DisplayName("Category")]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }
        //A category has many posts
        public virtual List<Post> Posts { get; set; }
    }
}