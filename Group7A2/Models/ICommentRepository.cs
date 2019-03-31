using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group7A2.Models
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        IQueryable<Post> Posts { get; }

        Comment Save(Comment comment);
        void Delete(Comment comment);
        void Dispose();
    }
}
