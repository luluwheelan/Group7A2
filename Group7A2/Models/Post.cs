using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class Post
    {
        private DateTime _date = DateTime.Now;

        public virtual int PostId { get; set; }
        [Required(ErrorMessage = "What is your topic?")]
        [StringLength(100, MinimumLength = 2)]
        public virtual String Subject { get; set; }

        [Required(ErrorMessage = "Give us more details about your topic.")]
        [StringLength(500, MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        public virtual String Content { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual String Author { get; set; }

        //Set PostTime dafault value.
        public DateTime PostTime
        {
            get { return _date; }
            set { _date = value; }
        }

        //A post may has zero or more comments
        public virtual List<Comment> Comments { get; set; }
    }
}