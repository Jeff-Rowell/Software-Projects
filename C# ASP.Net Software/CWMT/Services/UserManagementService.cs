using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CWMasterTeacher3;
using CWMT.Models;
using CWMasterTeacherDomain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CWMT.Services
{
    public class UserManagementService
    {
        private ApplicationDbContext _context;
        public UserManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SynchronizeRoles(UserAdminReturnModel model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));


            var appUser = userManager.FindByName(model.UserName);
            var userId = appUser.Id;

            if (appUser != null)
            {
                if (model.SelectedUserIsApplicationAdmin)
                {
                    userManager.AddToRole(userId, DomainWebUtilities.Roles.ApplicationAdmin);
                }
                else
                {
                    userManager.RemoveFromRole(userId, DomainWebUtilities.Roles.ApplicationAdmin);
                }

                if (model.SelectedUserHasAdminApproval)
                {
                    userManager.AddToRole(userId, DomainWebUtilities.Roles.ApprovedInstructor);
                }
                else
                {
                    userManager.RemoveFromRole(userId, DomainWebUtilities.Roles.ApprovedInstructor);
                }

                if (model.SelectedUserIsEditor)
                {
                    userManager.AddToRole(userId, DomainWebUtilities.Roles.Editor);
                }
                else
                {
                    userManager.RemoveFromRole(userId, DomainWebUtilities.Roles.Editor);
                }

                if (model.SelectedUserIsWorkingGroupAdmin)
                {
                    userManager.AddToRole(userId, DomainWebUtilities.Roles.WorkingGroupAdmin);
                }
                else
                {
                    userManager.RemoveFromRole(userId, DomainWebUtilities.Roles.WorkingGroupAdmin);
                }


            }


        }


    }
}