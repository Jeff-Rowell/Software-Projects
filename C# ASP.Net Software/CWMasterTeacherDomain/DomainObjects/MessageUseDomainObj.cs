using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    ///  Holds the rest of data and properties from MessageUseDomainObjBasic.
    /// </summary>
    public class MessageUseDomainObj
    {
        private Guid _selectedMessageUseId;
        private Guid _currentUserId;
        private bool _isNew;
        /// <summary>
        ///  Basic constructor that initializes the new instance of MessageUseDomainObj with the values of the provided arguments.
        /// </summary>
        /// <param name="messageUseBasic">The basic data from MessageUseDomainObjBasic class</param>
        /// <param name="messageDomainObj">The message that is associated with the MessageUse</param>
        /// <param name="messageId">The id of the message associated with the MessageUse</param>
        /// <param name="lessonId">The lesson id of the message associated with the MessageUse</param>
        /// <param name="isNew">The message that is new of the associated MessageUse</param>
        /// <param name="storageReferenceTime">The storage reference time of the message associated with the MessageUse</param>
        /// <param name="isArchived">The message that is archived of the associated MessageUse</param>
        /// <param name="isForEndOfSemRev">The message that is for the end of the semester rev of the associated MessageUse</param>
        /// <param name="isImportant">The message that is important of the associated MessageUse</param>
        /// <param name="isStored">The message that is stored of the associated MessageUse</param>
        /// <param name="termStartDate">The start date of the term</param>
        /// 
        public MessageUseDomainObj(MessageUseDomainObjBasic messageUseBasic, Guid messageId, Guid lessonId, 
                                    bool isNew, DateTime? storageReferenceTime, bool isArchived, bool isForEndOfSemRev, bool isImportant,
                                        bool isStored, DateTime termStartDate, Guid? threadParentId, string text,
                                        Guid userId, bool showArchived)
        {
            MessageUseBasic = messageUseBasic;
            MessageId = messageId;
            LessonId = lessonId;
            IsNew = isNew;
            StorageReferenceTime = storageReferenceTime;
            IsArchived = isArchived;
            IsForEndOfSemRev = isForEndOfSemRev;
            IsImportant = isImportant;
            IsStored = isStored;
            TermStartDate = termStartDate;
            Text = text;
            UserId = userId;
            IsShowArchived = showArchived;

        }

        /// <summary>
        ///  Read-only property representing the basic data from MessageUseDomainOBjBasic that associated with the instance of MessageUseDomainObj.
        /// </summary>
        public MessageUseDomainObjBasic MessageUseBasic { get; set; }

        /// <summary>
        ///  Read-only property representing the message associated with the instance of MessageUseDomainObj.
        /// </summary>
        public MessageDomainObj MessageDomainObj { get; set; }

        /// <summary>
        ///  Read-only property representing the id of the message associated with the instance of MessageUseDomainObj.
        /// </summary>
        public Guid MessageId { get; set; }

        public List<MessageUseDomainObj > ChildMessageUseObjList { get; set; }

        /// <summary>
        ///  Read-only property representing the lesson ID of the message associated with the instance of MessageUseDomainObj.
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        ///  Read-only property representing the message that is new which is associated with the instance of MessageUseDomainObj.
        /// </summary>
        public bool IsNew
        {
            get { return _isNew && UserId != CurrentUserId; }
            set { _isNew = value; }
        }

        /// <summary>
        ///  Read-only property representing the storage reference time of the message associated with the instance of MessageUseDomainObj.
        /// </summary>
        public DateTime? StorageReferenceTime { get;  }

        /// <summary>
        ///  Read-only property representing the message that is archived which is associated with the instance of MessageUseDomainObj.
        /// </summary>
        public bool IsArchived { get; }

        /// <summary>
        ///  Read-only property representing the message that is for the end of the semester rev which is associated with the instance of MessageUseDomainObj.
        /// </summary>
        public bool IsForEndOfSemRev { get;  }

        /// <summary>
        ///  Read-only property representing the message that is important which is associated with the instance of MessageUseDomainObj.
        /// </summary>
        public bool IsImportant { get; }

        /// <summary>
        ///  Read-only property representing the message that is stored which is associated with the instance of MessageUseDomainObj.
        /// </summary>
        public bool IsStored { get; }

        /// <summary>
        /// A value stored in Db to allow archived messages to be shown when they have active replies.
        /// </summary>
        public bool IsShowArchived { get; }

        public bool IsActive
        {
            get { return !IsStored && !IsActiveStored && !IsArchived; }
        }


        /// <summary>
        ///  Read-only property representing the start date of the term associated with the instance of MessageUseDomainObj.
        /// </summary>
        public DateTime TermStartDate { get;}

        /// <summary>
        ///  Read-only property representing the message that is active stored which is associated with the instance of MessageUseDomainObj.
        /// </summary>
        public bool IsActiveStored
        {
            get
            {
                return IsStored && StorageReferenceTime != null && StorageReferenceTime < TermStartDate;
            }
        }

        /// <summary>
        /// This is the selected messageUse in the viewObj where this is displayed.  
        /// "I need to know whether the selected object is me so I can set my CSS"
        /// Value is passed down to children so setting it in thread parents will set it in whole tree.
        /// /// </summary>
        public Guid SelectedMessageUseId //used for setting CSS values
        {
            get
            {
                return _selectedMessageUseId;
            }
            set
            {
                _selectedMessageUseId = value;
                foreach (var xMessageUseObj in ChildMessageUseObjList)
                {
                    xMessageUseObj.SelectedMessageUseId = value;
                }
            }
        }

        public bool IsSelected
        {
            get { return Id == SelectedMessageUseId; }
        }


        public Guid Id
        {
            get { return MessageUseBasic.Id; }
        }

        public Guid? ThreadParentId
        {
            get { return MessageDomainObj.ThreadParentId; }
        }

        public string Text { get; set; }

        public string DisplayText
        {
            get { return "<h4>" + DisplayName + "</h4>" + Text; }
        }

        public string Subject
        {
            get { return MessageUseBasic.Subject; }
        }

        public string UserDisplayName
        {
            get { return MessageUseBasic.UserDisplayName; }
        }

        public Guid UserId { get; set; }

        public Guid CurrentUserId
        {
            get { return _currentUserId; }
            set
            {
                _currentUserId = value;
                foreach (var xMessageUseObj in ChildMessageUseObjList)
                {
                    xMessageUseObj.CurrentUserId = value;
                }
            }
        }

        public DateTime TimeStamp
        {
            get { return MessageUseBasic.TimeStamp; }
        }
 
        /// <summary>
        /// Returns true if this, or any of its children, are not stored and not archived.
        /// </summary>
        public bool IsThisOrChildrenLive
        {
            get
            {
                if (!IsStored && !IsArchived)
                {
                    return true;
                }
                // If _threadParentId <= 0, then this is a parent.
                else 
                {
                    foreach (var x in ChildMessageUseObjList)
                    {
                        if (!x.IsStored && !x.IsArchived)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns true if this, or any of its children, are not stored and not archived.
        /// </summary>
        public bool DisplayThisMessageUse
        {
            get
            {
                if (!IsArchived)
                {
                    return true;
                }
                // If _threadParentId <= 0, then this is a parent.
                else
                {
                    foreach (var x in ChildMessageUseObjList)
                    {
                        if (!x.IsArchived)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }


        /// <summary>
        /// Returns true if this, or any of its children, are stored but not archived.
        /// </summary>
        public bool IsThisOrChildrenStoredButNotArchived
        {
            get
            {
                if (IsStored && !IsArchived)
                {
                    return true;
                }
                else
                {
                    foreach (var x in ChildMessageUseObjList)
                    {
                        if (x.IsStored && !x.IsArchived)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns true if this, or any of its children, are not archived.
        /// </summary>
        public bool IsThisOrChildrenNotArchived
        {
            get
            {
                if (!IsArchived)
                {
                    return true;
                }
                else
                {
                    foreach (var x in ChildMessageUseObjList)
                    {
                        if (!x.IsArchived)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }


        public string DisplayName
        {
            get { return MessageUseBasic.DisplayName; }
        }

        private string MessageAndDocClass
        {
            get {return DomainWebUtilities.GetMessageAndDocImageCSS(IsImportant, IsNew, IsActive,
                                                                    IsActiveStored, IsStored, false, IsArchived);}
        }

        private string SelectedClass
        {
            get {return DomainWebUtilities.ListSelectedClass(IsSelected, false);}
        }

        public string DisplayClass
        {
            get { return MessageAndDocClass + " " + SelectedClass; }
        }





    }//End Class
}
