using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    public class ClassMeetingDomainObjBuilder : IClassMeetingDomainObjBuilder
    {
        private IClassMeetingRepo _classMeetingRepo;
        private IClassSectionRepo _classSectionRepo;
        private LessonUseDomainObjBuilder _lessonUseObjBuilder;
        
        /// <summary>
        /// Construct a ClassMeetingDomainObjBuilder to build ClassMeetingDomainObjs.
        /// </summary>
        /// <param name="repo">
        ///     ClassMeetingRepo used by the builder for retrieving ClassMeeting DbObjects.
        /// </param>
        public ClassMeetingDomainObjBuilder(IClassMeetingRepo repo, IClassSectionRepo classSectionRepo, LessonUseDomainObjBuilder lessonUseObjBuilder)
        {
            _classMeetingRepo = repo;
            _classSectionRepo = classSectionRepo;
            _lessonUseObjBuilder = lessonUseObjBuilder;
        }
        
        /// <summary>
        /// Build a ClassMeetingDomainObj from the given ClassMeeting DbObject.
        /// </summary>
        /// <param name="dbObject">
        ///     ClassMeeting DbObject.
        /// </param>
        /// <returns>
        ///     ClassMeetingDomainObj
        /// </returns>
        /// <throws>
        ///     NullReferenceException if dbObject is null.
        /// </throws>
        public static ClassMeetingDomainObj Build(ClassMeeting dbObject)
        {
            if (dbObject == null)
            {
                throw new NullReferenceException("In ClassMeetingDomainObjBuilder.Build: Null reference");
            }

            ClassMeetingDomainObjBasic basicObj = BuildBasic(dbObject);
            ClassMeetingDomainObj domainObj = new ClassMeetingDomainObj(basicObj);

            domainObj.StartTime = dbObject.StartTime;
            domainObj.EndTime = dbObject.EndTime;
            domainObj.MeetingNumber = dbObject.MeetingNumber;
            domainObj.Comment = dbObject.Comment;
            domainObj.IsNoClass = dbObject.NoClass;
            domainObj.IsExamDay = dbObject.IsExamDay;
            domainObj.IsBeginningOfWeek = dbObject.IsBeginningOfWeek;
            if (!domainObj.IsNoClass) domainObj.LessonUseList = dbObject.LessonUses.Select(xLessonUse => LessonUseDomainObjBuilder.Build(xLessonUse)).ToList();
            domainObj.IsReadyToTeach = dbObject.IsReadyToTeach;
            domainObj.Id = dbObject.Id; //This needs to exist independently in both domain object and basic object.
            domainObj.NotesForStudents = dbObject.NotesForStudents;

            return domainObj;
		}
        
        /// <summary>
        /// Build a ClassMeetingDomainObj from the ClassMeeting DbObject with the given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        ///     ClassMeetingDomainObj
        /// </returns>
        /// <throws>
        ///     NullReferenceException if a ClassMeeting cannot be found with the given Id.
        /// </throws>
        public ClassMeetingDomainObj BuildFromId(Guid id)
        {
            ClassMeeting dbObject = _classMeetingRepo.GetById(id);
            if (dbObject == null)
            {
                return null;
            }
            ClassMeetingDomainObj domainObj = Build(_classMeetingRepo.GetById(id));
  
            return domainObj;
		}

        /// <summary>
        /// Build a ClassMeetingDomainObjBasic from the given ClassMeeting DbObject.
        /// </summary>
        /// <param name="dbObject">
        ///     ClassMeeting DbObject used to construct the basic object.
        /// </param>
        /// <returns>
        ///     ClassMeetingDomainObjBasic
        /// </returns>
        /// <throws>
        ///     NullReferenceException if the passed in dbObject is null.
        /// </throws>
        public static ClassMeetingDomainObjBasic BuildBasic(ClassMeeting dbObject)
        {
            if (dbObject == null)
            {
                throw new NullReferenceException("In ClassMeetingDomainObjBuilder.BuildBasic: Null reference");
            }
		    ClassMeetingDomainObjBasic basicObj = new ClassMeetingDomainObjBasic();
            basicObj.Id = dbObject.Id;
            basicObj.ClassSectionName = dbObject.ClassSection.Name;
            basicObj.MeetingDate = dbObject.MeetingDate;
            basicObj.ClassSectionId = dbObject.ClassSectionId;
            basicObj.IsReadyToTeach = dbObject.IsReadyToTeach;
            
            return basicObj;
		}
        
        /// <summary>
        /// Build a basic domain object from the ClassMeeting with the given Id.
        /// </summary>
        /// <param name="id">
        ///     ClassMeeting Id.
        /// </param>
        /// <returns>
        ///     ClassMeetingDomainObjBasic
        /// </returns>
        /// <throws>
        ///     NullReferenceException if there is no ClassMeeting with the given Id in
        ///     the ClassMeetingRepo.
        /// </throws>
        public ClassMeetingDomainObjBasic BuildBasicFromId(Guid id)
        {
            ClassMeeting dbObject = _classMeetingRepo.GetById(id);
            if (dbObject == null)
            {
                return null;
            }
            return BuildBasic(_classMeetingRepo.GetById(id));
        }

        /// <summary>
        /// Generates a list of ClassMeetingDomainObjBasic associated with a given class section Id.
        /// </summary>
        /// <param name="selectedClassSectionId"></param>
        /// <returns></returns>
        public List<ClassMeetingDomainObj> GetClassMeetingsForClassSection(Guid selectedClassSectionId)
        {
            IEnumerable<ClassMeeting> classMeetingList = _classMeetingRepo
                .ClassMeetingsForClassSection(selectedClassSectionId);
            return classMeetingList == null ? new List<ClassMeetingDomainObj>() 
                : classMeetingList.Select(x => Build(x)).ToList();
        }

        public List<ClassMeetingDomainObjBasic> GetClassMeetingBasicsForClassSection(Guid selectedClassSectionId, bool doReturnAllMeetings)
        {
            IEnumerable<ClassMeeting> classMeetingList = _classMeetingRepo
                .ClassMeetingsForClassSection(selectedClassSectionId);
            return classMeetingList == null ? new List<ClassMeetingDomainObjBasic>() 
                : classMeetingList.Select(x => BuildBasic(x)).ToList();
        }

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
        
        /// <summary>
        /// Used in ClassroomView
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<ClassMeetingDomainObjBasic> ClassMeetingsForUserAndDate(Guid userId, DateTime date)
        {
            List<ClassMeeting> classMeetingList = _classMeetingRepo.ClassMeetingsForUserAndDate(userId, date);
            return classMeetingList == null ? new List<ClassMeetingDomainObjBasic>() : classMeetingList.Select(x => BuildBasic(x)).ToList();
        }
    }
}
