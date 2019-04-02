using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class EFDataPosts : IPostRepository
    {
        //db connection
        private Group7A2Context db = new Group7A2Context();
        public IQueryable<Post> Posts { get { return db.Posts; } }

        public IQueryable<Category> Categories { get { return db.Categories; } }

        public IQueryable<Comment> Comments { get { return db.Comments; } }
         
        public void Delete(Post post)
        {
            db.Posts.Remove(post);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Post Save(Post post)
        {
            if (post.PostId == 0)
            {
                db.Posts.Add(post);
            }
            else
            {
                db.Entry(post).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return post;
        }

    }
}