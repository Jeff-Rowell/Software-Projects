using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class UserAdminCUDRouter
    {
        private UserCUDService _userCUDService;

        public UserAdminCUDRouter(UserCUDService userCUDService)
        {
            _userCUDService = userCUDService;
        }

        public string EditUser_ReturnMessage(Guid userId, Guid workingGroupId, string userName, string firstName,
                                            string lastName, string displayName, bool isApplicationAdmin, bool isWorkingGroupAdmin, bool isEditor,
                                            string emailAddress, bool isActive, bool hasAdminApproval)
        {
            _userCUDService.AdminUpdateUser(userId: userId, workingGroupId: workingGroupId, userName: userName,
                                    firstName: firstName, lastName: lastName, displayName: displayName,
                                    isApplicationAdmin: isApplicationAdmin, isWorkingGroupAdmin: isWorkingGroupAdmin,
                                    isEditor: isEditor, emailAddress: emailAddress, isActive: isActive, hasAdminApproval: hasAdminApproval);
            return "User " + firstName + " " + lastName + " has been edited.";
        }
    }
}
