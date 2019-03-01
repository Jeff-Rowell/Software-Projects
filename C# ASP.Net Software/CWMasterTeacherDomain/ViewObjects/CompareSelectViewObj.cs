using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class CompareSelectViewObj
    {


        public CourseDomainObj CourseObj { get; set; }
        public Guid SelectedLessonId { get; set; }
        public bool IsDisplayModeNarrative { get; set; }
        public bool IsDisplayModeLessonPlan { get; set; }
        public bool IsDisplayModeDocuments { get; set; }
        public Guid SelectedMetaCourseId { get; set; }
        public UserDomainObj CurrentUserObj { get; set; }
        public int ComparisonModeInt { get; set; }

        public List<MetaCourseDomainObjBasic> MetaCourses {get; set; }
        public List<TermDomainObj> CoursesByTerm { get; set; }

        public bool DoLockEditableLesson { get; set; } //We only have this so we can pass it back.

        public Guid ReferenceCourseId { get; set; }

        public Guid ReferenceEditableLessonId { get; set; }

        public SelectList MetaCourseSelectList
        {
            get { return new SelectList(MetaCourses, "Id", "Name"); }
        }

        public bool UserIsApplicationAdmin
        {
            get
            {
                return CurrentUserObj == null ? false : CurrentUserObj.IsApplicationAdmin;
            }
        }
        public bool UserIsWorkingGroupAdmin
        {
            get
            {
                return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin;
            }
        }

        private bool CourseIsMaster
        {
            get { return CourseObj != null && CourseObj.IsMaster; }
        }

        public string TitleCSS
        {
            get
            {
                return DomainWebUtilities.CourseLessonTitleClass(CourseIsMaster, false);
            }
        }

        public string CourseDisplayName
        {
            get { return CourseObj == null ? "" : CourseObj.DisplayName; }
        }

        public bool DoShowCourseSelect
        {
            get { return CoursesByTerm != null && CoursesByTerm.Count > 0
                    && (DomainUtilities.CompModeIsImportContent(ComparisonModeInt) || DomainUtilities.CompModeIsImportLesson(ComparisonModeInt)); }
        }

        public bool DoShowMetaCourseSelect
        {
            get { return DomainUtilities.CompModeIsImportContent(ComparisonModeInt) || DomainUtilities.CompModeIsImportLesson(ComparisonModeInt); }
        }

        public string CourseListHeading
        {
            get
            {
                if (DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt))
                {
                    return "Changes in " + CourseDisplayName;
                }
                else if (DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt))
                {
                    return "Changes in Group Lessons";
                }
                else
                {
                    return "Import from " + CourseDisplayName;
                }
            }
        }
    }
}
