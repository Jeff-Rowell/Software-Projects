using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using System;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IUserDomainObjBuilder : IDomainObjBuilder<UserDomainObj, UserDomainObjBasic, User>
    {
        List<UserDomainObjBasic> GetAllUsersForWorkingGroupList(Guid workingGroupId);
        UserDomainObj BuildFromUserName(string userName);
        void SetLastDisplayedCourseId(Guid userId, Guid courseId);
    }
}
