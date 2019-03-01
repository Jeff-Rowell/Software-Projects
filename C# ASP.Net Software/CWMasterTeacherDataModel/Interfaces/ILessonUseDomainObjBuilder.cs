using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ILessonUseDomainObjBuilder : Interfaces.IDomainObjBuilder<LessonUseDomainObj, 
                                                                               LessonUseDomainObjBasic, LessonUse>
    {
        List<LessonUseDomainObj> LessonUseObjsForClassMeeting(Guid classMeetingId);
        string GetTextForLessonUse(Guid lessonUseId);
    }
}
