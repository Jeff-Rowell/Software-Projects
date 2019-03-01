using System;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    ///  Holds the rest of data and properties from MessageDomainObjBasic.
    /// </summary>
    public class MessageDomainObj
    {
        private MessageDomainObjBasic _messageBasic;
        private Guid? _threadParentId;
        private string _text;
        private string _subject;
        private DateTime _timeStamp;

        /// <summary>
        ///  Basic constructor that initializes the new instance of MessageDomainObj with the values of the provided arguments.
        /// </summary>
        /// <param name="messageBasic">The basic data from MessageDomainObjBasic class</param>
        /// <param name="threadParentId">The thread parent id of the message</param>
        /// <param name="text">The text of the message</param>
        /// <param name="subject">The subject of the message</param>
        /// <param name="timeStamp">The timestamp of the message</param>
        public MessageDomainObj(MessageDomainObjBasic messageBasic, Guid? threadParentId, string text, string subject, DateTime timeStamp)
        {
            _messageBasic = messageBasic;
            _threadParentId = threadParentId;
            _text = text;
            _subject = subject;
            _timeStamp = timeStamp;
        }

        /// <summary>
        ///  Read-only property representing the basic data from MessageDomainOBjBasic that associated with the instance of MessageDomainObj.
        /// </summary>
        public MessageDomainObjBasic MessageBasic
        {
            get { return _messageBasic; }
        }

        /// <summary>
        ///  Read-only property representing the thread parent id associated with the instance of MessageDomainObj.
        /// </summary>
        public Guid? ThreadParentId
        {
            get { return _threadParentId; }
        }

        /// <summary>
        ///  Read-only property representing the text associated with the instance of MessageDomainObj.
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        ///  Read-only property representing the subject associated with the instance of MessageDomainObj.
        /// </summary>
        public string Subject
        {
            get { return _subject; }
        }

        /// <summary>
        ///  Read-only property representing the timestamp associated with the instance of MessageDomainObj.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
        }
    }
}