using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class ClassroomCUDRouter
    {
        private ClassMeetingCUDService _classMeetingCUDService;
        private ClassSectionStudentCUDService _classSectionStudentCUDService;
        private ReleasedDocumentCUDService _releasedDocumentCUDService;
        private LessonUseCUDService _lessonUseCUDService;

        public ClassroomCUDRouter(ClassMeetingCUDService classMeetingCUDService, 
                                    ClassSectionStudentCUDService classSectionStudentCUDService,
                                    ReleasedDocumentCUDService releasedDocumentCUDService,
                                    LessonUseCUDService lessonUseCUDService)
        {
            _classMeetingCUDService = classMeetingCUDService;
            _classSectionStudentCUDService = classSectionStudentCUDService;
            _releasedDocumentCUDService = releasedDocumentCUDService;
            _lessonUseCUDService = lessonUseCUDService;
        }
        
        public void SetClassMeetingIsReady(Guid classMeetingId, bool meetingIsReadyToTeach)
        {
            _classMeetingCUDService.SetClassMeetingIsReady(classMeetingId, meetingIsReadyToTeach);
        }

        public void UpdateClassSectionStudent(Guid classSectionStudentId, bool hasBeenApproved, bool hasBeenDenied)
        {
            _classSectionStudentCUDService.UpdateClassSectionStudent(classSectionStudentId, hasBeenApproved, hasBeenDenied);
        }

        public void ReleaseDocument(Guid documentId, Guid lessonUseId)
        {
            _releasedDocumentCUDService.CreateReleasedDocument(documentId, lessonUseId);
        }

        public void ReleaseAllDocuments(Guid lessonUseId)
        {
            _releasedDocumentCUDService.ReleaseAllDocumentsForLessonUse(lessonUseId);
        }

        public void UnReleaseDocument(Guid releasedDocumentId)
        {
            _releasedDocumentCUDService.DeleteReleasedDocument(releasedDocumentId);
        }

        public void UnReleaseAllDocumentsForLessonUse(Guid lessonUseId)
        {
            _releasedDocumentCUDService.DeleteAllReleasedDocumentsForLessonUse(lessonUseId);
        }

        public void SaveNotesForStudents(string notesForStudents, Guid selectedClassMeetingId)
        {
            _classMeetingCUDService.SaveNotesForStudents(notesForStudents, selectedClassMeetingId);
        }

        public void MoveLessonForward(Guid lessonUseId)
        {
            if (lessonUseId != Guid.Empty)
            {
                _lessonUseCUDService.MoveLessonUseForward(lessonUseId);
            }
        }

        public void CopyLessonForward(Guid lessonUseId)
        {
            if(lessonUseId != Guid.Empty)
            {
                _lessonUseCUDService.CopyLessonUseForward(lessonUseId);
            }
        }





    }//End class
}
