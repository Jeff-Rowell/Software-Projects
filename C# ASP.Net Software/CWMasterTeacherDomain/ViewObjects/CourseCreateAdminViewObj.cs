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
    /// A view object to store relevant information from the related Domain Object to be passed
    /// to the View Model.
    /// </summary>
    public class CourseCreateAdminViewObj
    {

        public CourseCreateAdminViewObj()
        {

        }

        public SelectList WorkingGroupSelectList { get; set; }

        public SelectList UserSelectList { get; set; }

        public SelectList TermSelectList { get; set; }

        public SelectList CourseSelectList { get; set; }

        public Guid CourseId { get; set; }

        public Guid WorkingGroupId { get; set; }

        public Guid UserId { get; set; }

        public Guid TermId { get; set; }

        public string Message { get; set; }

        public string NarrativeMessage { get; set; }

        public bool ShowDeleteCourse { get; set; }

        public bool ShowNewWorkingGroup { get; set; }

        public bool ShowDeleteWorkingGroup { get; set; }

        public bool ShowAllCourses { get; set; }

        public bool IsCourseDeletable { get; set; }

        public bool ShowRenameWorkingGroup { get; set; }

        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string WorkingGroupName { get; set; }

        public string CourseName { get; set; }

        public string CourseDisplayName { get; set; }

        public UserDomainObj CurrentUser { private get; set; }

        public bool UserIsApplicationAdmin { get { return CurrentUser == null ? false : CurrentUser.IsApplicationAdmin; } }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUser == null ? false : CurrentUser.IsWorkingGroupAdmin; }
        }

        public bool UserIsEditor { get { return CurrentUser == null ? false : CurrentUser.IsMasterEditor; } }
    }
}
