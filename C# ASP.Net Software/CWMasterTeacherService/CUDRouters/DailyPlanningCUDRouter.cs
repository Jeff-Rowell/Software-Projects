using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class DailyPlanningCUDRouter
    {
        private LessonUseCUDService _lessonUseCUDService;
        private ReferenceCalendarCUDService _referenceCalendarCUDService;
        private ClassMeetingCUDService _classMeetingCUDService;
        private ReleasedDocumentCUDService _releasedDocCUDService;
        private LessonCUDService _lessonCUDService;

        public DailyPlanningCUDRouter(LessonUseCUDService lessonUseCUDService, ReferenceCalendarCUDService referenceCalendarCUDService,
                                        ClassMeetingCUDService classMeetingCUDService, ReleasedDocumentCUDService releasedDocCUDService,
                                        LessonCUDService lessonCUDService)
        {
            _lessonUseCUDService = lessonUseCUDService;
            _referenceCalendarCUDService = referenceCalendarCUDService;
            _classMeetingCUDService = classMeetingCUDService;
            _releasedDocCUDService = releasedDocCUDService;
            _lessonCUDService = lessonCUDService;
        }

        public Guid AddLessonUseReturnId(Guid selectedLessonId, Guid selectedClassMeetingId)
        {
            return _lessonUseCUDService.CreateLessonUseReturnId(selectedLessonId, selectedClassMeetingId);
        }

        public void RemoveLesson(Guid lessonUseId)
        {
            _releasedDocCUDService.DeleteAllReleasedDocumentsForLessonUse(lessonUseId);
            if(lessonUseId != Guid.Empty)
            {
                _lessonUseCUDService.DeleteLessonUse(lessonUseId);
            }

        }

        public void MoveLessonBack(Guid lessonUseId)
        {
            if (lessonUseId != Guid.Empty)
            {
                _lessonUseCUDService.MoveLessonUseBack(lessonUseId);
            }
        }

        public void MoveLessonForward(Guid lessonUseId)
        {
            if (lessonUseId != Guid.Empty)
            {
                _lessonUseCUDService.MoveLessonUseForward(lessonUseId);
            }
        }

        public void RaiseSequenceNumber(Guid lessonUseId)
        {
            if (lessonUseId != Guid.Empty)
            {
                _lessonUseCUDService.RaiseLessonUseSequenceNumber(lessonUseId);
            }
        }

        public void LowerSequenceNumber(Guid lessonUseId)
        {
            if (lessonUseId != Guid.Empty)
            {
                _lessonUseCUDService.LowerLessonUseSequenceNumber(lessonUseId);
            }
        }

        public void AddReferenceCalendar(Guid selectedClassSectionId, Guid addToReferencesClassSectionId)
        {
            if (selectedClassSectionId != Guid.Empty && addToReferencesClassSectionId != Guid.Empty)
            {
                _referenceCalendarCUDService.CreateReferenceCalendar(selectedClassSectionId, addToReferencesClassSectionId);
            }
        }

        public Guid CustomLessonPlanUpdateReturnId(Guid lessonUseId, Guid selectedClassMeetingId, int lessonUseSequenceNumber,
                                                    string customName, string customText)
        {
            return _lessonUseCUDService.CustomLessonPlanUpdateReturnId(lessonUseId: lessonUseId, selectedClassMeetingId: selectedClassMeetingId,
                                                                  lessonUseSequenceNumber: lessonUseSequenceNumber, customName: customName,
                                                                  customText: customText);
        }

        public void SetClassMeetingIsReady(Guid classMeetingId, bool isReadyToTeach)
        {
            _classMeetingCUDService.SetClassMeetingIsReady(classMeetingId, isReadyToTeach);
        }

        public void ToggleLessonIsCollapsed(Guid lessonId)
        {
            _lessonCUDService.ToggleIsCollapsed(lessonId);
        }
    }//End Class
}
