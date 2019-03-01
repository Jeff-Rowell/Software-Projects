using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class StashedLessonPlanRepo : BaseRepo <StashedLessonPlan >, IStashedLessonPlanRepo
    {
        private LessonPlanRepo _lessonPlanRepo;
        public StashedLessonPlanRepo(MasterTeacherContext context, LessonPlanRepo lessonPlanRepo)
            : base(context)
        {
            _lessonPlanRepo = lessonPlanRepo;
        }

        public List<StashedLessonPlan> StashedLessonPlansForLesson(Guid lessonId)
        {
            var stashQuery = from x in _context.StashedLessonPlans
                             where x.LessonId == lessonId
                             select x;
            return stashQuery.ToList();
        }


    }
}
