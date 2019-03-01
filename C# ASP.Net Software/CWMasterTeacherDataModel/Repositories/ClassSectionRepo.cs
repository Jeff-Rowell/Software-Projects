using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ClassSectionRepo: BaseRepo<ClassSection>, IClassSectionRepo
    {
        private CourseRepo _courseRepo;
        private TermRepo _termRepo;

        public ClassSectionRepo() : base(new MasterTeacherContext())
        {
            _courseRepo = null;
            _termRepo = null;
        }

        public ClassSectionRepo(MasterTeacherContext context, CourseRepo courseRepo, TermRepo termRepo)
            : base(context)
        {
            _courseRepo = courseRepo;
            _termRepo = termRepo;
        }

        public IEnumerable<ClassSection> ClassSectionsForCourse(Guid courseId)
        {
            var listQuery = from x in _context.ClassSections
                                             where x.CourseId == courseId
                                             orderby x.Name
                                             select x;
            return listQuery.ToList();                                      
        }

        public IEnumerable<ClassSection> ClassSectionsForUser(Guid userId)
        {
            var listQuery = from x in _context.ClassSections
                            where x.Course.UserId == userId
                            orderby x.Course.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        public IEnumerable<ClassSection> ClassSectionsForUserAndTerm(Guid userId, Guid termId)
        {
            var listQuery = from x in _context.ClassSections
                            where x.Course.UserId == userId && x.Course.TermId == termId
                            orderby x.Course.Term.StartDate descending, x.Name
                            select x;
            return listQuery.ToList();
        }

        public IEnumerable<ClassSection> ClassSectionsForUserAndCurrentTerm(Guid userId)
        {
            Term currentTerm = _termRepo.CurrentTerm;
            if (currentTerm != null)
            {
                return ClassSectionsForUserAndTerm(userId, currentTerm.Id);
            }
            else
            {
                return ClassSectionsForUser(userId);
            }

        }

        /// <summary>
        /// A list of ClassSections that share Course.OriginalCourseId with the input.  Ordered by date descending
        /// </summary>
        /// <param name="takeThisMany">How many ClassSections do you want in the list?</param>
        /// <returns></returns>
        public List<ClassSection> PossibleReferenceClassSections(Guid classSectionId, int takeThisMany)
        {
            ClassSection classSection = GetById(classSectionId);
            
            var listQuery = from x in _context.ClassSections
                            where x.Course.MetaCourseId == classSection.Course.MetaCourseId
                            orderby x.Course.Term.StartDate descending
                            select x;
            List<ClassSection> sectionList = listQuery.ToList();
            return sectionList.Take(takeThisMany).ToList();
        }



        

        /// <summary>
        /// This is used in navigating between tabs
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Guid GetLastDisplayedClassMeetingId(User user)
        {
            Guid lastDisplayedClassSectionId = _courseRepo.GetLastDisplayedClassSectionId(user);
            if (lastDisplayedClassSectionId != Guid.Empty)
            {
                ClassSection classSection = GetById(lastDisplayedClassSectionId);
                if (classSection.LastDisplayedClassMeetingId.HasValue)
                {
                    return classSection.LastDisplayedClassMeetingId.Value;
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



        public List<ClassSection> ReferenceSectionsForClassSection(Guid classSectionId)
        {
            var listQuery = from x in _context.ReferenceCalendars
                            where x.ClassSectionId == classSectionId
                            orderby x.ClassSection.Course.Term.StartDate descending
                            select x;
            List<ReferenceCalendar> calendarList = listQuery.ToList();

            List<ClassSection> sectionList = new List<ClassSection>();

            foreach (var xCalendar in calendarList)
            {
                ClassSection classSection = GetById(xCalendar.ReferenceClassSectionId);
                if (classSection != null)
                {
                    sectionList.Add(classSection);
                }
            }
            return sectionList;
        }

    }//End Class
}
