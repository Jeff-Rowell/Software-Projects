using System;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using WebMatrix.WebData;

namespace CWMasterTeacherService.CUDServices
{
    public class UserCUDService
    {
        private IWorkingGroupRepo _workingGroupRepo;
        private IUserPreferenceRepo _userPreferenceRepo;
        private IUserRepo _userRepo;

        public UserCUDService(IUserRepo userRepo, IWorkingGroupRepo workingGroupRepo, IUserPreferenceRepo userPreferenceRepo)
        {
            _workingGroupRepo = workingGroupRepo;
            _userRepo = userRepo;
            _userPreferenceRepo = userPreferenceRepo;
        }

        public void AdminUpdateUser(Guid userId, Guid workingGroupId, string userName, string firstName, string lastName, string displayName,
                        bool isApplicationAdmin, bool isWorkingGroupAdmin, bool isEditor, string emailAddress,
                        bool isActive, bool hasAdminApproval)
        {
            User user = _userRepo.GetById(userId);

            user.IsActive = isActive;
            user.HasAdminApproval = hasAdminApproval;
            user.IsApplicationAdmin = isApplicationAdmin;
            user.IsWorkingGroupAdmin = isWorkingGroupAdmin;
            user.IsNarrativeEditor = isEditor;
            user = SetUserValues(user, workingGroupId, userName, firstName, lastName, displayName, emailAddress);
            _userRepo.Update(user);
        }

        private User SetUserValues(User user, Guid workingGroupId, string userName, string firstName, string lastName, 
                            string displayName, string emailAddress)
        {
            user.WorkingGroupId = workingGroupId;
            user.UserName = userName;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.DisplayName = displayName;
            user.EmailAddress = emailAddress;
            user.LastLogin = DateTime.Now;

            return user;
        }

        public void UserEditOrCreateProfile(Guid userId, Guid workingGroupId, string userName, string firstName, string lastName, string displayName,
                        string emailAddress)
        {
            User user = new User();
            if(userId != Guid.Empty)
            {
                user = _userRepo.GetById(userId);
            }
            else
            {
                UserPreference userPref = _userPreferenceRepo.CreateUserPreference(displayName + "_Preferences");
                user.UserPreferenceId = userPref.Id;
            }

            user = SetUserValues(user, workingGroupId, userName, firstName, lastName, displayName, emailAddress);
            if(userId != Guid.Empty)
            {
                _userRepo.Update(user);
            }
            else
            {
                _userRepo.Insert(user);
            }
        }


        //TODO:  This should probably go away afer redesign???
        /// <summary>
        /// This is used so that when a user logs on, the display can show the last course they were working on.
        /// </summary>
        public void SetLastDisplayedCourseId(Course lastDisplayedCourse)
        {
            User user = lastDisplayedCourse.User;
            user.LastDisplayedCourseId = lastDisplayedCourse.Id;
            _userRepo.Update(user);
        }

        //This is the new one
        public void SetLastDisplayedCourseId(Guid userId, Guid courseId)
        {
            User user = _userRepo.GetById(userId);
            user.LastDisplayedCourseId = courseId;
            _userRepo.Update(user);
        }

        public void SetCourseListDisplayValues(Guid currentUserId, bool showMasters, bool showAll)
        {
            User currentUser = _userRepo.GetById(currentUserId);
            currentUser.ShowMastersInCourseList = showMasters;
            currentUser.ShowAllInCourseList = showAll;
            _userRepo.Update(currentUser);
        }



    }
}
