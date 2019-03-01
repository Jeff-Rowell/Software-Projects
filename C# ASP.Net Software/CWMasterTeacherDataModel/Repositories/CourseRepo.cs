using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class CourseRepo : BaseRepo<Course>, ICourseRepo
    {
        LessonRepo _lessonRepo;
        UserRepo _userRepo;

        public CourseRepo(MasterTeacherContext context, LessonRepo lessonRepo, UserRepo userRepo)
            : base(context)
        {
            _lessonRepo = lessonRepo;
            _userRepo = userRepo;
        }

        /// <summary>
        /// I know this looks nuts but it's useful for building SelectLists
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<Course> SingleCourseList(Guid courseId)
        {
            var courseQuery = from x in _context.Courses
                              where x.Id == courseId
                              select x;
            return courseQuery.ToList();
        }

        /// <summary>
        /// Non-master courses for Term and User. Ordered by Name
        /// </summary>
        public List<Course> CoursesForTermAndUser(Guid termId, Guid userId)
        {
            var listQuery = from x in _context.Courses
                            where x.TermId == termId && x.UserId == userId && x.MasterCourseId != null
                            orderby x.Name
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> CoursesForTermAndWorkingGroup(Guid termId, Guid workingGroupId)
        {
            var listQuery = from x in _context.Courses
                            where x.TermId == termId && x.WorkingGroupId == workingGroupId
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> MasterCoursesForWorkingGroup(Guid workingGroupId)
        {
            var listQuery = from x in _context.Courses
                            where x.WorkingGroupId == workingGroupId && x.MasterCourseId == null
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> MasterCoursesForTermAndWorkingGroup(Guid termId, Guid workingGroupId)
        {
            var listQuery = from x in _context.Courses
                            where x.TermId == termId && x.WorkingGroupId == workingGroupId && x.MasterCourseId == null
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> MasterCoursesForTermAndMetaCourse(Guid termId, Guid metaCourseId)
        {
            var listQuery = from x in _context.Courses
                            where x.TermId == termId && x.MetaCourseId == metaCourseId && x.MasterCourseId == null
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> CoursesAllForUser(Guid userId)
        {
            var listQuery = from x in _context.Courses
                            where x.UserId == userId
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> IndividualCoursesForUser(Guid userId)
        {
            var listQuery = from x in _context.Courses
                            where x.UserId == userId && x.MasterCourseId != null
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        public List<Course> IndividualCoursesForUserAndMasterCoursesForWorkingGroup(Guid userId, Guid workingGroupId)
        {
            return IndividualCoursesForUser(userId).Concat(MasterCoursesForWorkingGroup(workingGroupId)).ToList();
        }


        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> IndividualCoursesForTerm(Guid termId)
        {
            var listQuery = from x in _context.Courses
                            where x.TermId == termId && x.MasterCourseId != null
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> IndividualCoursesForTermAndUser(Guid termId, Guid userId)
        {
            var listQuery = from x in _context.Courses
                            where x.UserId == userId && x.MasterCourseId != null && x.TermId == termId
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }


        /// <summary>
        /// Ordered by x.Term.StartDate descending, then by Name
        /// </summary>
        public List<Course> CoursesAllForWorkingGroup(Guid workingGroupId)
        {
            var listQuery = from x in _context.Courses
                            where x.WorkingGroupId == workingGroupId
                            orderby x.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        public List<Course> CoursesForMetaCourse(Guid metaCourseId)
        {
            var listQuery = from x in _context.Courses
                            where x.MetaCourseId == metaCourseId
                            select x;
            return listQuery.ToList();
        }


        public List<Course> CoursesThatShareMaster(Guid masterCourseId)
        {
            var listQuery = from x in _context.Courses
                            where (x.MasterCourseId == masterCourseId || x.Id == masterCourseId) && x.IsActive
                            select x;
            return listQuery.ToList();
        }
        

        /// <summary>
        /// Used in gathering Narratives.
        /// </summary>
        public Course MostRecentCourseForUserAndMetaCourse(Guid userId, Guid metaCourseId) 
        {
            if (metaCourseId != Guid.Empty)
            {
                var courseQuery = from x in _context.Courses
                                  where x.UserId == userId && x.MetaCourseId == metaCourseId
                                  orderby x.Term.StartDate descending
                                  select x;
                return courseQuery.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        //public IEnumerable<WorkingRelationship> GetWorkingRelationshipsForCourse(Guid courseId)
        //{
        //    var listQuery = from x in _context.WorkingRelationships
        //                    where x.CourseId == courseId
        //                    select x;
        //    return listQuery.ToList().OrderBy(x => x.UserNarrativeSequenceNumber);
        //}

        //public WorkingRelationship GetWorkingRelationshipForCourseAndUser(Guid courseId, Guid userId)
        //{
        //    var workRelQuery = from x in _context.WorkingRelationships
        //                       where x.CourseId == courseId && x.UserId == userId
        //                       select x;
        //    return workRelQuery.FirstOrDefault();
        //}

        public Guid GetLastDisplayedClassSectionIdFromUserId(Guid userId)
        {
            User user = _userRepo.GetById(userId);
            return user == null ? Guid.Empty : GetLastDisplayedClassSectionId(user);
        }

        /// <summary>
        /// Used in navigating between tabs and maintainting consistent display
        /// </summary>
        public Guid GetLastDisplayedClassSectionId(User user)
        {
            Course course = null;
            if (user.LastDisplayedCourseId.HasValue && user.LastDisplayedCourseId != Guid.Empty)
            {
                course = GetById(user.LastDisplayedCourseId.Value);
                if (course.LastDisplayedClassSectionId.HasValue)
                {
                    return course.LastDisplayedClassSectionId.Value;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            else
            {
                return Guid.Empty;
            }
        }

        public static List <Lesson> ContainerChildLessonsForCourse(Course course)
        {
            var lessonQuery = from xLesson in course.Lessons
                              where xLesson.ContainerLesson == null
                              orderby xLesson.SequenceNumber
                              select xLesson;

            return lessonQuery.ToList();
        }


        /// <summary>
        /// This gets all the courses with an end date after Now and working group Ids in the list.
        /// </summary>
        /// <param name="workingGroupIds"></param>
        /// <returns></returns>
        public List<Course> AllCurrentCoursesForWorkingGroupList(List<Guid> workingGroupIds)
        {
            var query = from x in _context.Courses
                        where workingGroupIds.Contains(x.WorkingGroupId) && x.Term.EndDate > DateTime.Now
                        select x;
            return query.ToList();
        }


    }//End class
}
