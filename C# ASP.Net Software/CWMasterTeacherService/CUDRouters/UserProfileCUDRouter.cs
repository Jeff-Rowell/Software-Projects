using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherService.CUDServices;

namespace CWMasterTeacherService.CUDRouters
{
    public class UserProfileCUDRouter
    {
        private UserCUDService _userCUDService;
        private StudentUserCUDService _studentUserCUDService;

        public UserProfileCUDRouter(UserCUDService userCUDService, StudentUserCUDService studentUserCUDService)
        {
            _userCUDService = userCUDService;
            _studentUserCUDService = studentUserCUDService;

        }

        public string EditOrCreateUserProfile_ReturnMessage(Guid userId, Guid workingGroupId, string userName, string firstName,
                                            string lastName, string displayName,
                                            string emailAddress, bool isInstructor)
        {
            if (isInstructor)
            {
                _userCUDService.UserEditOrCreateProfile(userId: userId, workingGroupId: workingGroupId, userName: userName,
                                        firstName: firstName, lastName: lastName, displayName: displayName, emailAddress: emailAddress);
                return "User profile for " + firstName + " " + lastName + " has been edited.";
            }
            else
            {
                _studentUserCUDService.StudentUserEditOrCreateProfile(studentUserId: userId, userName: userName, firstName: firstName, 
                                                                        lastName: lastName, emailAddress: emailAddress );
                return "Student User profile for " + firstName + " " + lastName + " has been edited.";
            }

        }

    }
}
