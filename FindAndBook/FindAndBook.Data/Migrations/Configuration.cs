using FindAndBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FindAndBook.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FindAndBookContext>
    {
        private const string AdministratorUserName = "yoanageorgieva@gmail.com";
        private const string AdministratorPassword = "123456";
        private const int RolesCount = 3;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FindAndBookContext context)
        {
            this.SeedUsers(context);
            base.Seed(context);
        }

        private void SeedUsers(FindAndBookContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new string [RolesCount] { "Admin", "Manager", "User"};

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                for (int i = 0; i < RolesCount; i++)
                {
                    var role = new IdentityRole { Name = roles[i] };
                    roleManager.Create(role);
                }

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
