using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ILessonPlanRepo : IRepository<LessonPlan>
    {
        IEnumerable<LessonPlan> StashedLessonPlansForLesson(Guid lessonId);
        void CleanUpOrphanLessonPlans();
    }
}
