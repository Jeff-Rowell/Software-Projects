using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class CoursePreferenceAdminCUDRouter
    {
        private CoursePreferenceCUDService _coursePreferenceCUDService;
        
        public CoursePreferenceAdminCUDRouter(CoursePreferenceCUDService coursePreferenceCUDService)
        {
            _coursePreferenceCUDService = coursePreferenceCUDService;
        }


        public void UpdateCoursePreference(Guid coursePreferenceId, bool doShowNarrativeNotifications, bool doShowLessonPlanNotifications, bool doShowDocumentNotifications)
        {
            _coursePreferenceCUDService.UpdateCoursePreference(coursePreferenceId: coursePreferenceId,
                                                                doShowNarrativeNotifications: doShowNarrativeNotifications,
                                                                doShowLessonPlanNotifications: doShowLessonPlanNotifications,
                                                                doShowDocumentNotifications: doShowDocumentNotifications);
        }
    }
}
