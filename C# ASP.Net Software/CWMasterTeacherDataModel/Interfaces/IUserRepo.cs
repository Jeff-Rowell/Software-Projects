using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IUserRepo : IRepository<User>
    {
        IEnumerable<User> ApprovedUsersForWorkingGroup(Guid workingGroupId);
        IEnumerable<User> AllUsersForWorkingGroup(Guid workingGroupId);
        User UserByUserName(string userName);
    }
}
