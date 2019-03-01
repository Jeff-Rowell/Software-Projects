using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class MessageBoardViewObj
    {

        /// <summary>
        /// The constructor for the MessageBoardViewObj.
        /// This contains the logic and most of the fields required by the View Model.
        /// </summary>
        public MessageBoardViewObj(Guid selectedLessonID, Guid selectedMessageUseId,
            MessageUseDomainObj selectedMessageUse, List<MessageUseDomainObj> ParentViewObjects)
        {
            
        }

        public MessageBoardViewObj()
        {

        }

        public MessageUseDomainObj SelectedMessageUseObj { get; set; }

        public bool IsDisplayImportant
        {
            get { return SelectedMessageUseObj == null ? false : SelectedMessageUseObj.IsImportant; }
        }

        public bool IsDisplayStored
        {
            get { return SelectedMessageUseObj == null ? false : SelectedMessageUseObj.IsStored; }
        }

        public bool IsDisplayArchived
        {
            get { return SelectedMessageUseObj == null ? false : SelectedMessageUseObj.IsArchived; }
        }

        public bool IsDisplay_IsShowArchived
        {
            get { return SelectedMessageUseObj == null ? false : SelectedMessageUseObj.IsShowArchived; }
        }

        public bool IsDisplayForEndOfSemRev
        {
            get { return SelectedMessageUseObj == null ? false : SelectedMessageUseObj.IsForEndOfSemRev; }
        }

        public string DisplayMessageText
        {
            get { return SelectedMessageUseObj == null ? "No Message Selected" : SelectedMessageUseObj.DisplayText; }
        }

        public List<MessageUseDomainObj> LiveMessages { get; set; }

        public List<MessageUseDomainObj> StoredMessages { get; set; }

        public List<MessageUseDomainObj> ArchivedMessages { get; set; }

        public bool HasStored
        {
            get { return StoredMessages == null ? false : StoredMessages.Count > 0; }
        }
        public Guid SelectedLessonId { get; set; }

        public Guid DisplayMessageUseId { get; set; }

        /// <summary>
        /// Returns a string based on the internal bool representation.
        /// </summary>
        public string ArchivedMenuText
        {
            get
            {
                if (IsDisplayArchived && !IsDisplay_IsShowArchived)
                {
                    return ("Remove from archive");
                }
                else
                {
                    return ("Archive message");
                }
            }
        }

        /// <summary>
        /// Returns a string based on the internal bool representation.
        /// </summary>
        public string StoredMenuText
        {
            get
            {
                if (IsDisplayStored)
                {
                    return ("Remove from storage");
                }
                else
                {
                    return ("Store message");
                }
            }
        }

        /// <summary>
        /// Returns a string based on the internal bool representation.
        /// </summary>
        public string ImportantMenuText
        {
            get
            {
                if (IsDisplayImportant)
                {
                    return ("Remove 'Important' tag");
                }
                else
                {
                    return ("Mark as 'Important'");
                }
            }
        }

        /// <summary>
        /// Returns a string based on the internal bool representation.
        /// </summary>
        public string EndOfSemRevMenuText
        {
            get
            {
                if (IsDisplayForEndOfSemRev)
                {
                    return ("Remove from 'End of semester review'");
                }
                else
                {
                    return ("Add to 'End of semester review'");
                }
            }
        }

        public bool ShowArchived { get; set; }
    }
}
