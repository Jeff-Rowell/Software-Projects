using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IWorkingGroupRepo : IRepository<WorkingGroup>
    {
        bool AccessCodeIsBeingUsed(string accessCode);
        IEnumerable<WorkingGroup> GetWorkingGroups();
        WorkingGroup WorkingGroupForInstructorAccessCode(string instructorAccessCode);
        WorkingGroup WorkingGroupForStudentAccessCode(string studentAccessCode);
        List<WorkingGroup> WorkingGroupsForStudentUser(Guid studentUserId);
        WorkingGroupStudent WorkingGroupStudentFromIds(Guid workingGroupId, Guid studentUserId);
    }
}
