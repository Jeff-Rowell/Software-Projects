using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class MessageUseCUDService
    {
        private IMessageUseRepo _messageUseRepo;
        public MessageUseCUDService(IMessageUseRepo messageUseRepo)
        {
            _messageUseRepo = messageUseRepo;
        }

        public MessageUse Create(Guid messageId, Guid lessonId, bool isStored, bool isNew)
        {
            MessageUse messageUse = new MessageUse();
            messageUse.MessageId = messageId;
            messageUse.LessonId = lessonId;
            messageUse.IsNew = isNew;
            messageUse.IsStored = isStored;
            messageUse.IsImportant = false;
            messageUse.IsArchived = false;
            messageUse.IsForEndOfSemRev = false;

            _messageUseRepo.Insert(messageUse);

            return messageUse;
        }





    }//End Class
}
