using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class LessonPlanTabCUDRouter
    {
        private LessonPlanCUDService _lessonPlanCUDService;
        private LessonCUDService _lessonCUDService;
        private StashedLessonPlanCUDService _stashedLessonPlanCUDService;

        public LessonPlanTabCUDRouter(LessonPlanCUDService lessonPlanCUDService, LessonCUDService lessonCUDService,
                                        StashedLessonPlanCUDService stashedLessonPlanCUDService)
        {
            _lessonPlanCUDService = lessonPlanCUDService;
            _lessonCUDService = lessonCUDService;
            _stashedLessonPlanCUDService = stashedLessonPlanCUDService;
        }

        public void AddOrUpdateLessonPlan(Guid lessonPlanId, Guid lessonId, Guid userId, string text, string name,
                                            bool isPreNote, bool isPostNote)
        {
            _lessonPlanCUDService.AddOrUpdateLessonPlan(lessonPlanId: lessonPlanId, 
                                                        lessonId: lessonId, 
                                                        userId: userId, 
                                                        text: text, 
                                                        name: name,
                                                        isPreNote: isPreNote,
                                                        isPostNote: isPostNote);

        }

        public void CreateSavedStashedLessonPlan(Guid lessonPlanId, Guid lessonId)
        {
            _stashedLessonPlanCUDService.CreateStashedLessonPlanFromLessonId(lessonId: lessonId, lessonPlanId: lessonPlanId, isSaved: true);
        }


    }//End Class
}
