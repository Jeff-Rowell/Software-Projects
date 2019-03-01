using CWMasterTeacherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ILessonPlanDomainObjBuilder : IDomainObjBuilder<LessonPlanDomainObj,
                                                                     LessonPlanDomainObjBasic,
                                                                     LessonPlan>
    {
        LessonPlanDomainObj GetLessonPlanObjForLesson(Guid lessonId);
        string GetLessonPlanTextForLesson(Guid lessonId);
    }
}
