using Group7A2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Group7A2.Startup))]
namespace Group7A2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Creating Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role  
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";


                //Here we create a Admin super user who will maintain the website content.            	 
                ApplicationUser user = UserManager.FindByEmail("luluwheelan@gmail.com");
                if (user != null)
                {
                    roleManager.Create(role);
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }


    }
}
