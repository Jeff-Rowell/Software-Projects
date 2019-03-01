using CWMasterTeacherDataModel;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class CurriculumCUDRouter
    {
        private UserCUDService _userCUDService;
        private CourseCUDService _courseCUDService;
        private LessonCUDService _lessonCUDService;
        private LessonPlanCUDService _lessonPlanCUDService;
        private NarrativeCUDService _narrativeCUDService;
        private MessageCUDService _messageCUDService;
        private MessageUseCUDService _messageUseCUDService;
        private DocumentCUDService _documentCUDService;
        private DocumentUseCUDService _documentUseCUDService;
        private SharedCUDService _sharedCUDService;

        public CurriculumCUDRouter (UserCUDService userCUDService, CourseCUDService courseCUDService,
                LessonCUDService lessonCUDService, NarrativeCUDService narrativeCUDService, 
                LessonPlanCUDService lessonPlanCUDService, MessageCUDService messageCUDService, MessageUseCUDService messageUseCUDService,
                DocumentCUDService documentCUDService, DocumentUseCUDService documentUseCUDService, SharedCUDService sharedCUDService)
        {
            _userCUDService = userCUDService;
            _courseCUDService = courseCUDService;
            _lessonCUDService = lessonCUDService;
            _narrativeCUDService = narrativeCUDService;
            _lessonPlanCUDService = lessonPlanCUDService;
            _messageCUDService = messageCUDService;
            _messageUseCUDService = messageUseCUDService;
            _documentCUDService = documentCUDService;
            _documentUseCUDService = documentUseCUDService;
            _sharedCUDService = sharedCUDService;
        }


        public void SetCourseListDisplayValues(bool showMasters, bool showAll, Guid currentUserId)
        {
            _userCUDService.SetCourseListDisplayValues(currentUserId, showMasters, showAll);
        }

        public void ToggleShowFolders(Guid courseId)
        {
            _courseCUDService.ToggleShowFolders(courseId);
        }

        public void ToggleShowOptionalLessons(Guid courseId)
        {
            _courseCUDService.ToggleShowOptionalLessons(courseId);
        }

        public void ToggleLessonIsCollapsed(Guid lessonId)
        {
            _lessonCUDService.ToggleIsCollapsed(lessonId);
        }

        public void SetLessonIsHidden(Guid lessonId, bool isHidden)
        {
            _lessonCUDService.SetIsHidden(lessonId, isHidden);
        }

        public void SetShowHiddenLessons(Guid courseId, bool doShowHidden)
        {
            _courseCUDService.SetShowHiddenLessons(courseId: courseId, doShowHidden: doShowHidden);
        }


    }
}
