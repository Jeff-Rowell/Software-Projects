using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    ///  Holds the basic data required to identify a MessageUseDomainObject.
    /// </summary>
    public class MessageUseDomainObjBasic
    {

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="id">The id of the basic object</param>
        public MessageUseDomainObjBasic(Guid id, string subject, string userDisplayName, DateTime timestamp)
        {
            Id = id;
            Subject = subject;
            UserDisplayName = userDisplayName;
            TimeStamp = timestamp;
        }

        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime TimeStamp { get; set; }

        public string DisplayName
        {
            get
            {
                return Subject + "  (" + UserDisplayName + "__" + TimeStamp.ToString() + ")";
            }
        }
    }
}
