using Group7A2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web.Security;

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
                roleManager.Create(role);

            }
            AddAdmin(UserManager,"group7a2@gmail.com");
        }

        private static void AddAdmin(UserManager<ApplicationUser> UserManager,string Email)
        {
            ApplicationUser user = UserManager.FindByEmail(Email);
            if (user == null) return;
            var result1 = UserManager.AddToRole(user.Id, "Admin");
         
        }


    }
}
