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

            Category general = new Category { Name = "General", Description = "What is happening" };

            context.Categories.Add(books);
            context.Categories.Add(carPool);
            context.Categories.Add(club);
            context.Categories.Add(general);

            Post p1 = new Post
            {
                Subject = "Sell Java book",
                Content = "The “for dummies” books are rarely worth the money if you already have some experience writing code. But for absolute beginners these books are often perfect because they’re written clearly in plain English without too many confusing terms.",
                Category = books,
                PostTime = DateTime.Now.Date.AddHours(1).AddMinutes(30),
                Author = "Lulu"

            };

            Post p2 = new Post
            {
                Subject = "Orillia to Barrie",
                Content = "Every monday to friday.",
                Category = carPool,
                PostTime = DateTime.Now.Date.AddHours(3).AddMinutes(30),
                Author = "Lulu"

            };
            Post p3 = new Post
            {
                Subject = "Barrie south to north",
                Content = "Every monday morning leave at 7:30.",
                Category = carPool,
                PostTime = DateTime.Now.Date.AddHours(5).AddMinutes(30),
                Author = "Lulu"

            };
            Post p4 = new Post
            {
                Subject = "Looking for CSS book",
                Content = "Looking for CSS Pocket Reference, 4th Edition",
                Category = books,
                PostTime = DateTime.Now.Date.AddHours(24).AddMinutes(30),
                Author = "Lulu"
            };



            context.Posts.Add(p1);
            context.Posts.Add(p2);
            context.Posts.Add(p3);
            context.Posts.Add(p4);

            Comment c1 = new Comment
            {
                Content = "I want your book",
                Post = p1,
                Author = "Lulu",
                PostTime = DateTime.Now.Date.AddHours(24).AddMinutes(30)
            };

            Comment c2 = new Comment
            {
                Content = "How much for your book",
                Post = p1,
                Author = "Lulu",
                PostTime = DateTime.Now.Date.AddHours(2).AddMinutes(30)
            };
            context.Comments.Add(c1);
            context.Comments.Add(c2);
            base.Seed(context);
        }
    }
}