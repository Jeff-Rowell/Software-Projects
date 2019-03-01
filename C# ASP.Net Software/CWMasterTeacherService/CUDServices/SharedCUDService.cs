using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    /// <summary>
    /// This CUD Service is for things that need to be called from multiple places and thus create Ninject problems.  
    /// This Service should reference no other CUDServices.
    /// </summary>
    public class SharedCUDService
    {
        private ILessonRepo _lessonRepo;
        private ICourseRepo _courseRepo;
        private IMessageUseRepo _messageUseRepo;

        public SharedCUDService (ILessonRepo lessonRepo, ICourseRepo courseRepo, IMessageUseRepo messageUseRepo)
        {
            _lessonRepo = lessonRepo;
            _courseRepo = courseRepo;
            _messageUseRepo = messageUseRepo;
        }


        public void SetCourseMessageBooleansById(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);
            if (course != null)
            {
                SetCourseMessageBooleans(course);
            }
        }

        /// <summary>
        /// Sets the booleans that indicate whether a course has Active Messages, Stored Messages, etc.
        /// Used to determine css for the icon in the dropdown.
        /// </summary>
        public void SetCourseMessageBooleans(Course course)
        {
            course.HasActiveMessages = false;
            course.HasActiveStoredMessages = false;
            course.HasStoredMessages = false;
            course.HasImportantMessages = false;
            course.HasNewMessages = false;

            List<Lesson> lessonList = _lessonRepo.ActiveLessonsForCourse(course.Id).ToList();
            if (lessonList != null && lessonList.Count() > 0)
            {

                foreach (Lesson xLesson in lessonList)
                {
                    if (xLesson.HasActiveMessages)
                    {
                        course.HasActiveMessages = true;
                    }
                    if (xLesson.HasActiveStoredMessages)
                    {
                        course.HasActiveStoredMessages = true;
                    }
                    if (xLesson.HasStoredMessages)
                    {
                        course.HasStoredMessages = true;
                    }
                    if (xLesson.HasImportantMessages)
                    {
                        course.HasImportantMessages = true;
                    }
                    if (xLesson.HasNewMessages)
                    {
                        course.HasNewMessages = true;
                    }
                }
            }
            _courseRepo.Update(course);
        }

        /// <summary>
        /// Allows this to be called with just an id.
        /// </summary>
        /// <param name="lessonId"></param>
        public void SetLessonMessageBooleansFromId(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            SetLessonMessageBooleans(lesson);
        }


        /// <summary>
        /// Sets the booleans that control the css for the icons that indicate messages.
        /// Is called when a message is sent, deleted, moved, etc.
        /// Also needs to be set when a lesson is copied
        /// </summary>
        public void SetLessonMessageBooleans(Lesson lesson)
        {
            List<MessageUse> messageUseList = _messageUseRepo.GetAllActiveMessageUsesForLesson(lesson.Id).ToList();
            DateTime refDate = lesson.Course.Term.StartDate;
            lesson.HasActiveMessages = false;
            lesson.HasActiveStoredMessages = false;
            lesson.HasStoredMessages = false;
            lesson.HasImportantMessages = false;
            lesson.HasNewMessages = false;

            foreach (var x in messageUseList)
            {
                if (!x.IsStored && !x.IsArchived)
                {
                    lesson.HasActiveMessages = true;
                }
                if (x.IsStored)
                {
                    if (x.StorageReferenceTime != null && x.IsStored && x.StorageReferenceTime < x.Lesson.Course.Term.StartDate)
                    {
                        lesson.HasActiveStoredMessages = true;
                    }
                    else
                    {
                        lesson.HasStoredMessages = true;
                    }
                }
                if (x.IsImportant)
                {
                    lesson.HasImportantMessages = true;
                }
                if (x.IsNew)
                {
                    lesson.HasNewMessages = true;
                }
            }

            _lessonRepo.Update(lesson);
        }

        /// <summary>
        ///  Updates the message booleans for a lesson.
        /// </summary>
        public void UpdateMessageBooleansForLessonAndCourseById(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lesson != null)
            {
                UpdateMessageBooleansForLessonAndCourse(lesson);
            }
        }

        /// <summary>
        ///  Updates the message booleans for a lesson.
        ///  This overload lets us call this directly if we already have the lesson
        /// </summary>
        public void UpdateMessageBooleansForLessonAndCourse(Lesson lesson)
        {
            if (lesson != null)
            {
                SetLessonMessageBooleans(lesson);
                SetCourseMessageBooleans(lesson.Course);

            }
        }

        /// <summary>
        /// Updates message booleans for all lessons who may have received a recently posted message.
        /// </summary>
        /// <param name="lessonId"></param>
        public void UpdateAllMessageBooleansForLessonsSharingCourse(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lesson.CourseId != null)
            {
                UpdateAllMessageBooleansForCourse(lesson.CourseId);
            }
        }

        public void UpdateAllMessageBooleansForCourse(Guid courseId)
        {
            List<Lesson> lessonList = _lessonRepo.LessonsForCourse(courseId).ToList();
            foreach (var xLesson in lessonList)
            {
               SetLessonMessageBooleans(xLesson);
            }
            SetCourseMessageBooleansById(courseId);
        }

        /// <summary>
        /// Parameter may or may not be master.  If not master, master should be included in list.
        /// Not ordered since sequence number makes no sense here.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<Lesson> LessonsThatShareMaster(Guid lessonId, bool includeMaster)
        {
            List<Lesson> lessonList = new List<Lesson>();
            if (lessonId != Guid.Empty)
            {
                Lesson lesson = _lessonRepo.GetById(lessonId);
                Guid masterLessonId;
                if (lesson != null)
                {
                    if (lesson.MasterLessonId == null)
                    {
                        masterLessonId = lesson.Id;
                    }
                    else
                    {
                        masterLessonId = lesson.MasterLessonId.Value;
                    }


                    lessonList = _lessonRepo.LessonsThatShareMaster(masterLessonId).ToList();

                    if (!includeMaster)
                    {
                        lessonList = lessonList.Where(x => x.MasterLessonId != null).ToList();
                    }
                }
            }
            return lessonList;
        }


    }//End Class
}
