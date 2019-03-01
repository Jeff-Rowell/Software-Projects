using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CWMasterTeacherService.CUDServices
{
    public class CourseCUDService
    {
        private ILessonRepo _lessonRepo;
        private IUserRepo _userRepo;
        private ICourseRepo _courseRepo;
        private IMetaCourseRepo _metaCourseRepo;
        private LessonCUDService _lessonCUDService;
        private UserCUDService _userCUDService;
        private ICoursePreferenceRepo _coursePreferenceRepo;
        private CoursePreferenceCUDService _coursePreferenceCUDService;
        private SharedCUDService _sharedCUDService;

        public CourseCUDService(ICourseRepo courseRepo, ILessonRepo lessonRepo, IUserRepo userRepo,
                        IMetaCourseRepo metaCourseRepo, LessonCUDService lessonCUDService,
                        UserCUDService userCUDService, ICoursePreferenceRepo coursePreferenceRepo,
                        CoursePreferenceCUDService coursePreferenceCUDService, SharedCUDService sharedCUDService)
        {
            _lessonRepo = lessonRepo;
            _userRepo = userRepo;
            _courseRepo = courseRepo;
            _metaCourseRepo = metaCourseRepo;
            _lessonCUDService = lessonCUDService;
            _userCUDService = userCUDService;
            _coursePreferenceRepo = coursePreferenceRepo;
            _coursePreferenceCUDService = coursePreferenceCUDService;
            _sharedCUDService = sharedCUDService;
        }


        public string CopyCourseReturnMessage(Guid fromCourseId, Guid? masterCourseId, Guid toUserId, Guid toTermId)
        {
            bool isThisADuplicate = IsThisADuplicateCourse(fromCourseId: fromCourseId, masterCourseId: masterCourseId,
                                                           toUserId: toUserId, toTermId: toTermId);
            if (isThisADuplicate)
            {
                if (masterCourseId == null)
                {
                    return "We already have a master course for this MetaCourse and Term.";
                }
                else
                {
                    return "There is already a course for this master course, user, and term.";
                }
            }
            else
            {
                Course newCourse = CopyCourse(fromCourseId: fromCourseId,
                                                masterCourseId: masterCourseId,
                                                toUserId: toUserId,
                                                toTermId: toTermId);

                string successMessage = "";

                if (newCourse != null && newCourse.Id != Guid.Empty)
                {
                    User newUser = _userRepo.GetById(newCourse.UserId);
                    if (newUser != null)
                    {
                        successMessage = "Course: " + newCourse.Name + " for " + newCourse.User.DisplayName + 
                                         " was successfully created.";
                    }
                    else
                    {
                        successMessage = "Alert!!!!  New Course was created but UserID is not valid.";
                    }
                }
                else
                {
                    successMessage = "Alert!!!.  There were problems creating Course.";
                }

                return successMessage;
            }
        }

        public Course CopyCourse(Guid fromCourseId, Guid? masterCourseId, Guid toUserId, Guid toTermId)
        {
            // go to repo get course by id and copy
            Course fromCourse = _courseRepo.GetById(fromCourseId);

            if (fromCourse != null)
            {
                CoursePreference newCoursePreference = _coursePreferenceCUDService
                                                       .CopyCoursePreference(fromCourse.CoursePreference);
                return CopyCourse(fromCourse, masterCourseId, toUserId, toTermId, newCoursePreference.Id );
            }
            else
            {
                return null;
            }    
        }

        private Course CopyCourse(Course fromCourse, Guid? masterCourseId, Guid toUserId, 
                                  Guid toTermId, Guid newCoursePreferenceId)
        {
            Course newCourse = new Course();

            Course masterCourse;
            if (masterCourseId != null)
            {
                Guid masterId = masterCourseId.Value;
                masterCourse = _courseRepo.GetById(masterId);
            }
            else
            {
                masterCourse = null;
                newCourse.IsMaster = true;
            }

            newCourse.CoursePreferenceId = newCoursePreferenceId;
            newCourse.MetaCourseId = fromCourse.MetaCourseId;
            newCourse.WorkingGroupId = fromCourse.WorkingGroupId;
            newCourse.TermId = toTermId;
            newCourse.User = fromCourse.User; //Jeff added, I think this line may have been needed but missed
            newCourse.UserId = toUserId;
            newCourse.MasterCourseId = masterCourseId;
            newCourse.PredecessorCourseId = fromCourse.Id;
            newCourse.Name = fromCourse.Name;
            newCourse.DateCreated = DateTime.UtcNow;
            newCourse.DateModified = DateTime.UtcNow;
            newCourse.IsActive = true;

            _courseRepo.Insert(newCourse);

            List<Lesson> fromCourseContainerChildLessons = CourseRepo.ContainerChildLessonsForCourse(fromCourse);

            foreach (Lesson xLesson in fromCourseContainerChildLessons)
            {
                _lessonCUDService.CopyLesson(fromLesson: xLesson,
                                               masterCourse: masterCourse,
                                               toCourseId: newCourse.Id,
                                               containerLessonId: null);
            }

            return newCourse;
        }

        public Tuple<Guid, string> CreateMasterCourseReturnIdAndMessage(string name, Guid workingGroupId, 
                                                                        Guid userId, Guid termId)
        {
            string message;
            Guid courseId = Guid.Empty;
            if (workingGroupId == Guid.Empty || userId == Guid.Empty || termId == Guid.Empty)
            {
                message = "Some values were missing";
                return new Tuple<Guid, string>(Guid.Empty, message);
            }

            CoursePreference coursePreference = _coursePreferenceCUDService.CreateCoursePreference(name + 
                                                                                                   "_Preferences");

            MetaCourse metaCourse = new MetaCourse();
            metaCourse.Name = name;
            metaCourse.WorkingGroupId = workingGroupId;
            _metaCourseRepo.Insert(metaCourse);

            Course course = new Course();

            course.CoursePreferenceId = coursePreference.Id;
            course.Name = name;
            course.WorkingGroupId = workingGroupId;
            course.MetaCourseId = metaCourse.Id;
            course.TermId = termId;
            course.UserId = userId;
            course.DateCreated = DateTime.Now;
            course.DateModified = DateTime.Now;
            course.IsActive = true;
            course.IsMaster = true;

            _courseRepo.Insert(course);

            message = "Course successfully created.";
            courseId = course.Id;
            return Tuple.Create(courseId, message);
        }

        public Course RenameCourse(Guid courseId, string newName)
        {
            Course course = _courseRepo.GetById(courseId);
            course.Name = newName;
            _courseRepo.Update(course);
            return course;
        }

        /// <summary>
        /// This assumes that CheckIfCourseIsDeletable has been run and confirmed that we have no attached entities 
        /// other than lessons
        /// </summary>
        public string DeleteCourse(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);
            if (course != null)
            {
                string message = CheckIfCourseDeletable(courseId);
                if (message.Length < 4)
                {

                    List<Lesson> containerChildren = CourseRepo.ContainerChildLessonsForCourse(course);
                    List<Guid> idList = new List<Guid>();
                    foreach (var xLesson in containerChildren)
                    {
                        idList.Add(xLesson.Id);
                    }
                    foreach (Guid xId in idList)
                    {
                        _lessonCUDService.DeleteLessonById(xId);
                    }
                    _courseRepo.Delete(course.Id);
                    return "Course has been deleted.";
                }
                else
                {
                    return message;
                }

            }
            else
            {
                return "Course with that Id does not exist.";
            }
        }

        /// <summary>
        /// Approval string is "OK", which is short.  Other messages are longer so to check OK, check the 
        /// message.length less than 4 Longer strings will describe any issues found that make the course 
        /// not deletable.
        /// </summary>
        public string CheckIfCourseDeletable(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);
            string message = "";

            if (course.MasterCourseChildren.Count > 0)
            {
                message = "  Course has Master Child Courses.  ";
            }
            if (course.ClassSections.Count > 0)
            {
                message = message + "Course has Class Sections.  ";
            }
            if (course.PredecessorCourseChildren.Count > 0)
            {
                message = message + "Course has PredecessorCourseChildren.  ";
            }
            if (message.Length < 3)
            {
                message = "OK";
            }
            message = message + AreLessonsDeletable(course);
            return message;
        }

        private string AreLessonsDeletable(Course course)
        {
            bool areDeletable = true;
            foreach (var xLesson in course.Lessons)
            {
                if (!_lessonCUDService.CheckIfLessonIsDeletable(xLesson))
                {
                    areDeletable = false;
                }
            }
            if (areDeletable)
            {
                return "";
            }
            else
            {
                return "  Lessons have attachments.";
            }
        }

        public void ToggleShowFolders(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);
            course.ShowFolders = !course.ShowFolders;
            _courseRepo.Update(course);
        }

        public void ToggleShowOptionalLessons(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);
            course.ShowOptionalLessons = !course.ShowOptionalLessons;
            _courseRepo.Update(course);
        }
 
        /// <summary>
        /// Sets the booleans for whether a course has a document OutForEdit
        /// Used to determine css for icons.
        /// </summary>
        public void SetCourseDocumentsBoolean(Course course)
        {
            course.HasOutForEditDocuments = false;
            List<Lesson> lessonList = _lessonRepo.ActiveLessonsForCourse(course.Id).ToList();
            if (lessonList != null && lessonList.Count() > 0)
            {

                foreach (Lesson xLesson in lessonList)
                {
                    if (xLesson.HasOutForEditDocuments)
                    {
                        course.HasOutForEditDocuments = true;
                    }
                }
            }
            _courseRepo.Update(course);
        }

        /// <summary>
        /// Used in navigation between tabs and maintaining consistent display
        /// </summary>
        public void SetLastDisplayedClassSectionId(ClassSection lastDisplayedClassSection)
        {
            if (lastDisplayedClassSection != null)
            {
                Course course = lastDisplayedClassSection.Course;
                course.LastDisplayedClassSectionId = lastDisplayedClassSection.Id;
                _courseRepo.Update(course);

                _userCUDService.SetLastDisplayedCourseId(course);
            }
        }

        /// <summary>
        /// This is used so that when a user opens a course, the display can show the last lesson they were working on.
        /// </summary>
        public void SetLastDisplayedLessonId(Guid lastDisplayedLessonId)
        {
            Lesson lastDisplayedLesson = _lessonRepo.GetById(lastDisplayedLessonId);
            Course course = lastDisplayedLesson.Course;
            course.LastDisplayedLessonId = lastDisplayedLesson.Id;
            _courseRepo.Update(course);
        }

        public Course GetById(Guid courseId)
        {
            return _courseRepo.GetById(courseId);
        }

        public List<Guid> GetAllLessonIdsForCourse(Guid courseId)
        {
            Course course = GetById(courseId);
            HashSet<Guid> idSet = new HashSet<Guid>();
            foreach (var xLesson in course.Lessons)
            {
                idSet = AddLessonAndChildrenToIdSet(xLesson, idSet);
            }
            return idSet.ToList();
        }

        private HashSet<Guid> AddLessonAndChildrenToIdSet(Lesson lesson, HashSet<Guid> idSet)
        {
            idSet.Add(lesson.Id);
            foreach (var childLesson in lesson.ContainerLessonChildren)
            {
                idSet = AddLessonAndChildrenToIdSet(childLesson, idSet);
            }
            return idSet;
        }

        public bool IsThisADuplicateCourse(Guid fromCourseId, Guid? masterCourseId, Guid toUserId, Guid toTermId)
        {
            if (masterCourseId != null)  //Which means this is not copying to a master
            {
                List<Course> coursesForTermAndUserAndMaster = _courseRepo.CoursesForTermAndUser(toTermId, toUserId)
                                                                .Where(x => x.MasterCourseId == masterCourseId)
                                                                .ToList();
                return coursesForTermAndUserAndMaster.Count > 0;
            }
            else  //Which means this is copying to a master.
            {
                Course fromCourse = _courseRepo.GetById(fromCourseId);
                if (fromCourse != null)
                {
                    List<Course> masterCoursesForTermAndMetaCourse = _courseRepo.MasterCoursesForTermAndMetaCourse
                                (termId: toTermId, metaCourseId: fromCourse.MetaCourseId);
                    return masterCoursesForTermAndMetaCourse.Count > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        public void SetShowHiddenLessons(Guid courseId, bool doShowHidden)
        {
            Course course = _courseRepo.GetById(courseId);
            if (course != null)
            {
                course.ShowHiddenLessons = doShowHidden;
                _courseRepo.Update(course);
            }
        }
    }
}
