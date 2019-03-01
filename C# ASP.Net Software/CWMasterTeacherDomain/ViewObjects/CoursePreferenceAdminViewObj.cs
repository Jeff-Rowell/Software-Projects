using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class CoursePreferenceAdminViewObj
    {
        public Guid CoursePreferenceId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Show notifications of changes to Narrative.")]
        public bool DoShowNarrativeNotifications { get; set; }


        [Display(Name = "Show notifications of changes to Lesson Notes.")]
        public bool DoShowLessonPlanNotifications { get; set; }

        [Display(Name = "Show notifications of changes to Documents.")]
        public bool DoShowDocumentNotifications { get; set; }

        public Guid CourseId { get; set; }

        public string Message { get; set; }

        public UserDomainObj CurrentUserObj { get; set; }

        public bool UserIsEditor
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsMasterEditor; }
        }

        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

    }
}
