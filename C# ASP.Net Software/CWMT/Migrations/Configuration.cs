namespace CWMT.Migrations
{
    using CWMasterTeacherDomain;
    using CWMT.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CWMT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CWMT.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            SeedUserAndRoles(context);
        }

        private void SeedUserAndRoles(CWMT.Models.ApplicationDbContext context)
        {
            string[] _allRoles = DomainWebUtilities.Roles.AllRoles;

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            for (int i = 0; i < _allRoles.Length; i++)
            {
                if (RoleManager.RoleExists(_allRoles[i]) == false)
                {
                    RoleManager.Create(new IdentityRole(_allRoles[i]));
                }
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var PasswordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName == "SeedUser"))
            {
                var user = new ApplicationUser
                {
                    UserName = "SeedUser",
                    Email = "bogusaddress@bogusmail.com",
                    PasswordHash = PasswordHash.HashPassword("TempPass")
                };

                userManager.Create(user);
                userManager.AddToRole(user.Id, DomainWebUtilities.Roles.Instructor);
            }
        }

    }
}
