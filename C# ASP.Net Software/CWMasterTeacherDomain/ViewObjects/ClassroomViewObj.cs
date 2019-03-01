using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class ClassroomViewObj
    {
        public bool ShowReferenceDocs { get; set; }

        public string DateLabelString { get; set; }

        public bool ShowAllDates { get; set; }

        public List<DateViewObj> PossibleDatesObjList { get; set; }

        public bool UserIsEditor
        {
            get { return CurrentUserObj.IsMasterEditor; }
        }

        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }


        public ClassMeetingDomainObjBasic SelectedClassMeetingObj { get; set; }
        public Guid SelectedClassMeetingId
        {
            get { return SelectedClassMeetingObj == null ? Guid.Empty : SelectedClassMeetingObj.Id; }
        }

        public ClassSectionDomainObj ClassSectionObj { get; set; }

        public Guid ClassSectionId
        {
            get { return SelectedClassMeetingObj == null ? Guid.Empty : SelectedClassMeetingObj.ClassSectionId; }
        }
        public Guid SelectedClassSectionId { get; set; }

        public List<ClassMeetingDomainObj> ClassMeetingObjList
        {
            get { return (ClassSectionObj ==null || ClassSectionObj.ClassMeetingList == null) ? 
                                        new List <ClassMeetingDomainObj>() : ClassSectionObj.ClassMeetingList; }
        }

        public string SelectedClassSectionName
        {
            get { return ClassSectionObj == null ? "" : ClassSectionObj.DisplayName; }
        }

        /// <summary>
        /// List of class sections, with only basic information.
        /// </summary>
        public List<ClassSectionDomainObjBasic> ClassSectionDomainObjBasicList { get; set; }

        public SelectList ClassSectionSelectList
        {
            get { return new SelectList(ClassSectionDomainObjBasicList, "Id", "Name");}
        }

        public Guid SelectedTermId { get; set; }

        public IEnumerable<SelectListItem> TermSelectList { get; set; }

        public DateTime SelectedDate { get; set; }

        public string LessonPlanTypeName { get; set; }

        public UserDomainObj CurrentUserObj {get; set;}

        public bool DoShowStudentManager { get; set; }

        public bool ShowAllTerms { get; set; }

        public int AwaitingApprovalCount
        {
            get { return ClassSectionObj == null ? 0 : ClassSectionObj.AwaitingApprovalCount; }
        }

        public bool DoAllowEditing
        {
            get { return ClassSectionObj == null ? false : ClassSectionObj.DoAllowEditing; }
        }







        //This is a dummy variable used to fool Razor
        public Guid SelectedLessonUseId { get; set; }

        
    }
}
