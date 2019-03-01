using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ILessonDomainObjBuilder : Interfaces.IDomainObjBuilder<LessonDomainObj, LessonDomainObjBasic, Lesson>
    {
        Guid ReturnLessonIdForMasterLesson(Guid lessonId, Guid userId);
        int SiblingCountForLesson(Guid lessonId);
    }
}
