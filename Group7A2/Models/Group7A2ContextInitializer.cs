using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class Group7A2ContextInitializer : DropCreateDatabaseAlways<Group7A2Context>
    {
        protected override void Seed(Group7A2Context context)
        {
            Category books = new Category { Name = "Text Books", Description = "Buy and sale your text books" };

            Category carPool = new Category { Name = "Car Pool", Description = "Find a ride" };

            Category club = new Category { Name = "Clubs", Description = "Join a club" };

            Category news = new Category { Name = "News", Description = "What is happening" };

            context.Categories.Add(books);
            context.Categories.Add(carPool);
            context.Categories.Add(club);
            context.Categories.Add(news);



            base.Seed(context);
        }
    }
}