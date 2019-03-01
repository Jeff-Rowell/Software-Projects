using System;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    ///  Holds the basic data required to identify a MessageDomainObject.
    /// </summary>
    public class MessageDomainObjBasic
    {


        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="messageId">The id of the message</param>
        /// <param name="subject"> The subject of the message</param>
        /// <param name="userDisplayName"> The display name of the associated user</param>
        /// <param name="timeStamp"> The time stamp of the message</param>
        public MessageDomainObjBasic(Guid messageId, string subject, string userDisplayName, DateTime timeStamp)
        {
            Id = messageId;
            Subject = subject;
            UserDisplayName = userDisplayName;
            TimeStamp = timeStamp;
        }

        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime TimeStamp { get; set; }

        /// <summary>
        ///  Read-only property representing the display name of the message associated with the instance 
        ///  of MessageDomainObjBasic.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return Subject + "  (" + UserDisplayName + "__" + TimeStamp.ToString() + ")";
            }
        }
    }
}
