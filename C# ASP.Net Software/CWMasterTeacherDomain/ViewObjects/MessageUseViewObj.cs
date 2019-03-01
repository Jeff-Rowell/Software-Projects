using System;
using System.Collections.Generic;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class MessageUseViewObj
    {
        public MessageUseDomainObj MessageUseDomainObj { get; private set; }
        private List<MessageUseViewObj> _viewObjChildren;
        private DateTime _storageReferenceTime;
        private int _threadParentId;
        private bool _isNew;
        private bool _isActive;
        private bool _isStored;
        private bool _isArchived;
        private bool _isActiveStored;
        private bool _isImportant;
        private bool _isForEndOfSemRev;

        /// <summary>
        /// The constructor for the MessageUseViewObj.
        /// This contains the fields required for the MessageBoardViewObj.
        /// </summary>
        public MessageUseViewObj(MessageUseDomainObj messageUseDomainObj, List<MessageUseViewObj> childViewObjects)
        {
            MessageUseDomainObj = messageUseDomainObj;
            _threadParentId =
                getThreadParentId(messageUseDomainObj.MessageDomainObj.ThreadParentId);
            _viewObjChildren = new List<MessageUseViewObj>();
            foreach (MessageUseViewObj child in childViewObjects)
            {
                _viewObjChildren.Add(child);
            }
            _storageReferenceTime = messageUseDomainObj.StorageReferenceTime == null ?
                default(DateTime) : messageUseDomainObj.StorageReferenceTime.Value;

            _isStored = messageUseDomainObj.IsStored;
            _isArchived = messageUseDomainObj.IsArchived;
            _isActiveStored = messageUseDomainObj.IsActiveStored;
            _isNew = messageUseDomainObj.IsNew;
            _isImportant = messageUseDomainObj.IsImportant;
            _isForEndOfSemRev = messageUseDomainObj.IsForEndOfSemRev;
            _isActive = !_isStored && !_isArchived && !_isActiveStored; 
        }

        private int getThreadParentId(int? parentId)
        {
            return (parentId != null && parentId > 0) ?
                parentId.Value : -1;
        }

        public List<MessageUseViewObj> getChildMessageUses
        {
            get
            {
                return _viewObjChildren;
            }
        }

        public bool IsSelected { get; set; }

        public int ThreadParentId
        {
            get
            {
                return _threadParentId;
            }
        }

        public bool IsNew
        {
            get
            {
                return _isNew;
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public bool IsStored
        {
            get
            {
                return _isStored;
            }
        }

        public bool IsActiveStored
        {
            get
            {
                return _isActiveStored;
            }
        }

        public bool IsArchived
        {
            get
            {
                return _isArchived;
            }
        }

        public bool IsImportant
        {
            get
            {
                return _isImportant;
            }
        }

        public bool IsForEndOfSemRev
        {
            get
            {
                return _isForEndOfSemRev;
            }
        }


        /// <summary>
        /// Returns true if this, or any of its children, are new.
        /// </summary>
        public bool IsThisOrChildrenNew
        {
            get
            {
                if (IsNew)
                {
                    return true;
                }
                // If _threadParentId <= 0, this is a parent.
                else if (_threadParentId <= 0)
                {
                    foreach (var x in _viewObjChildren)
                    {
                        if (x.IsNew)
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
        public bool IsThisOrChildrenLive
        {
            get
            {
                if (!IsStored && !IsArchived)
                {
                    return true;
                }
                // If _threadParentId <= 0, then this is a parent.
                else if (_threadParentId <= 0)
                {
                    foreach (var x in _viewObjChildren)
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
        /// Returns true if this, or any of its children, are flagged as important.
        /// </summary>
        public bool IsThisOrChildrenImportant
        {
            get
            {
                if (_isImportant)
                {
                    return true;
                }
                // If _threadParentId <= 0, then this is a parent.
                else if (_threadParentId <= 0)
                {
                    foreach (var x in _viewObjChildren)
                    {
                        if (x.IsImportant)
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
                if (_isStored && !_isArchived)
                {
                    return true;
                }
                // If _threadParentId <= 0, this is a parent.
                else if (_threadParentId <= 0)
                {
                    foreach (var x in _viewObjChildren)
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
        /// Returns true if this, or any of its children, are flagged for end of semester revision.
        /// </summary>
        public bool IsThisOrChildrenForEndOfSemRev
        {
            get
            {
                if (_isForEndOfSemRev)
                {
                    return true;
                }
                // If _threadParentId <= 0, this is a parent.
                else if (_threadParentId <= 0)
                {
                    foreach (var x in _viewObjChildren)
                    {
                        if (x.IsForEndOfSemRev)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        private string MessageAndDocClass
        {
            get
            {
                return DomainWebUtilities.GetMessageAndDocImageCSS(IsImportant, IsNew, IsActive,
                                                                    IsActiveStored, IsStored, false, IsArchived);
            }
        }

        private string SelectedClass
        {
            get
            {
                return DomainWebUtilities.ListSelectedClass(true, false);
            }
        }

        public string DisplayClass
        {
            get { return MessageAndDocClass + " " + SelectedClass; }
        }
    }//End Class

}
