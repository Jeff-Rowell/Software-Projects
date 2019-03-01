using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class StudentUserDomainObj
    {

        public StudentUserDomainObjBasic StudentUserBasic { get; set; }


        public List<ClassSectionStudentDomainObjBasic> ClassSectionStudents { get; set; }
        public string EmailAddress { get; set; }

        public Guid Id
        {
            get { return StudentUserBasic == null ? Guid.Empty : StudentUserBasic.Id; }
        }

        public string FullName
        {
            get { return StudentUserBasic == null ? "" : StudentUserBasic.FullName; }
        }

        public string UserName { get; set; }
    }
}
