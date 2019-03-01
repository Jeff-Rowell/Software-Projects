using System.Collections.Generic;
using System.Linq;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherService.CUDServices;
using System;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// User Domain Object Builder returns a Domain object or DomainBasic object for a user.
    /// This builder implements 4 methods: Build, BuildFromId, BuildBasic, and BuildBasicFromId
    /// </summary>
    public class UserDomainObjBuilder : IUserDomainObjBuilder
    {
        private static UserRepo _userRepo;
        private static WorkingGroupRepo _workingGroupRepo;
        private UserCUDService _userCUDService;
        private TermRepo _termRepo;

        
        /// <summary>
        /// Constructs a UserDomainObjBuilder, requires a UserRepo
        /// </summary>
        /// <param name="userRepo"></param>
        public UserDomainObjBuilder(UserRepo userRepo, UserCUDService userCUDService, TermRepo termRepo)
        {
            _userRepo = userRepo;
            _userCUDService = userCUDService;
            _termRepo = termRepo;
        }




        /// <summary>
        /// Build takes a User DbObject and creates a UserDomainObj.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UserDomainObj</returns>
        public static UserDomainObj Build(User user)
        {
            UserDomainObj userDomainObj = new UserDomainObj(user.Id, user.UserName, user.FirstName, user.LastName);

            userDomainObj.BasicObj = BuildBasic(user);            
            userDomainObj.WorkingGroupId = user.WorkingGroupId;
            userDomainObj.WorkingGroupName = user.WorkingGroup.Name;
            userDomainObj.EmailAddress = user.EmailAddress;
            userDomainObj.IsApplicationAdmin = user.IsApplicationAdmin;
            userDomainObj.IsWorkingGroupAdmin = user.IsWorkingGroupAdmin;
            userDomainObj.IsMasterEditor = user.IsNarrativeEditor;
            userDomainObj.IsActive = user.IsActive;
            userDomainObj.HasAdminApproval = user.HasAdminApproval;
            userDomainObj.LastLogin = user.LastLogin == null ? new DateTime() : user.LastLogin.Value;
            userDomainObj.ShowMastersInCourseList = user.ShowMastersInCourseList;
            userDomainObj.ShowAllInCourseList = user.ShowAllInCourseList;
            userDomainObj.WorkingGroupId = user.WorkingGroupId;

            if (user.LastDisplayedCourseId.HasValue)
            {
                userDomainObj.LastDisplayedCourseId = user.LastDisplayedCourseId.Value;
            }
            else
            {
                userDomainObj.LastDisplayedCourseId = Guid.Empty;
            }

            if (user.LastDisplayedCourseId.HasValue)
            {
                userDomainObj.LastDisplayedCourseId = user.LastDisplayedCourseId.Value;
            }
            else
            {
                userDomainObj.LastDisplayedCourseId = Guid.Empty;
            }

            return userDomainObj;
        }
       
        /// <summary>
        /// Builds UserDomainObj from provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserDomainObj</returns>

        public UserDomainObj BuildFromId(Guid id)
        {
            return Build(_userRepo.GetById(id));
        }

        //I'm doing the DisplayName this way in order to not mess with all the test code.
        /// <summary>
        /// BuildBasic creates a UserDomainObjBasic from a User DbObject.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UserDomainObjBasic</returns>
        public static UserDomainObjBasic BuildBasic(User user)
        {     
            UserDomainObjBasic basicObj = new UserDomainObjBasic(user.Id, user.UserName, user.FirstName, user.LastName );
            basicObj.DisplayName = user.DisplayName;
            return basicObj;
        }
        /// <summary>
        /// BuildBasicFromId builds a UserDomainObjBasic from an id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserDomainObjBasic</returns>
        
        public UserDomainObjBasic BuildBasicFromId(Guid id)
        {
            return BuildBasic(_userRepo.GetById(id));
        }

        /// <summary>
        /// Builds a list of users that are associated with the provided workingGroup id.
        /// </summary>
        /// <param name="workingGroupId">The id of the workingGroup for which all users will be built.</param>
        /// <returns>A list of UserDomainObj that are associated with the workingGroup id.</returns>
        public List<UserDomainObjBasic> GetAllUsersForWorkingGroupList(Guid workingGroupId)
        {
            IEnumerable<User> usersForWorkingGroupList = _userRepo.AllUsersForWorkingGroup(workingGroupId);
            return usersForWorkingGroupList == null ? new List<UserDomainObjBasic>() : usersForWorkingGroupList.Select(x => BuildBasic(x)).ToList();
        }

        public UserDomainObj BuildFromUserName(string userName)
        {
            User user = _userRepo.UserByUserName(userName);          
            return user == null ? null : Build(user);
        }

        public UserDomainObjBasic BuildBasicFromUserName(string userName)
        {
            return BuildBasic(_userRepo.UserByUserName(userName));
        }

        public void SetLastDisplayedCourseId (Guid userId, Guid courseId)
        {
            _userCUDService.SetLastDisplayedCourseId(userId, courseId);
        }

        public void SetLastLogin(Guid userId)
        {
            User user = _userRepo.GetById(userId);
            if(user != null)
            {
                user.LastLogin = DateTime.Now;
                _userRepo.Update(user);
            }
        }

        public List<TermDomainObj>UsersInWorkingGroupByTerm(Guid workingGroupId)
        {
            List<User> userList = _userRepo.AllUsersForWorkingGroup(workingGroupId).ToList();
            return UsersByTerm(userList, workingGroupId);
        }

        private List<TermDomainObj> UsersByTerm(List<User> userList, Guid workingGroupId)
        {
            List<TermDomainObj> returnList = new List<TermDomainObj>();
            List<Term> termList = _termRepo.TermsForWorkingGroup(workingGroupId).ToList();

            foreach (var xTerm in termList)
            {
                List<User> termUserList = userList.Where(x => x.LastLogin > xTerm.StartDate).ToList();
                userList = userList.Except(termUserList).ToList();
                if (termUserList.Count > 0)
                {
                    List<UserDomainObjBasic> userObjList = termUserList.Select(x => UserDomainObjBuilder.BuildBasic(x)).ToList();
                    TermDomainObj termObj = TermDomainObjBuilder.BuildWithUserList(xTerm, userObjList);
                    returnList.Add(termObj);
                }
            }
            return returnList;
        }

        private List<UserDomainObjBasic> UsersThatHaveCurrentCoursesInTerm(Guid termId, Guid currentUserId)
        {
            return new List<UserDomainObjBasic>();
        }


    }
}
