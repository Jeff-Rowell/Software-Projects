using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IClassSectionRepo: IRepository<ClassSection>
    {
        IEnumerable<ClassSection> ClassSectionsForUserAndTerm(Guid userId, Guid termId);
    }
}
