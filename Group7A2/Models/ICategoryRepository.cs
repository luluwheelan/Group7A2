using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        Category Save(Category category);
        void Delete(Category category);
        void Dispose();
    }
}