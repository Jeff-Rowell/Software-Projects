using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class LessonPlanRepo : BaseRepo<LessonPlan>, ILessonPlanRepo
    {

        /// <summary>
        /// This is needed for testing, specifically for mocking.
        /// Users should not use this constructor to instantiate a Repo.
        /// </summary>
        public LessonPlanRepo() : base(new MasterTeacherContext())
        {
        }

        public LessonPlanRepo(MasterTeacherContext context)
            : base(context)
        {
        }
        /// <summary>
        /// Gets a list of LessonPlanLessons for a given lesson, then returns the associated LessonPlans
        /// </summary>
        public IEnumerable<LessonPlan> StashedLessonPlansForLesson(Guid lessonId)
        {
            var listQuery = from x in _context.StashedLessonPlans
                            where x.LessonId == lessonId
                            orderby x.CreationDate descending
                            select x.LessonPlan;
            return listQuery.ToList();

        }

        public void CleanUpOrphanLessonPlans()
        {
            var orphanQuery = from x in _context.LessonPlans
                              where x.Lessons.Count == 0 && x.StashedLessonPlans.Count == 0
                              select x;
            if(orphanQuery.Count()> 0)
            {
                List<Guid> idList = orphanQuery.Select(x => x.Id).ToList();
                foreach (var xId in idList)
                {
                    Delete(xId);
                }
            }
        }


    }//End Class
}
