using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class LessonManagerCUDRouter
    {

        private LessonCUDService _lessonCUDService;
        private CourseCUDService _courseCUDService;

        public LessonManagerCUDRouter (LessonCUDService lessonCUDService, CourseCUDService courseCUDService)
        {
            _lessonCUDService = lessonCUDService;
            _courseCUDService = courseCUDService;
        }

        public void CreateOrEditLesson(Guid selectedLessonId, bool isCreate, Guid parentId, string lessonName, Guid courseId, 
                                        int sequenceNumber, bool isFolder, bool isHidden, Guid currentUserId)
        {
            if (selectedLessonId != Guid.Empty && !isCreate)//which means this is an edit
            {
                if (parentId != selectedLessonId)//This is to protect us from putting a lesson inside itself, which would crete an infinite loop.
                {
                    _lessonCUDService.UpdateLesson(lessonId: selectedLessonId, 
                                                    name: lessonName, 
                                                    containerLessonId: parentId,
                                                    courseId: courseId,
                                                    masterLessonId: Guid.Empty,  
                                                    predecessorLessonId: Guid.Empty, 
                                                    sequenceNumber: sequenceNumber,  
                                                    isFolder: isFolder, 
                                                    isHidden: isHidden,
                                                    dateModified: DateTime.Now);
                }

            }
            else
            {
                _lessonCUDService.CreateLesson(name: lessonName,
                                                containerLessonId: parentId,
                                                courseId: courseId,
                                                masterLessonId: Guid.Empty,  
                                                predecessorLessonId: Guid.Empty, 
                                                sequenceNumber: sequenceNumber, 
                                                isFolder: isFolder,
                                                currentUserId: currentUserId, isHidden: isHidden,
                                                dateModified: DateTime.Now);
            }
        }

        public void ToggleLessonIsOptional(Guid lessonId)
        {
            _lessonCUDService.ToggleLessonIsOptional(lessonId);
        }

        public void ToggleLessonIsCollapsed(Guid lessonId)
        {
            _lessonCUDService.ToggleIsCollapsed(lessonId);
        }



        //TODO: call cleaning orphans from here.  See comment for method this calls.
        public Tuple<string, Guid> DeleteLessonReturnMessageAndId(Guid selectedLessonId)
        {
            return _lessonCUDService.DeleteLessonReturnMessageAndId(selectedLessonId);
        }

        public void RaiseSequenceNumber(Guid lessonId)
        {
            _lessonCUDService.RaiseLessonSequenceNumberByOne(lessonId);
        }

        public void LowerSequenceNumber(Guid lessonId)
        {
            _lessonCUDService.LowerLessonSequenceNumberByOne(lessonId);
        }

        public void RenumberAllLessonsInCourse(Guid courseId)
        {
            _lessonCUDService.RenumberAllLessonsInCourse(courseId);
        }

        public void SetLessonIsHidden(Guid lessonId, bool isHidden)
        {
            _lessonCUDService.SetIsHidden(lessonId, isHidden);
        }

        public void SetShowHiddenLessons(Guid courseId, bool doShowHidden)
        {
            _courseCUDService.SetShowHiddenLessons(courseId: courseId, doShowHidden: doShowHidden);
        }



    }//End Class
}
