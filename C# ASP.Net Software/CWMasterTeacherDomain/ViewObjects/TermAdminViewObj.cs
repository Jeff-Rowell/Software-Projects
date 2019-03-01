using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CWMasterTeacherDomain.ViewObjects
{
    /// <summary>
    /// Contains logic in order to build a TermViewObj.
    /// </summary>
    public class TermAdminViewObj
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public TermAdminViewObj()
        {

        }

        public TermDomainObj TermObj { get; set; }

        [Display(Name = "Term Name")]
        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string Name
        {
            get { return TermObj == null ? "" : TermObj.Name; }
        }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StartDate
        {
            get { return TermObj == null ? new DateTime() : TermObj.StartDate; }
        }


        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime EndDate
        {
            get { return TermObj == null ? new DateTime() : TermObj.EndDate; }
        }

        public Guid SelectedTermId
        {
            get { return TermObj == null ? Guid.Empty : TermObj.Id; }
        }

        public bool SelectedTermIsCurrent
        {
            get { return TermObj == null ? false : TermObj.IsCurrent; }
        }

        [Display(Name = "Date of Holiday")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "You must enter a Date for the Holiday")]
        public DateTime AddThisHolidayDate { get; set; }

        [Display(Name = "Holiday Name")]
        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string HolidayName { get; set; }

        public Guid SelectedHolidayId { get; set; }

        [Display(Name = "Holidays")]
        public SelectList HolidaySelectList { get; set; }

        public SelectList TermSelectList { get; set; }


        public string SubmitButtonText { get; set; }

        [Display(Name = "Delete Selected Holiday")]
        public bool ShowDeleteHoliday { get; set; }

        public UserDomainObj CurrentUser { private get; set; }
        public bool UserIsEditor
        {
            get { return CurrentUser == null ? false : CurrentUser.IsMasterEditor;  }
        }

        public bool UserIsApplicationAdmin
        {
            get { return CurrentUser == null ? false:  CurrentUser.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUser == null ? false : CurrentUser.IsWorkingGroupAdmin; }
        }







    }//End Class
}
