namespace AfricanMagicSystem.Migrations
{
    using AfricanMagicSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AfricanMagicSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AfricanMagicSystem.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser { UserName = "admin@afmag.com" };
            var guestUser = new ApplicationUser { UserName = "guest@guest.com" };

            userManager.Create(user, "Admin123@"); //strong password!#@$!
            userManager.Create(guestUser, "guest1"); //strong password!#@$!

            roleManager.Create(new IdentityRole { Name = "Admin" });
            userManager.AddToRole(user.Id, "Admin");
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
