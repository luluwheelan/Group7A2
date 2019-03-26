using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    //This class will connect to database 
    public class EFDataCategories : ICategoryRepository
    {
        //db connection
        private Group7A2Context db = new Group7A2Context();
        public IQueryable<Category> Categories { get { return db.Categories; } }

        public void Delete(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Category Save(Category category)
        {
            if(category.CategoryId == 0)
            {
                db.Categories.Add(category);
            }
            else
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return category;
        }

    }
}