namespace CWMasterTeacherDataModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CWMasterTeacherDataModel.MasterTeacherContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CWMasterTeacherDataModel.MasterTeacherContext context)
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
            UserPreference userPreference = SeedUserPreference(context);
            WorkingGroupPreference workGroupPreference = SeedWorkGroupPreference(context);

            WorkingGroup workingGroup = SeedWorkingGroup(context, workGroupPreference);

            SeedUser(context, workingGroup, userPreference);

        }




        private WorkingGroup SeedWorkingGroup(CWMasterTeacherDataModel.MasterTeacherContext context, WorkingGroupPreference workPref)
        {
            WorkingGroup seedWorkingGroup = new WorkingGroup();
            string adminWorkingGroupName = "SeedWorkingGroup";

            var instQuery = from x in context.WorkingGroups
                            where x.Name == adminWorkingGroupName
                            select x;
            if (instQuery.Count() > 0)
            {
                seedWorkingGroup = instQuery.FirstOrDefault();
            }
            else
            {
                seedWorkingGroup = new WorkingGroup();
                seedWorkingGroup.Name = "SeedWorkingGroup";
                seedWorkingGroup.WorkingGroupPreferenceId = workPref.Id;
                context.WorkingGroups.Add(seedWorkingGroup);
                context.SaveChanges();
            }
            return seedWorkingGroup;
        }

        private UserPreference SeedUserPreference(CWMasterTeacherDataModel.MasterTeacherContext context)
        {
            UserPreference userPreference = new UserPreference();
            string seedPrefName = "SeedPreference";
            var prefQuery = from x in context.UserPreferences
                            where x.Name == seedPrefName
                            select x;
            if (prefQuery.Count() > 0)
            {
                userPreference = prefQuery.FirstOrDefault();
            }
            else
            {
                userPreference.Name = seedPrefName;
                context.UserPreferences.Add(userPreference);
                context.SaveChanges();
            }

            return userPreference;
        }

        private WorkingGroupPreference SeedWorkGroupPreference(CWMasterTeacherDataModel.MasterTeacherContext context)
        {
            WorkingGroupPreference userPreference = new WorkingGroupPreference();
            string seedPrefName = "SeedPreference";
            var prefQuery = from x in context.WorkingGroupPreferences
                            where x.Name == seedPrefName
                            select x;
            if (prefQuery.Count() > 0)
            {
                userPreference = prefQuery.FirstOrDefault();
            }
            else
            {
                userPreference.Name = seedPrefName;
                context.WorkingGroupPreferences.Add(userPreference);
                context.SaveChanges();
            }

            return userPreference;
        }

        private void SeedUser(CWMasterTeacherDataModel.MasterTeacherContext context, WorkingGroup workingGroup, UserPreference userPreference)
        {
            string adminUserName = "SeedUser";
            var userQuery = from x in context.Users
                            where x.UserName == adminUserName
                            select x;
            if (userQuery.Count() == 0)
            {
                User user = new User();
                user.UserName = adminUserName;
                user.WorkingGroupId = workingGroup.Id;
                user.UserPreferenceId = userPreference.Id;
                user.FirstName = "Seed";
                user.LastName = "User";
                user.DisplayName = "Seed User";
                user.EmailAddress = "myBogusEmail@bogus.com";
                user.IsActive = true;
                user.IsNarrativeEditor = true;
                user.IsApplicationAdmin = true;
                user.IsWorkingGroupAdmin = true;
                user.HasAdminApproval = true;

                context.Users.Add(user);
                context.SaveChanges();
            }
        }



    }
}
