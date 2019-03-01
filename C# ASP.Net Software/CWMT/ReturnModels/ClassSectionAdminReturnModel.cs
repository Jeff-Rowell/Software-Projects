using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMasterTeacher3
{
    public class ClassSectionAdminReturnModel
    {

        [DisplayName("Meets Sunday")]
        public bool MeetsSunday { get; set; }

        [DisplayName("Meets Monday")]
        public bool MeetsMonday { get; set; }

        [DisplayName("Meets Tuesday")]
        public bool MeetsTuesday { get; set; }

        [DisplayName("Meets Wednesday")]
        public bool MeetsWednesday { get; set; }

        [DisplayName("Meets Thursday")]
        public bool MeetsThursday { get; set; }


        [DisplayName("Meets Friday")]
        public bool MeetsFriday { get; set; }


        [DisplayName("Meets Saturday")]
        public bool MeetsSaturday { get; set; }


        [Display(Name = "Users")]
        public IEnumerable<SelectListItem> UserSelectList { get; set; }


        public Guid SelectedUserId { get; set; }


        public IEnumerable<SelectListItem> TermSelectList { get; set; }

        public Guid SelectedTermId { get; set; }


        public IEnumerable<SelectListItem> CourseSelectList { get; set; }


        [Required(ErrorMessage = "You must select a course.")]
        public Guid SelectedCourseId { get; set; }



        [DisplayName("Start Time")]
        [RegularExpression(@"^([1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Time must be hh:mm AM/PM.")]
        public string StartTimeLocal { get; set; }


        [DisplayName("End Time")]
        [RegularExpression(@"^([1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Time must be hh:mm AM/PM.")]
        public string EndTimeLocal { get; set; }

        public IEnumerable<SelectListItem> ClassMeetingSelectList { get; set; }

        public Guid SelectedClassMeetingId { get; set; }

        public bool AddSingleClassMeeting { get; set; }

        public IEnumerable<SelectListItem> ClassSectionSelectList { get; set; }

        public Guid SelectedClassSectionId { get; set; }

        [Display(Name = "Class Section Name")]
        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string SelectedClassSectionName { get; set; }

        public bool UserIsEditor { get; set; }

        public bool UserIsAdmin { get; set; }

        [Display(Name = "Single Class Meeting Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime SingleMeetingDate { get; set; }

        public bool ClassMeetingIsHoliday { get; set; }

    }//End class
}