using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class StudentManagerViewObj
    {
        public List<ClassSectionStudentDomainObjBasic> StudentList { private get; set; }

        public List<ClassSectionStudentDomainObjBasic> StudentsWaitingForApproval
        {
            get { return StudentList == null || StudentList.Count == 0 ? new List<ClassSectionStudentDomainObjBasic>() : 
                                                    StudentList.Where(x => !x.HasBeenApproved && !x.HasBeenDenied).ToList(); }
        }

        public List<ClassSectionStudentDomainObjBasic> StudentsDenied
        {
            get
            {
                return StudentList == null || StudentList.Count == 0 ? new List<ClassSectionStudentDomainObjBasic>() :
                                                  StudentList.Where(x => x.HasBeenDenied).ToList();
            }
        }

        public List<ClassSectionStudentDomainObjBasic> CurrentStudents
        {
            get
            {
                return StudentList == null || StudentList.Count == 0 ? new List<ClassSectionStudentDomainObjBasic>() :
                                                  StudentList.Where(x => x.HasBeenApproved).ToList();
            }
        }

        public bool DoShowCurrentStudents
        {
            get { return CurrentStudents == null ? false : CurrentStudents.Count > 0; }
        }

        public bool DoShowWaitingForApproval
        {
            get { return StudentsWaitingForApproval == null ? false : StudentsWaitingForApproval.Count > 0; }
        }

        public bool DoShowDeniedStudents
        {
            get { return StudentsDenied == null ? false : StudentsDenied.Count > 0; }
        }

        public Guid ClassSectionId { get; set; }
        public Guid SelectedClassMeetingId { get; set; }


    }
}
