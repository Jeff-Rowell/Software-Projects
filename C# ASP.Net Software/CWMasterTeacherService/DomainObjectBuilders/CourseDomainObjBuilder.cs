using System;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherService.DomainObjectBuilders;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// A class for building course domain objects.
    /// </summary>
    public class CourseDomainObjBuilder : ICourseDomainObjBuilder
    {
        private readonly CourseRepo _courseRepo;
        private readonly UserRepo _userRepo;
        private readonly TermRepo _termRepo;
        private readonly MetaCourseRepo _metaCourseRepo;
        private readonly ClassSectionRepo _classSectionRepo;
        private readonly CourseCUDService _courseCUDService;
        private readonly WorkingGroupRepo  _workingGroupRepo;
        

        /// <summary>
        /// Provides services that build and return CourseDomainObjs.
        /// </summary>
        /// <param name="courseRepo"></param>
        public CourseDomainObjBuilder(CourseRepo courseRepo, UserRepo userRepo, TermRepo termRepo, ClassSectionRepo classSectionRepo,
            CourseCUDService courseCUDService, MetaCourseRepo metaCourseRepo, WorkingGroupRepo workingGroupRepo)
        {
            _courseRepo = courseRepo;
            _userRepo = userRepo;
            _termRepo = termRepo;
            _classSectionRepo = classSectionRepo;
            _courseCUDService = courseCUDService;
            _metaCourseRepo = metaCourseRepo;
            _workingGroupRepo = workingGroupRepo;
        }

        /// <summary>
        /// A method to build a course domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CourseDomainObj BuildFromId(Guid id)
        {
            Course course = _courseRepo.GetById(id);
            bool doShowHidden = course.ShowHiddenLessons;
            return course == null ? null : Build(course: course, doShowHidden: doShowHidden,
                                                  referenceCourseId: Guid.Empty,selectedEditableLessonId: Guid.Empty,
                                                  comparisonModeInt: -1);
        }

        //public CourseDomainObj BuildFromId(Guid id, bool doShowHidden)
        //{
        //    Course course = _courseRepo.GetById(id);
        //    return course == null ? null : Build(course: course, doShowHidden: doShowHidden,
        //                                          referenceCourseId: Guid.Empty, selectedEditableLessonId: Guid.Empty,
        //                                          comparisonModeInt: -1);
        //}

        public CourseDomainObj BuildFromIdForComparison(Guid id, bool doShowHidden, 
                                            Guid referenceCourseId, Guid selectedEditableLessonId, 
                                            bool doShowLessonComparisons, int comparisonModeInt)
        {
            Course course = _courseRepo.GetById(id);
            CourseDomainObj courseObj = course == null ? null : Build(course: course, doShowHidden: doShowHidden, 
                                                referenceCourseId: referenceCourseId, selectedEditableLessonId:selectedEditableLessonId, 
                                                comparisonModeInt: comparisonModeInt);

            return courseObj;
        }


        /// <summary>
        /// A method to build a basic course domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CourseDomainObjBasic BuildBasicFromId(Guid id)
        {
            var course = _courseRepo.GetById(id);
            return course == null ? null : BuildBasic(course);

        }

        /// <summary>
        /// A methos to build a couse
        /// </summary>
        /// <param name="course"></param>
        /// <returns>a course domain object</returns>
        public static CourseDomainObj Build(Course course, bool doShowHidden, Guid referenceCourseId, 
                                            Guid selectedEditableLessonId, 
                                            int comparisonModeInt)
        {
            List<Lesson> ContainerChildLessonDbObjs = GetContainerChildLessonsForCourse(course);

            return new CourseDomainObj
            {
                CourseDomainObjBasic = BuildBasic(course),
                DateModified = course.DateModified,
                IsActive = course.IsActive,
                DateCreated = course.DateCreated,
                LastDisplayedClassSectionId = course.LastDisplayedClassSectionId,
                LastDisplayedLessonId = course.LastDisplayedLessonId.GetValueOrDefault(),
                MasterCourseId = course.MasterCourseId.GetValueOrDefault(),
                PredecessorCourseId = course.PredecessorCourseId,
                ShowFolders = course.ShowFolders,
                ShowOptionalLessons = course.ShowOptionalLessons,
                WorkingGroupId = course.WorkingGroupId,
                MetaCourseId = course.MetaCourseId,
                DoShowHiddenLessons = course.ShowHiddenLessons,
                PreferencesObj = CoursePreferenceDomainObjBuilder.BuildBasic(course.CoursePreference),
                MirrorTargetUserDisplayName = course.MirrorTargetCourse == null ? "" : course.MirrorTargetCourse.User.DisplayName,


                ContainerChildLessons = ContainerChildLessonDbObjs.Select(childLesson => LessonDomainObjBuilder
                                                            .Build(lesson: childLesson, doShowHidden: doShowHidden, 
                                                            referenceCourseId: referenceCourseId,
                                                             comparisonModeInt: comparisonModeInt)).ToList(),
                HasCustomMasterEditRights = course.HasMasterEditRights 
            };
        }

        /// <summary>
        /// A static method to construct a basic object.
        /// </summary>
        /// <param name="course"></param>
        /// <returns>a basic course domain instance</returns>
        public static CourseDomainObjBasic BuildBasic(Course course)
        {
            return new CourseDomainObjBasic(courseId: course.Id,
                                              name: course.Name,
                                              isMaster: course.MasterCourseId == null || course.MasterCourseId == Guid.Empty,
                                              termName: course.Term.Name,
                                              userDisplayName: course.User.DisplayName,
                                              termId: course.TermId,
                                              hasActiveMessages: course.HasActiveMessages,
                                              hasImportantMessages: course.HasImportantMessages,
                                              hasActiveStoredMessages: course.HasActiveStoredMessages,
                                              hasNewMessages: course.HasNewMessages,
                                              hasOutForEditDocuments: course.HasOutForEditDocuments,
                                              hasStoredMessages: course.HasStoredMessages,
                                              termEndDate: course.Term.EndDate,
                                              userId :course.UserId);
        }

        /// <summary>
        /// This is a separate method so we can address the "do show hidden" question.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        private static List<Lesson>GetContainerChildLessonsForCourse(Course course)
        {
            List<Lesson> childLessons = CourseRepo.ContainerChildLessonsForCourse(course);
            if (course.ShowHiddenLessons)
            {
                return childLessons;
            }
            else
            {
                return childLessons.Where(x => !x.IsHidden).ToList();
            }
        }


        /// <summary>
        /// Builds a list of courses that are associated with the provided term id and workingGroup id.
        /// </summary>
        /// <param name="termId">The id of the term for which the courses will be built.</param>
        /// <param name="workingGroupId">The id of the workingGroup for which the courses will be built.</param>
        /// <returns>A list of CourseDomainObj that are associated with the term id and workingGroup id.</returns>
        public List<CourseDomainObjBasic> GetCoursesForTermWorkingGroupList(Guid termId, Guid workingGroupId)
        {
            List<Course> coursesForTermWorkingGroupList = _courseRepo.CoursesForTermAndWorkingGroup(termId, workingGroupId);
            return coursesForTermWorkingGroupList == null ? new List<CourseDomainObjBasic>() : coursesForTermWorkingGroupList.Select(x => BuildBasic(x)).ToList();
        }

        /// <summary>
        /// Builds a list of courses that are associated with the provided workingGroup id.
        /// </summary>
        /// <param name="workingGroupId">The id of the workingGroup for which all courses will be built.</param>
        /// <returns>A list of CourseDomainObj that are associated with the workingGroup id.</returns>
        public List<CourseDomainObjBasic> GetCoursesAllForWorkingGroupList(Guid workingGroupId)
        {
            List<Course> coursesAllForWorkingGroupList = _courseRepo.CoursesAllForWorkingGroup(workingGroupId);
            return coursesAllForWorkingGroupList == null ? new List<CourseDomainObjBasic>() : coursesAllForWorkingGroupList.Select(x => BuildBasic(x)).ToList();
        }

        /// <summary>
        /// Builds a list of master courses that are associated with the provided workingGroup id.
        /// </summary>
        /// <param name="workingGroupId">The id of the workingGroup for which all master courses will be built.</param>
        /// <returns>A list of master CourseDomainObj that are associated with the workingGroup id.</returns>

        public List<CourseDomainObjBasic> GetMasterCourseForWorkingGroupList(Guid workingGroupId)
        {
            List<Course> masterCourseForWorkingGroupList = _courseRepo.MasterCoursesForWorkingGroup(workingGroupId);
            return masterCourseForWorkingGroupList == null ? new List<CourseDomainObjBasic>() : masterCourseForWorkingGroupList.Select(x => BuildBasic(x)).ToList();
        }

        /// <summary>
        /// Builds a list of course domain objects associated with a term and user
        /// </summary>
        /// <param name="selectedTermId">A term id used for the db query</param>
        /// <param name="selectedUserId">A user id used for the db query</param>
        /// <returns>A list of master CourseDomainObj that are associated with a termID and user Id</returns>
        public List<CourseDomainObjBasic> GetCoursesForTermAndUserList(Guid selectedTermId, Guid selectedUserId)
        {
            List<Course> courseList = _courseRepo.CoursesForTermAndUser(selectedTermId, selectedUserId);
            return courseList == null ? new List<CourseDomainObjBasic>() : courseList.Select(x => BuildBasic(x)).ToList();
        }

        /// <summary>
        /// Builds a list of course domain objects associated with a user
        /// </summary>
        /// <param name="selectedUserId">A user id used for the db query</param>
        /// <returns>A list of CourseDomainObj associated with the user id</returns>
        public List<CourseDomainObjBasic> GetIndividualCoursesForUserList(Guid selectedUserId)
        {
            List<Course> courseList = _courseRepo.IndividualCoursesForUser(selectedUserId);
            return courseList == null ? new List<CourseDomainObjBasic>() : courseList.Select(x => BuildBasic(x)).ToList();
        }

        /// <summary>
        /// Builds a list of course domain objects associated with a user
        /// </summary>
        /// <param name="selectedTermId">A term id used for the db query</param>
        /// <returns>A list of CourseDomainObj associated with the user id</returns>
        public List<CourseDomainObjBasic> GetIndividualCoursesForTermList(Guid selectedTermId)
        {
            List<Course> courseList = _courseRepo.IndividualCoursesForTerm(selectedTermId);
            return courseList == null ? new List<CourseDomainObjBasic>() : courseList.Select(x => BuildBasic(x)).ToList();
        }

        /// <summary>
        /// Gets the list to display in Curriculum View based on values in the User object
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CourseDomainObjBasic> GetDisplayCourseListForUser(Guid userId)
        {
            User user = _userRepo.GetById(userId);
            List<Course> courseList = _courseRepo.IndividualCoursesForUserAndMasterCoursesForWorkingGroup(userId, user.WorkingGroupId);
            return courseList == null ? new List<CourseDomainObjBasic>() : courseList.Select(x => BuildBasic(x)).ToList();
        }


        public List<CourseDomainObjBasic> GetMasterCourseChildren(Guid id)
        {
            return id == Guid.Empty ? new List<CourseDomainObjBasic>() :
                _courseRepo.GetById(id).MasterCourseChildren.Select(childCourse => BuildBasic(childCourse)).ToList();
        }

        public void SetLastDisplayedLessonId(Guid lessonId)
        {
            _courseCUDService.SetLastDisplayedLessonId(lessonId);
        }

        public void SetLastDisplayedClassSectionId(Guid lastDisplayedLessonId)
        {
            ClassSection classSection = _classSectionRepo.GetById(lastDisplayedLessonId);
            _courseCUDService.SetLastDisplayedClassSectionId(classSection);
        }

        public Guid GetLastDisplayedClassSectionId(Guid userId)
        {
            return _courseRepo.GetLastDisplayedClassSectionIdFromUserId(userId);
        }

        public List<CourseDomainObjBasic > CoursesForTermAndUser(Guid termId, Guid userId)
        {
            List<Course> courseList = _courseRepo.CoursesForTermAndUser(termId, userId);
            return courseList == null || courseList.Count == 0 ? new List<CourseDomainObjBasic>() :
                courseList.Select(xCourse => CourseDomainObjBuilder.BuildBasic(xCourse)).ToList();
        }

        public List<TermDomainObj> CoursesForMetaCourseByTerm(Guid metaCourseId)
        {
            MetaCourse metaCourse = _metaCourseRepo.GetById(metaCourseId);
            List<TermDomainObj> returnList = new List<TermDomainObj>();
            if(metaCourse != null)
            {
                List<Course> courseList = _courseRepo.CoursesForMetaCourse(metaCourseId);
                return CoursesByTerm(courseList, metaCourse.WorkingGroupId);
            }
            else
            {
                return new List<TermDomainObj>();
            }  
        }

        public List<CourseDomainObjBasic>CoursesThatShareMaster(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);
            Guid masterCourseId = Guid.Empty;
            if(course != null)
            {
                if(course.MasterCourseId == null)
                {
                    masterCourseId = course.Id;
                }
                else
                {
                    masterCourseId = course.MasterCourseId.Value;
                }
            }
            if(masterCourseId != Guid.Empty)
            {
                List<Course> courseList = _courseRepo.CoursesThatShareMaster(masterCourseId);
                return courseList.Select(x => BuildBasic(x)).ToList();
            }
            else
            {
                return new List<CourseDomainObjBasic>();
            }
        }

        public List<TermDomainObj> CoursesForUserByTerm(Guid userId, bool doIncludeMasters)
        {
            User user = _userRepo.GetById(userId);
            
            if (user != null)
            {
                List<Course> courseList = doIncludeMasters ? _courseRepo.CoursesAllForUser(userId) : _courseRepo.IndividualCoursesForUser(userId);
                return CoursesByTerm(courseList, user.WorkingGroupId);
            }
            else
            {
                return new List<TermDomainObj>();
            }
        }

        private List<TermDomainObj> CoursesByTerm(List<Course> courseList, Guid workingGroupId)
        {
            List<TermDomainObj> returnList = new List<TermDomainObj>();
            List<Term> termList = _termRepo.TermsForWorkingGroup(workingGroupId).ToList();
            foreach (var xTerm in termList)
            {
                List<Course> termCourseList = courseList.Where(x => x.TermId == xTerm.Id).ToList();
                if (termCourseList.Count > 0)
                {
                    List<CourseDomainObjBasic> courseObjList = courseList.Select(x => CourseDomainObjBuilder.BuildBasic(x)).ToList();
                    TermDomainObj termObj = TermDomainObjBuilder.BuildWithCourseList(xTerm, courseObjList);
                    returnList.Add(termObj);
                }
            }
            return returnList;
        }

        public List<CourseDomainObjBasic>AllAvailableCoursesForWorkingGroupList(List<Guid> workingGroupIdList)
        {
            if(workingGroupIdList.Count > 0)
            {
                return _courseRepo.AllCurrentCoursesForWorkingGroupList(workingGroupIdList).Select(x => BuildBasic(x)).ToList();
            }
            else
            {
                return new List<CourseDomainObjBasic>();
            }
        }

        private List<Guid> GetAllMetaLessonIdsInCourse(Guid referenceCourseId)
        {
            Course referenceCourse = _courseRepo.GetById(referenceCourseId);
            List<Guid> idList = new List<Guid>();
            foreach (var xLesson in referenceCourse.Lessons)
            {
                idList.Add(xLesson.MetaLessonId);
            }
            return idList;
        }

    }//End Class
}