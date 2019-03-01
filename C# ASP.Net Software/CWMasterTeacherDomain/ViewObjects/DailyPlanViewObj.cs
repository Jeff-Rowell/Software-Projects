using System.Collections.Generic;
using CWMasterTeacherDomain.DomainObjects;
using System.Web.Mvc;
using System;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class DailyPlanViewObj
    {
        private List<ClassSectionDomainObjBasic> _classSectionDomainObjBasicList;
        /// <summary>
        /// Class section domain object.
        /// </summary>
        public ClassSectionDomainObj ClassSectionObj { get; set; }
        public CourseDomainObj CourseObj { get; set; }
        public UserDomainObj CurrentUserObj { get; set; }

        public List<ClassMeetingDomainObj> ClassMeetingObjList
        {
            get { return (ClassSectionObj == null || ClassSectionObj.ClassMeetingList == null) ? 
                new List <ClassMeetingDomainObj>() : ClassSectionObj.ClassMeetingList; }
        }

        /// <summary>
        /// List of class sections, with only basic information.
        /// </summary>
        public List<ClassSectionDomainObjBasic> ClassSectionDomainObjBasicList { get; set; }

        public IEnumerable<SelectListItem> ClassSectionSelectList
        {
            get
            {
                return new SelectList(ClassSectionDomainObjBasicList, "Id", "Name");
            }
        }

        /// <summary>
        /// Header for the class section for a reference calendar dropdown
        /// </summary>
        public string ReferenceCalDropdownHeading { get; set; }

        public SelectList ReferenceCalendarClassSectionSelectList { get; set; }

        public SelectList ReferenceCalendarPossibleClassSectionSelectList { get; set; }

        public Guid ReferenceClassSectionId { get; set; }

        /// <summary>
        /// The selected class section name.
        /// </summary>
        public string SelectedClassSectionName
        {
            get { return ClassSectionObj == null ? "" : ClassSectionObj.DisplayName; }
        }

        public Guid SelectedClassSectionId { get; set; }

        public LessonDomainObjBasic SelectedLessonObjBasic { get; set; }

        public Guid SelectedLessonId
        {
            get { return SelectedLessonObjBasic == null ? Guid.Empty : SelectedLessonObjBasic.Id; }
        }

        public string LessonCollapseLinkText
        {
            get { return SelectedLessonObjBasic == null || SelectedLessonObjBasic.IsCollapsed ? "Expand Lesson" : "Collapse Lesson"; }
        }

        public bool ShowLessonCollapseLink
        {
            get { return SelectedLessonObjBasic != null; }
        }

        public string LessonCollapseButtonCSS
        {
            get { return DomainWebUtilities.PlusMinusButtonClass(SelectedLessonObjBasic == null || SelectedLessonObjBasic.IsCollapsed); }
        }

        public Guid SelectedLessonUseId { get; set; }

        /// <summary>
        /// Is the current user and editor?
        /// </summary>
        public bool UserIsEditor
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsMasterEditor; }
        }

        /// <summary>
        /// Is the current user an admin?
        /// </summary>
        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get  { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid AddToReferencesClassSectionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedLessonUseSequencNumber { get; set; }

        /// <summary>
        /// The selected course Id.
        /// </summary>
        public Guid SelectedCourseId
        {
            get { return ClassSectionObj == null ? Guid.Empty : ClassSectionObj.CourseId; }
        }

        public string TermName
        {
            get { return ClassSectionObj == null ? "" : ClassSectionObj.TermName; }
        }

        public bool ShowAllTerms { get; set; }
        public bool ShowAddReferenceCalendar { get; set; }

        /// <summary>
        /// List of class meetings for reference calendar
        /// </summary>
        public IEnumerable<ClassMeetingDomainObj> ReferenceCalendarClassMeetingObjList { get; set; }

        /// <summary>
        /// What it says.
        /// </summary>
        public IEnumerable<LessonDomainObj> LessonObjsForTreeList
        {
            get { return (CourseObj == null) ? new List<LessonDomainObj>() : CourseObj.ContainerChildLessons; }
        }

        public bool DoAllowEditing
        {
            get { return ClassSectionObj == null ? false : ClassSectionObj.DoAllowEditing; }
        }
    }
}
