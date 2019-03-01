using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class MessageUseRepo: BaseRepo<MessageUse>, IMessageUseRepo
    {
        public MessageUseRepo(MasterTeacherContext context)
            : base(context)
        { }

        /// <summary>
        /// Returns all MessageUses that have ThreadParentId == null.  Ordered by TimeStamp descending
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public IEnumerable<MessageUse> GetParentMessageUsesForLesson(Guid lessonId)
        {
            var listQuery = from x in _context.MessageUses
                            where x.LessonId == lessonId && x.Message.ThreadParentId == null
                                             orderby x.Message.TimeStamp descending
                                             select x;
            return listQuery.ToList().OrderByDescending(x => x.Message.TimeStamp);                                      
        }

        /// <summary>
        /// Returns all MessageUses, active and inactive, for a Lesson. Order by TimeStamp descending
        /// </summary>
        public IEnumerable<MessageUse> GetAllMessageUsesForLesson(Guid lessonId)
        {
            var listQuery = from x in _context.MessageUses
                            where x.LessonId == lessonId
                            orderby x.Message.TimeStamp descending
                            select x;
            return listQuery.ToList().OrderByDescending(x => x.Message.TimeStamp);
        }

        /// <summary>
        /// Ordered by TimeStamp descending
        /// </summary>
        public IEnumerable<MessageUse> GetAllActiveMessageUsesForLesson(Guid lessonId)
        {
            var listQuery = from x in _context.MessageUses
                            where x.LessonId == lessonId 
                            orderby x.Message.TimeStamp descending
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Returns all MessageUses that have a given MessageUse as their parent.  OrderBy TimeStamp descending.
        /// </summary>
        public IEnumerable<MessageUse> GetChildMessageUsesForMessageUse(MessageUse messageUse)
        {
            var listQuery = from x in _context.MessageUses
                            where x.LessonId == messageUse.LessonId && x.Message.ThreadParentId == messageUse.Message.Id
                            orderby x.Message.TimeStamp descending
                            select x;
            return listQuery.ToList().OrderByDescending(x => x.Message.TimeStamp);
        }

        /// <summary>
        /// Gets all MessageUses for a Message. Used for posting replies to a message.
        /// </summary>
        public IEnumerable<MessageUse> GetMessageUsesForMessage(Guid messageId)
        {
            var query = from x in _context.MessageUses
                        where x.MessageId == messageId
                        select x;
            return query.ToList();

        }

        public string GetMessageText(Guid messageUseId)
        {
            MessageUse messageUse = GetById(messageUseId);
            return messageUse.Message.Text;
        }


    }//End Class
}
