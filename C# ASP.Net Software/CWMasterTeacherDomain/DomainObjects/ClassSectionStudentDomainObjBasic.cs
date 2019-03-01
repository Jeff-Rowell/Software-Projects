using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class ClassSectionStudentDomainObjBasic
    {
        public Guid Id { get; set; }
        
        public StudentUserDomainObjBasic StudentUserObj { get; set; }
        public ClassSectionDomainObjBasic ClassSectionObj { get; set; }

        [Display(Name = "Has been approved")]
        public bool HasBeenApproved { get; set; }

        [Display(Name = "Has been denied")]
        public bool HasBeenDenied { get; set; }

        public Guid StudentUserId
        {
            get { return StudentUserObj == null ? Guid.Empty : StudentUserObj.Id; }
        }

        public string StudentFullName
        {
            get { return StudentUserObj == null ? " " : StudentUserObj.FullName; }
        }

        public Guid ClassSectionId
        {
            get { return ClassSectionObj == null ? Guid.Empty : ClassSectionObj.Id; }
        }

        public string ClassSectionDisplayName
        {
            get { return ClassSectionObj == null ? " " : ClassSectionObj.DisplayName; }
        }

        public string ClassSectionName
        {
            get { return ClassSectionObj == null ? " " : ClassSectionObj.Name; }
        }



    }
}
