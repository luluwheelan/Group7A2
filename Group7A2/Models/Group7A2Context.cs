using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Group7A2.Models
{
    public class Group7A2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Group7A2Context() : base("name=Group7A2Context")
        {
            Database.CommandTimeout = 300;
        }

        public System.Data.Entity.DbSet<Group7A2.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Group7A2.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<Group7A2.Models.Comment> Comments { get; set; }
    }
}
