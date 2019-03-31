using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class EFDataComments : ICommentRepository
    {
        //db connection
        private Group7A2Context db = new Group7A2Context();
        public IQueryable<Comment> Comments { get { return db.Comments; } }

        public IQueryable<Post> Posts { get { return db.Posts; } }


        public void Delete(Comment comment)
        {
            db.Comments.Remove(comment);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Comment Save(Comment comment)
        {
            if (comment.CommentId == 0)
            {
                db.Comments.Add(comment);
            }
            else
            {
                db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return comment;
        }
    }
}