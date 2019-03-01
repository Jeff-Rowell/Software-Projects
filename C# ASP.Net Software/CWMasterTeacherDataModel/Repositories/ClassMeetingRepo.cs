using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ClassMeetingRepo: BaseRepo<ClassMeeting>, IClassMeetingRepo
    {
        private ClassSectionRepo _classSectionRepo;
        private HolidayRepo _holidayRepo;
        

        /// <summary>
        /// This is needed for testing, specifically for mocking.
        /// Users should not use this constructor to instantiate a ClassMeetingRepo.
        /// </summary>
        public ClassMeetingRepo() : base(new MasterTeacherContext())
        {
            _classSectionRepo = null;
            _holidayRepo = null;
        }

        public ClassMeetingRepo(MasterTeacherContext context, ClassSectionRepo classSectionRepo, HolidayRepo holidayRepo)
            : base(context)
        {
            _classSectionRepo = classSectionRepo;
            _holidayRepo = holidayRepo;
        }

        /// <summary>
        /// All class meetings for a class section
        /// </summary>
        public IEnumerable<ClassMeeting> ClassMeetingsForClassSection(Guid classSectionId)
        {
            var listQuery = from x in _context.ClassMeetings
                                             where x.ClassSectionId == classSectionId
                                             orderby x.StartTime
                                             select x;
            return listQuery.ToList();                                      
        }



        //TODO: Kill this after Jan 2107 redesign
        /// <summary>
        /// Returns a List of DateTime for all the dates during a term on which a particular user has a ClassMeeting
        /// </summary>
        public List<DateTime> DatesForUserAndTerm(Guid userId, Guid termId)
        {
            HashSet<DateTime> dateSet = new HashSet<DateTime>();
            List<ClassSection> classSectionList = _classSectionRepo.ClassSectionsForUserAndTerm(userId, termId).ToList();
            foreach (var xSection in classSectionList)
            {
                foreach (var xMeeting in xSection.ClassMeetings)
                {
                    dateSet.Add(xMeeting.MeetingDate);
                }
            }
            List<DateTime> dateList = dateSet.ToList();
            return dateList.OrderBy(x => x).ToList();
        }

        //TODO: Kill this after Jan 2107 redesign
        /// <summary>
        /// Generates a list of dates in the past (from Now) in this term for which a user has ClassMeetings
        /// Used below in SevenCurrentDates to generate dates to display in Classroom View
        /// </summary>
        private List<DateTime> PastDatesForUserAndTerm(Guid userId, Guid termId)
        {
            List<DateTime> dateList = DatesForUserAndTerm(userId, termId);
            var dateQuery = from x in dateList
                            where x < DomainUtilities.Now 
                            orderby x.Date descending
                            select x;
            return dateQuery.ToList();
        }

        //TODO: Kill this after Jan 2107 redesign
        /// <summary>
        /// Generates a list of dates in the future (from Now) in this term for which a user has ClassMeetings
        /// Used below in SevenCurrentDates to generate dates to display in Classroom View
        /// </summary>      
        private List<DateTime> FutureDatesForUserAndTerm(Guid userId, Guid termId)
        {
            List<DateTime> dateList = DatesForUserAndTerm(userId, termId);
            var dateQuery = from x in dateList
                            where x >= DomainUtilities.Now 
                            orderby x.Date
                            select x;
            return dateQuery.ToList();
        }

        //TODO: Kill this after Jan 2107 redesign
        /// <summary>
        /// A list of seven dates, three in the past, one current, three in the future, on which a given user has a ClassMeeting
        /// </summary>
        public List<DateTime> SevenCurrentDates(Guid userId, Guid termId)
        {
            List<DateTime> pastDates = PastDatesForUserAndTerm(userId, termId).Take(3).ToList();
            List<DateTime> futureDates = FutureDatesForUserAndTerm(userId, termId).Take(4).ToList();
            List<DateTime> allDates = pastDates.Concat(futureDates).ToList();
            return allDates.OrderBy(x => x).ToList();
        }

        public List<ClassMeeting> ClassMeetingsForUserAndDate(Guid userId, ClassMeeting classMeeting)
        {
            var dateQuery = from x in _context.ClassMeetings
                             where x.ClassSection.Course.UserId == userId && x.MeetingDate == classMeeting.MeetingDate
                             orderby x.StartTime
                             select x;
            return dateQuery.ToList();
        }

        public List<ClassMeeting> ClassMeetingsForUserAndDate(Guid userId, DateTime date)
        {
            ClassMeeting tempClassMeeting = new ClassMeeting();
            tempClassMeeting.MeetingDate = date;  //This is because I had a hard time passing raw dates to a LINQ query

            var dateQuery = from x in _context.ClassMeetings
                            where x.ClassSection.Course.UserId == userId && x.MeetingDate == tempClassMeeting.MeetingDate
                            orderby x.StartTime
                            select x;
            return dateQuery.ToList();
        }

        public ClassMeeting NextClassMeetingAfterThisOne(ClassMeeting thisClassMeeting)
        {
            var meetingQuery = from x in _context.ClassMeetings
                               where x.ClassSectionId == thisClassMeeting.ClassSectionId && x.StartTime > thisClassMeeting.StartTime
                               orderby x.StartTime
                               select x;
            return meetingQuery.FirstOrDefault();
        }

        public ClassMeeting LastClassMeetingBeforeThisOne(ClassMeeting thisClassMeeting)
        {
            var meetingQuery = from x in _context.ClassMeetings
                               where x.ClassSectionId == thisClassMeeting.ClassSectionId && x.StartTime < thisClassMeeting.StartTime
                               orderby x.StartTime descending
                               select x;
            return meetingQuery.FirstOrDefault();
        }



    }//End Class
}
