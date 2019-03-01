using System;
using System.Collections.Generic;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class InstitutionDomainObj
    {
        public InstitutionDomainObj()
        {
            //internal int id;
            Courses = new HashSet<CourseDomainObj>();
            Terms = new HashSet<TermDomainObj>();
            Users = new HashSet<UserDomainObj>();
        }

        public Guid Id
        {
            get
            {
                return InstitutionId;
            }
        }

        public Guid InstitutionId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CourseDomainObj> Courses { get; set; }

        public virtual ICollection<TermDomainObj> Terms { get; set; }

        public virtual ICollection<UserDomainObj> Users { get; set; }
    }
}