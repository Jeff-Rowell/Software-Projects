using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IMessageUseRepo : IRepository<MessageUse>
    {
        IEnumerable<MessageUse> GetParentMessageUsesForLesson(Guid lessonId);
        IEnumerable<MessageUse> GetAllMessageUsesForLesson(Guid lessonId);
        IEnumerable<MessageUse> GetAllActiveMessageUsesForLesson(Guid lessonId);
        IEnumerable<MessageUse> GetChildMessageUsesForMessageUse(MessageUse messageUse);
        IEnumerable<MessageUse> GetMessageUsesForMessage(Guid messageId);
        string GetMessageText(Guid messageUseId);
    }
}
