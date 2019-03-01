using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class WorkingRelationshipDomainObj
    {
        public WorkingRelationshipDomainObjBasic BasicObj { get; set; }

        public Guid Id
        {
            get { return BasicObj.Id; }
        }

        public string Name
        {
            get { return BasicObj.Name; }
        }



        public bool FollowUserNarrative { get; set; }

        public int UserNarrativeSequenceNumber { get; set; }

        public virtual CourseDomainObj TargetCourse { get; set; }

        public Guid TargetCourseId
        {
            get { return TargetCourse == null ? Guid.Empty : TargetCourse.Id; }
        }

        public string TargetCourseDisplayName { get; set; }

        public string TargetUserDisplayName
        {
            get { return User == null ? "" : User.DisplayName; }
        }


        public virtual UserDomainObj User { get; set; }

        public Guid UserId
        {
            get { return User == null ? Guid.Empty : User.Id; }
        }

        


    }
}
