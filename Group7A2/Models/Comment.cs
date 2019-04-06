using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class Comment
    {
        private DateTime _date = DateTime.Now;
        
        public virtual int CommentId { get; set; }

        [Required(ErrorMessage = "Do not want say anything?")]
        [DisplayName("Comment")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 10)]
        public virtual string Content { get; set; }

        public virtual int PostId { get; set; }

        public virtual Post Post { get; set; }

        public virtual string Author { get; set; }

        //Set PostTime dafault value.
        public DateTime PostTime
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}