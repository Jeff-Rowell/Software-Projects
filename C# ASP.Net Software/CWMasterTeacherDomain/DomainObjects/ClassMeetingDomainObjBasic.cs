using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
   
    public class ClassMeetingDomainObjBasic
    {
        public Guid Id { get; set; }

        public string ClassSectionName { get; set; }

        public Guid ClassSectionId { get; set; }

        public DateTime MeetingDate { get; set; }

        public string MeetingDateString
        {
            get { return DomainWebUtilities.DateTime_ToLongDateString(MeetingDate); }
        }

        public bool IsSelected { get; set; }

        public bool IsReadyToTeach { get; set; }
        
        public string DisplayClass
        {
            get
            {
                return DomainWebUtilities.ListSelectedClass(IsSelected, true) + " " +
                    DomainWebUtilities.ListCurrentClass(IsCurrent);
            }
        }

        public bool IsCurrent { get; set; }
    }
}
