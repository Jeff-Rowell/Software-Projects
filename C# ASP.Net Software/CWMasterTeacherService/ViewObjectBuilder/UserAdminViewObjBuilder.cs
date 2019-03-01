using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using CWMasterTeacherDataModel.Interfaces;
using System;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// Returns an instance of the User View Object with appropriate information
    /// constructed from a User Domain Object.
    /// </summary>
    public class UserAdminViewObjBuilder
    {
        IUserDomainObjBuilder _userDomainObjBuilder;
        IWorkingGroupDomainObjBuilder _workingGroupObjBuilder;
        /// <summary>
        /// Constructs an instance of UserService from an instance of UserDomainObjBuilder
        /// for which a User Domain Object should be built.
        /// </summary>
        /// <param name="userDomainObjectBuilder"></param>
        public UserAdminViewObjBuilder(IUserDomainObjBuilder userDomainObjBuilder, IWorkingGroupDomainObjBuilder workingGroupDomainObjBuilder)

        {
            _userDomainObjBuilder = userDomainObjBuilder;
            _workingGroupObjBuilder = workingGroupDomainObjBuilder;
        }

        /// <summary>
        /// Builds a UserViewObject from the given userId and workingGroupId. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workingGroupId"></param>
        /// <returns></returns>
        public UserAdminViewObj RetrieveUserAdminViewObject(UserDomainObj currentUserObj, Guid userId, Guid workingGroupId, string currentUserName, 
                                bool showAllWorkingGroups, bool doChangePassword, string message)
        {
            UserAdminViewObj userViewObj = new UserAdminViewObj();

            if (userId != Guid.Empty)
            {
                UserDomainObj userObj = _userDomainObjBuilder.BuildFromId(userId);
                userViewObj.UserObj = userObj;
                workingGroupId = userObj.WorkingGroupId;             
            }
            else
            {
                UserDomainObj user = new UserDomainObj();
                user.Id = Guid.Empty;
                user.UserName = "";
                userViewObj.UserObj = user;
            }

            if (workingGroupId != Guid.Empty)
            {                
                userViewObj.WorkingGroupName = _workingGroupObjBuilder.BuildBasicFromId(workingGroupId).Name;
                userViewObj.WorkingGroupId = workingGroupId;
            }
            else
            {
                workingGroupId = currentUserObj.WorkingGroupId;
                userViewObj.WorkingGroupId = currentUserObj.WorkingGroupId;
                userViewObj.WorkingGroupName = currentUserObj.WorkingGroupName;
            }

            userViewObj.UserObjBasicList  = _userDomainObjBuilder.GetAllUsersForWorkingGroupList(workingGroupId);
            userViewObj.WorkingGroupObjList = _workingGroupObjBuilder.AllWorkingGroupsBasicList();
            userViewObj.CurrentUserObj = _userDomainObjBuilder.BuildFromUserName(currentUserName);
            userViewObj.ShowAllWorkingGroups = showAllWorkingGroups;
            userViewObj.DoChangePassword = doChangePassword;
            userViewObj.Message = message;

            return userViewObj;
        }



    }
}
