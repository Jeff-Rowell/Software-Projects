using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class LessonManagerViewObj
    {
        public CourseDomainObj CourseObj { get; set; }
        public LessonDomainObj LessonObj { get; set; }
        public LessonDomainObj ParentLessonObj { get; set; }
        public UserDomainObj CurrentUserObj { get; set; }

        public Guid SelectedLessonId {get; set; }
        public string LessonName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsArchive { get; set; }

        [Display(Name = "Show Hidden")]
        public bool DoShowHiddenLessons
        {
            get { return CourseObj == null ? false : CourseObj.DoShowHiddenLessons; }
        }
        public string CourseName
        {
            get { return CourseObj == null ? "" : CourseObj.DisplayName; }
        }
        public Guid CourseId
        {
            get { return CourseObj == null ? Guid.Empty : CourseObj.Id; }
        }
        public Guid ParentId { get; set; }
        public IEnumerable<SelectListItem> SequenceNumberSelectList { get; set; }
        public bool ParentIsCourse { get; set; }
        public string CourseLabelClass
        {
            get {  return CourseObj == null ? null : DomainWebUtilities.CourseLessonTitleClass(CourseObj.IsMaster, false); }
        }
        public string CourseSmallLabelClass
        {
            get { return CourseObj == null ? null : DomainWebUtilities.CourseLessonTitleClass(CourseObj.IsMaster, true); }
        }


        public bool SelectedLessonIsFolder
        {
            get { return LessonObj != null && LessonObj.IsFolder && !IsCreate; }
        }

        public string ParentName { get; set; }
        public bool ShowSelectLocation { get; set; }
        public string EditorHeading
        {
            get
            {
                if (IsMove && LessonObj != null)
                {
                    return "Move Lesson: " + LessonObj.DisplayName;
                }
                else if (IsEdit && LessonObj != null)
                {
                    return "Edit Lesson: " + LessonObj.DisplayName;
                }
                else if (LessonObj != null && IsCreate)
                {
                    return "Create Lesson";
                }
                else
                {
                    return "_";
                }
            }
        }
        public bool IsMove { get; set; }
        public bool UserIsEditor
        {
            get { return CurrentUserObj != null && CurrentUserObj.IsMasterEditor; }
        }
        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj != null && CurrentUserObj.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

        public bool ShowConfirmDelete { get; set; }

        public string DeletionMessage { get; set; }

        public int SequenceNumber
        {
            get
            {
                if(LessonObj != null && !IsCreate && LessonObj.SequenceNumber != null)
                {
                    return LessonObj.SequenceNumber.Value;
                }
                else if (SequenceNumberSelectList != null)
                {
                    return SequenceNumberSelectList.Count();
                }
                else
                {
                    return 0;
                }
            }
        }
        public string HideLessonLinkText
        {
            get
            {
                if(LessonObj != null)
                {
                    return LessonObj.IsHidden ? "Unhide Lesson" : "Hide Lesson";
                }
                else
                {
                    return "_";
                }

            }
        }

        public bool SelectedLessonIsHidden
        {
            get { return LessonObj == null ? false : LessonObj.IsHidden; }
        }

        public bool ShowLessonCollapseLink
        {
            get { return LessonObj != null; }
        }
        public string LessonCollapseButtonCSS
        {
            get { return DomainWebUtilities.PlusMinusButtonClass(LessonObj == null || LessonObj.IsCollapsed); }
        }


    }//End Class
}
