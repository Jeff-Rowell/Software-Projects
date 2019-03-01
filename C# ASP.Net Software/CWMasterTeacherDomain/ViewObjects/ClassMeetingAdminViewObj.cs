using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CWMasterTeacherDomain.ViewObjects
{
    /// <summary>
    /// Contains the properties and logic needed to build a ClassMeetingViewModel 
    /// </summary>
    public class ClassMeetingAdminViewObj
    {
        public ClassMeetingDomainObj classMeetingDomainObj { get; set; }

        [Display(Name = "Users")]
        public IEnumerable<SelectListItem> UserSelectList { get; set; }
        public IEnumerable<SelectListItem> TermSelectList { get; set; }
        public IEnumerable<SelectListItem> CourseSelectList { get; set; }
        public IEnumerable<SelectListItem> ClassMeetingSelectList { get; set; }
        public Guid SelectedClassSectionId { get; set; }
        public Guid SelectedUserId { get; set; }
        public Guid SelectedTermId { get; set; }

        public IEnumerable<SelectListItem> ClassSectionSelectList { get; set; }

        public bool SelectedClassMeetingHasNoClass { get; set; }
        public Guid SelectedClassMeetingId { get; set; }

        public UserDomainObj CurrentUserObj { get; set; }

        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsApplicationAdmin; }
        }
        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

        public bool UserIsEditor
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsMasterEditor; }
        }

        /// <summary>
        /// Empty constructor creates a blank ClassMeetingViewObject
        /// </summary>
        public ClassMeetingAdminViewObj()
        {

        }

        public List<CourseDomainObjBasic> PossibleMirrorCourses { private get; set; }
        public SelectList MirrorCourseSelectList
        {
            get { return PossibleMirrorCourses == null ? new SelectList(new List<CourseDomainObjBasic>(), "Id", "DisplayName") : new SelectList(PossibleMirrorCourses, "Id", "DisplayName"); }
        }
        public Guid MirrorCourseId { get; set; }
        public string MirrorCourseName { get; set; }



        /// <summary>
        /// Sets the StartTimeLocal and EndTimeLocal properties based on a class meeting domain object.
        /// </summary>
        /// <param name="classMeeting">A list of class meeting domain objects</param>
        public void setStartAndEndTime(ClassMeetingDomainObj classMeeting)
        {
            if (classMeeting == null)
            {
                StartTimeLocal = new DateTime().ToString("t");
                EndTimeLocal = new DateTime().ToString("t");
            }
            else
            {
                StartTimeLocal = DomainWebUtilities.DateTime_ToTimeString(classMeeting.StartTimeLocal);
                EndTimeLocal = DomainWebUtilities.DateTime_ToTimeString(classMeeting.EndTimeLocal);
            }
        }

        /// <summary>
        /// Takes a list of course domain basic objects and creates a selectList from the 
        /// "Id" and "DisplayName" properties and assigns the generated list to a field.
        /// </summary>
        /// <param name="courseList">A list of course domain objects</param>
        public void generateCourseSelectList(List<CourseDomainObjBasic> courseList)
        {
            CourseSelectList = new SelectList(courseList, "Id", "DisplayName");
        }

        /// <summary>
        /// Generates select lists from user domain objects and term basic domain objects.
        /// </summary>
        /// <param name="userList">A list of user domain objects</param>
        /// <param name="termList">A lsit of term domain objects</param>
        public void genUserAndTermSelectLists(List<UserDomainObjBasic> userList, 
            List<TermDomainObj> termList)
        {
            UserSelectList = new SelectList(userList, "Id", "DisplayName");
            TermSelectList = new SelectList(termList, "Id", "Name");
        }

        /// <summary>
        /// Takes a list of class meeting domain basic objects and creates a selectList from the 
        /// "Id" and "Name" properties and assigns the generated list to a field.
        /// </summary>
        /// <param name="classList">A list of class meeting domain objects</param>
        public void genClassMeetingSelectList(List<ClassMeetingDomainObj> classList)
        {
            if (classList == null)
            {
                ClassMeetingSelectList = Enumerable.Empty<SelectListItem>();
            }
            else
            {
                ClassMeetingSelectList = new SelectList(classList, "Id", "DisplayName");
            }
        }

        /// <summary>
        /// Takes a list of class section domain basic objects and creates a selectList from the 
        /// "Id" and "Name" properties and assigns the generated list to a field.
        /// </summary>
        /// <param name="classList">A list of class section domain objects</param>
        public void genClassSectionSelectList(List<ClassSectionDomainObjBasic> classList)
        {
            if (classList == null)
            {
                ClassSectionSelectList = Enumerable.Empty<SelectListItem>();
            }
            else
            {
                ClassSectionSelectList = new SelectList(classList, "Id", "Name");
            }
        }

        public string NoClassMenuText
        {
            get
            {
                if (SelectedClassMeetingHasNoClass)
                {
                    return "Remove Holiday";
                }
                else
                {
                    return "Mark as Holiday";
                }
            }
        }
        //pass-through parameters
        public bool AddSingleClassMeeting { get; set; }
        public bool ShowEditNoClass { get; set; }
        public bool ShowRenameClassSection { get; set; }
        public bool ShowCreateMirrorClassSection { get; set; }

        //Not sure about this one
        [Display(Name = "Single Class Meeting Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime SingleMeetingDate { get; set; }


        [Required(ErrorMessage = "You must select a course.")]
        public Guid SelectedCourseId { get; set; }

        [DisplayName("Start Time")]
        [RegularExpression(@"^([1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Time must be hh:mm AM/PM.")]
        public string StartTimeLocal { get; set; }

        [DisplayName("End Time")]
        [RegularExpression(@"^([1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Time must be hh:mm AM/PM.")]
        public string EndTimeLocal { get; set; }


        [Display(Name = "Class Section Name")]
        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string SelectedClassSectionName { get; set; }

        [Display(Name = "Comment (e.g. 'Memorial Day')")]
        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string NoClassComment { get; set; }

    }
}
