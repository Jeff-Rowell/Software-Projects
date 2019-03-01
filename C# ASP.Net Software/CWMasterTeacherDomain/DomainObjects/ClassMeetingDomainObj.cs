using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// ClassMeetingDomainObj abstracts the dependencies of the db from the rest of the application
    /// by taking relevant information from the Class DbObject and storing it in the Domain object
    /// </summary>
    public class ClassMeetingDomainObj
    {
        /// <summary>
        /// The basic object, which contains the Id.
        /// </summary>
        private ClassMeetingDomainObjBasic _basicObj;
        private List<LessonUseDomainObj> _lessonUseList;
    

        /// <summary>
        /// A no argument constructor used in ClassRoom View
        /// </summary>
        public ClassMeetingDomainObj() { }

        /// <summary>
        /// Construct a new ClassMeetingDomainObj with the given basic domain object.
        /// </summary>
        /// <param name="basicObj">
        ///     ClassMeetingDomainObjBasic.
        /// </param>
        public ClassMeetingDomainObj(ClassMeetingDomainObjBasic basicObj)
        {
            _basicObj = basicObj;
        }

        public ClassMeetingDomainObjBasic BasicObj { get; set; }


        /// <summary>
        /// Unique identifier of this ClassMeetingDomainObj.
        /// This needs to exist independently here and in the Basic Obj in order for both to be used in Razor.
        /// DomainObj is used in DailyPlanning and BasicObj is used in Classroom
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Same as Id.
        /// </summary>
        public Guid ClassMeetingId
        {
            get
            {
                return Id;
            }
        }

        public List<LessonUseDomainObj> LessonUseList
        {
            get
            {
                if (IsNoClass)
                {
                    LessonUseDomainObj dummyLessonUseObj = new LessonUseDomainObj();
                    dummyLessonUseObj.CustomName = Comment + ": NO CLASS";
                    dummyLessonUseObj.Id = Guid.Empty;
                    List<LessonUseDomainObj> dummyList = new List<LessonUseDomainObj>();
                    dummyList.Add(dummyLessonUseObj);
                    return dummyList;
                }
                else
                {
                    return _lessonUseList.OrderBy(x => x.SequenceNumber).ToList();
                }
            }
            set
            {
                _lessonUseList = value;
            }
        }
        
        public int LessonCount
        {
            get { return LessonUseList.Count; }
        }

        public SelectList LessonUseSelectList
        {
            get {  return new SelectList(LessonUseList, "Id", "DisplayName"); }
        }

        /// <summary>
        /// Identifier for the class section associated with this class meeting.
        /// </summary>
        public Guid ClassSectionId
        {
            get { return _basicObj.ClassSectionId; }
            set { _basicObj.ClassSectionId = value; }
        }


        /// <summary>
        /// The name of the class section associated with this class meeting.
        /// </summary>
        public string ClassSectionName
        {
            get { return _basicObj.ClassSectionName; }
            set { _basicObj.ClassSectionName = value; }
        }

        /// <summary>
        /// The date of the class meeting.
        /// </summary>
        public DateTime MeetingDate
        {
            get { return _basicObj.MeetingDate; }
            set { _basicObj.MeetingDate = value; }
        }

        public String MeetingDateString
        {
            get { return _basicObj.MeetingDateString; }
        }

        /// <summary>
        /// The time the class meeting starts.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The time the class meeting ends.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The meeting number.
        /// </summary>
        public int? MeetingNumber { get; set; }

        /// <summary>
        /// A comment for when there is no class on a class meeting.
        /// </summary>
        public string Comment { get; set; }

        public string NotesForStudents { get; set; }

        /// <summary>
        /// Is there no class for this meeting?
        /// </summary>
        public bool IsNoClass { get; set; }

        /// <summary>
        /// Is this class meeting an exam day?
        /// </summary>
        public bool IsExamDay { get; set; }

        /// <summary>
        /// Does this class meeting fall on the beginning of the week?
        /// </summary>
        public bool IsBeginningOfWeek { get; set; }

        /// <summary>
        /// Is this class meeting ready to be taught?
        /// This value has to exist independently in both DomainObj and Basic in order to 
        /// function correctly in both DailyPlanningView and ClassroomView
        /// </summary>
        public bool IsReadyToTeach { get; set; }

        // TODO figure out what to do here. Need LessonUsesDomainObj?
        //public virtual ICollection<LessonUseDomainObj> LessonUses { get; set; }

        /// <summary>
        /// Name to be displayed to the user.
        /// </summary>
        public string DisplayName
        {
            get
            {
                string noClassString = "";
                if (IsNoClass)
                {
                    noClassString = "   (No Class: " + Comment + ")";
                }
                else
                {
                    noClassString = "";
                }
                return String.Format("{0:ddd, MMM d, yyyy}", MeetingDate) + noClassString;
            }
        }

        /// <summary>
        /// Same as DisplayName, with the addition of the meeting start time.
        /// </summary>
        public string DisplayNameWithTime
        {
            get
            {
                if (IsNoClass)
                {
                    return DisplayName;
                }
                else
                {
                    return DisplayName + "...." + StartTimeLocal.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// DisplayName prefixed by the class section name.
        /// </summary>
        public string LongDisplayName
        {
            get
            {
                return ClassSectionName + "........" + DisplayName;
            }
        }

        /// <summary>
        /// Class meeting start time, in user local time.
        /// </summary>
        public DateTime StartTimeLocal
        {
            get
            {
                return DomainUtilities.ConvertUtcToLocalTime(StartTime);
            }
            set
            {
                StartTime = DomainUtilities.ConvertLocalTimeToUtc(value);
            }
        }

        /// <summary>
        /// Class meeting end time, in user local time.
        /// </summary>
        public DateTime EndTimeLocal
        {
            get
            {
                return DomainUtilities.ConvertUtcToLocalTime(EndTime);
            }
            set
            {
                EndTime = DomainUtilities.ConvertLocalTimeToUtc(value);
            }
        }

        //These are not set when the object is constructed.  They are set in the Service
        public int ClassNumber { get; set; }
        public int ClassCount { get; set; }
        public int WeekNumber { get; set; }
        public bool IsNextClass { get; set; }
        public bool IsLessonUseSelected { get; set; }
        public bool IsALessonSelected { get; set; }

        //These are CSS constants and other display related values
        public string IsUseSelectedEnabledClass
        {
            get{ return DomainWebUtilities.EnabledClass(IsLessonUseSelected); }
        }

        public string UpArrowClass
        {
            get
            {
                return DomainWebUtilities.UpArrowClass(IsLessonUseSelected);
            }
        }

        public string DownArrowClass
        {
            get
            {
                return DomainWebUtilities.DownArrowClass(IsLessonUseSelected);
            }
        }

        public string LeftArrowClass
        {
            get
            {
                return DomainWebUtilities.LeftArrowClass(IsLessonUseSelected);
            }
        }

        public string RightArrowClass
        {
            get
            {
                return DomainWebUtilities.RightArrowClass(IsALessonSelected && !IsNoClass);
            }
        }

        public string UpSmallArrowClass
        {
            get
            {
                return DomainWebUtilities.UpSmallArrowClass(IsLessonUseSelected);
            }
        }

        public string DownSmallArrowClass
        {
            get
            {
                return DomainWebUtilities.DownSmallArrowClass(IsLessonUseSelected);
            }
        }

        public string WeekNumberString
        {
            get
            {
                if (IsBeginningOfWeek )
                {
                    return "Week " + WeekNumber.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public string ClassNumberString
        {
            get {return "Class " + ClassNumber.ToString(); }
        }

        public string ClassesToGoString
        {
            get { return (ClassCount - ClassNumber).ToString() + " to go";}
        }

        public String DisplayNameClass
        {
            get {return DomainWebUtilities.MeetingDisplayNameClass(IsBeginningOfWeek);}
        }

        public string ClassNumberDisplayClass
        {
            get{ return DomainWebUtilities.ClassNumberDisplayClass(IsBeginningOfWeek);}
        }

        public string ClassMeetingBorderCSS
        {
            get{return DomainWebUtilities.ClassMeetingBorderClass(IsNextClass);}
        }

        /// <summary>
        /// Not set from Db, used in Classroom View and set in DailyPlanning.
        /// </summary>
        public bool IsMeetingSelected { get; set; }

    }//End Class
}
