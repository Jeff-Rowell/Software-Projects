using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IStashedLessonPlanRepo: IRepository<StashedLessonPlan>
    {
        List<StashedLessonPlan> StashedLessonPlansForLesson(Guid lessonId);
    }
}
