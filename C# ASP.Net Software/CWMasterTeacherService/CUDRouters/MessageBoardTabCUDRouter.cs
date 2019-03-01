using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class MessageBoardTabCUDRouter
    {
        private MessageCUDService _messageCUDService;
        private MessageUseDomainObjBuilder _messageUseObjBuilder;
        private SharedCUDService _sharedCUDService;

        public MessageBoardTabCUDRouter(MessageCUDService messageCUDService, MessageUseDomainObjBuilder messageUseObjBuilder,
                                        SharedCUDService sharedCUDService)
        {
            _messageCUDService = messageCUDService;
            _messageUseObjBuilder = messageUseObjBuilder;
            _sharedCUDService = sharedCUDService;
        }

        public void PostMessage(Guid lessonId, Guid parentMessageUseId, Guid authorId, string subject, string text, bool isToSelf, bool isToStorage)
        {
            if(text == null || text.Length < 1)
            {
                text = "_";
            }
            Guid parentMessageId;
            MessageUseDomainObj parentMessageUse = _messageUseObjBuilder.BuildFromId(parentMessageUseId);
            if (parentMessageUse != null)
            {
                parentMessageId = parentMessageUse.MessageId;
            }
            else
            {
                parentMessageId = Guid.Empty;
            }
            _messageCUDService.PostMessage(lessonId: lessonId, parentMessageId: parentMessageId, authorId: authorId,
                                            subject: subject, text: text, isToSelf: isToSelf, isToStorage: isToStorage);
        }

        public void UpdateIsStored(Guid messageUseId, bool isStored)
        {
            _messageCUDService.UpdateIsStored(messageUseId, isStored);
        }

        public void UpdateIsImportant(Guid messageUseId, bool isImportant)
        {
            _messageCUDService.UpdateIsImportant(messageUseId, isImportant);
        }
        public void UpdateIsArchived(Guid messageUseId, bool isArchived, bool isShowArchived)
        {
            //archived values are coming in as they currently exist. The call is for what we want the Archived value to be.
            _messageCUDService.UpdateIsArchived(messageUseId, (!isArchived || isShowArchived));
        }

        public void UpdateIsForEndOfSemRev(Guid messageUseId, bool isForEndOfSemRev)
        {
            _messageCUDService.UpdateIsForEndOfSemRev(messageUseId, isForEndOfSemRev);
        }

        public void UpdateAllMessageBooleansForLessonsSharingCourse(Guid lessonId)
        {
            _sharedCUDService.UpdateAllMessageBooleansForLessonsSharingCourse(lessonId);
        }


    }//End Class
}
