using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CWMasterTeacher3
{
    /// <summary>
    /// A view model that contains properties needed in the controller to create this view.
    /// </summary>
    public class ClassMeetingAdminReturnModel
    {
        ////pass-through parameters
        //public bool AddSingleClassMeeting { get; set; }
        //public bool ShowEditNoClass { get; set; }

        //Not sure about this one
        [Display(Name = "Single Class Meeting Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime SingleMeetingDate { get; set; }

        //public int SelectedUserId { get; set; }
        //public int SelectedTermId { get; set; }

        //[Display(Name = "Users")]
        //public IEnumerable<SelectListItem> UserSelectList { get; set; }

        //public IEnumerable<SelectListItem> TermSelectList { get; set; }

        //public IEnumerable<SelectListItem> CourseSelectList { get; set; }

        //[Required(ErrorMessage = "You must select a course.")]
        //public int SelectedCourseId { get; set; }

        [DisplayName("Start Time")]
        [RegularExpression(@"^([1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Time must be hh:mm AM/PM.")]
        public string StartTimeLocal { get; set; }

        [DisplayName("End Time")]
        [RegularExpression(@"^([1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Time must be hh:mm AM/PM.")]
        public string EndTimeLocal { get; set; }

        //public IEnumerable<SelectListItem> ClassMeetingSelectList { get; set; }

        //public Guid SelectedClassMeetingId { get; set; }

        //public IEnumerable<SelectListItem> ClassSectionSelectList { get; set; }

        public Guid SelectedClassSectionId { get; set; }

        //[Display(Name = "Class Section Name")]
        //[Required(ErrorMessage = "This field must be at least 5 characters.")]
        //[MinLength(5)]
        //public string SelectedClassSectionName { get; set; }

        //public bool UserIsEditor { get; set; }

        //public bool UserIsAdmin { get; set; }

        //public bool SelectedClassMeetingHasNoClass { get; set; }

        //public string NoClassMenuText { get; set; }

        //[Display(Name = "Comment (e.g. 'Memorial Day')")]
        //[Required(ErrorMessage = "This field must be at least 5 characters.")]
        //[MinLength(5)]
        //public string NoClassComment { get; set; }

    }
}