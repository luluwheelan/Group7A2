using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Comment> Comments { get; }
        Post Save(Post post);
        void Delete(Post post);
        void Dispose();
        Comment Save(Comment comment);
    }
}