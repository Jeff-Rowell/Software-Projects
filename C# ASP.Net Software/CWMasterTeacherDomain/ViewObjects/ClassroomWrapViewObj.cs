using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class ClassroomWrapViewObj
    {
        public Guid ClassSectionId { get; set; }

        public ClassMeetingDomainObj SelectedClassMeetingObj {get; set;}

        public Guid SelectedClassMeetingId
        {
            get { return SelectedClassMeetingObj == null ? Guid.Empty : SelectedClassMeetingObj.Id; }
        }

        public List<ClassroomDocsLessonViewObj> DocsLessonList { get; set; }

        public bool DoShowReferenceDocs { get; set; }

        public string MessageEditType
        {
            get { return DomainWebUtilities.TextEditType.Message; }
        }

        public string NoteToSelfEditType
        {
            get { return DomainWebUtilities.TextEditType.MessageToSelf; }
        }

        public string AwaitingApprovalMessage
        {
            get { return "There are students waiting for approval."; }
        }

        public int AwaitingApprovalCount { get; set; }

        public bool DoShowStudentsAwaitingApproval
        {
            get { return AwaitingApprovalCount > 0; }
        }

        public string DateString
        {
            get { return SelectedClassMeetingObj == null ? "" : SelectedClassMeetingObj.MeetingDateString; }
        }

        public bool DoAllowEditing { get; set; }

        [DataType(DataType.MultilineText)]
        public string NotesForStudents
        {
            get { return SelectedClassMeetingObj == null ? "_" : SelectedClassMeetingObj.NotesForStudents; }
        }
    }
}
