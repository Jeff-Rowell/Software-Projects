using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IMetaCourseRepo : IRepository<MetaCourse>
    {
        IEnumerable<MetaCourse> MetaCoursesForWorkingGroup(Guid workingGroupId);
    }
}
