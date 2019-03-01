using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    class ReferenceCalendarDomainObjBasic
    {
        /// <summary>
        /// The name of theReferenceCalendar.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique identifier of the ReferenceCalendar.
        /// </summary>
        public Guid Id { get; set; }
    }
}
