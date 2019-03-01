
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class ClassSectionDomainObjBasic
    {
        /// <summary>
        /// The name of the ClassSection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique identifier of the ClassSection.
        /// </summary>
        public Guid Id { get; set; }

        public string CourseUserDisplayName { get; set; }

        public Guid CourseId { get; set; }

        /// <summary>
        /// The name to be displayed for this class section.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return Name + "_" + CourseUserDisplayName;
            }
        }

        public string ExtendedDisplayName
        {
            get { return DisplayName; }
        }

        public Guid? MirrorTargetClassSectionId { get; set; }
    }
}
