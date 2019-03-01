using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IClassMeetingRepo: IRepository<ClassMeeting>
    {
        IEnumerable<ClassMeeting> ClassMeetingsForClassSection(Guid classSectionId);
        List<ClassMeeting> ClassMeetingsForUserAndDate(Guid userId, DateTime date);
    }
}
