using System;
using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// Builds MessageUseDomainObjs out of DB objects
    /// </summary>
    public class MessageUseDomainObjBuilder : IMessageUseDomainObjBuilder
    {
        private MessageUseRepo _messageUseRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageUseRepo">A repository containing the MessageUse data</param>
        public MessageUseDomainObjBuilder(MessageUseRepo messageUseRepo)
        {
            _messageUseRepo = messageUseRepo;
        }

        /// <summary>
        /// Builds a MessageUseDomainObj from the provided MessageUse.
        /// </summary>
        /// <param name="dbObject">A MessageUse object containing the data used to build the MessageUseDomainObj.</param>
        /// <returns>A new MessageUseDomainObj initialized with the data from the argument.</returns>
        public static MessageUseDomainObj Build(MessageUse dbObject)
        {
            return new MessageUseDomainObj
            (
                messageUseBasic: BuildBasic(dbObject),
                messageId: dbObject.MessageId,
                lessonId: dbObject.LessonId,
                isNew: dbObject.IsNew,
                storageReferenceTime: dbObject.StorageReferenceTime,
                isArchived: dbObject.IsArchived,
                isForEndOfSemRev: dbObject.IsForEndOfSemRev,
                isImportant: dbObject.IsImportant,
                isStored: dbObject.IsStored,
                termStartDate: dbObject.Lesson.Course.Term.StartDate,
                threadParentId: dbObject.Message.ThreadParentId,
                text: dbObject.Message.Text,
                userId: dbObject.Message.User.Id,
                showArchived: dbObject.ShowArchived );       
        }


        /// <summary>
        /// Builds a MessageUseDomainObj from the MessageUse, retrieved using the id parameter.
        /// Does not include archived messageUses
        /// </summary>
        /// <param name="id">The id of the MessageUse to be used to build the MessageUseDomainObj.</param>
        /// <returns>A new MessageUseDomainObj initialized with the data retrieved using the argument.</returns>
        public MessageUseDomainObj BuildFromId(Guid id)
        {
            MessageUse messageUse = _messageUseRepo.GetById(id);
            return messageUse == null ? null : BuildCompleteFromDbObject(messageUse);
        }

        public MessageUseDomainObj BuildFromId_IncludeArchived(Guid id)
        {
            MessageUse messageUse = _messageUseRepo.GetById(id);
            return messageUse == null ? null : BuildCompleteFromDbObject(messageUse);
        }

        /// <summary>
        /// Builds a MessageUseDomainObjBasic from the provided MessageUse.
        /// </summary>
        /// <param name="dbObject">A MessageUse object containing the data used to build the MessageUseDomainObjBasic.</param>
        /// <returns>A new MessageUseDomainObjBasic initialized with the data from the argument.</returns>
        public static MessageUseDomainObjBasic BuildBasic(MessageUse dbObject)
        {
            return new MessageUseDomainObjBasic(id: dbObject.Id,
                                                subject: dbObject.Message.Subject,
                                                userDisplayName: dbObject.Message.User.DisplayName,
                                                timestamp: dbObject.Message.TimeStamp);
        }

        /// <summary>
        /// Builds a MessageUseDomainObjBasic from the MessageUse, retrieved using the id parameter.
        /// </summary>
        /// <param name="id">The id of the MessageUse to be used to build the MessageUseDomainObjBasic.</param>
        /// <returns>A new DocumentUseDomainObjBasic initialized with the data retrieved using the argument.</returns>
        public MessageUseDomainObjBasic BuildBasicFromId(Guid id)
        {
            MessageUse messageUse = _messageUseRepo.GetById(id);
            return BuildBasic(messageUse);
        }

        /// <summary>
        /// Builds all child MessageUse associated with the selected MessageUse.
        /// </summary>
        /// <param name="messageUseId">The MessageUse Id to be used to get the MessageUse object and build the
        /// MessageUseDomainObj children.</param>
        /// <returns>A list of children MessageUseDomainObj associated with the MessageUseDomainObj.</returns>
        public List<MessageUseDomainObj> GetChildMessageUseObjsForMessageUseListFromId(Guid messageUseId)
        {
            MessageUse messageUse = _messageUseRepo.GetById(messageUseId);
            IEnumerable<MessageUse> childMessageUsesList = _messageUseRepo.GetChildMessageUsesForMessageUse(messageUse);
            IEnumerable<MessageUseDomainObj> childMessageUseObjList = childMessageUsesList.Select(x => BuildCompleteFromDbObject(x));
            return childMessageUseObjList.ToList();
        }

        public List<MessageUseDomainObj> GetChildMessageUseObjsForMessageUseList(MessageUse messageUse)
        {
            IEnumerable<MessageUse> childMessageUsesList = _messageUseRepo.GetChildMessageUsesForMessageUse(messageUse);
            IEnumerable<MessageUseDomainObj> childMessageUseObjList = childMessageUsesList.Select(x => BuildCompleteFromDbObject(x));
            return childMessageUseObjList.ToList();
        }

        /// <summary>
        /// Builds all parent MessageUse associated with the lesson. Specifically means MessageUses that have ThreadParentId == null.
        /// </summary>
        /// <param name="lessonId">The id of the lesson for which all parent MessageUse will be built.</param>
        /// <returns>A list of parent MessageUseDomainObj that are associated with the lesson.</returns>
        public List<MessageUseDomainObj> GetParentMessageUsesList(Guid lessonId)
        {
            IEnumerable<MessageUse> parentMessageUsesList = _messageUseRepo.GetParentMessageUsesForLesson(lessonId);
            return parentMessageUsesList.Select(x => BuildCompleteFromDbObject(x)).ToList();
        }

        /// <summary>
        /// Builds all Message Uses for Lesson.  Used in copying Lessons.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<MessageUseDomainObj> GetAllMessageUseObjsForLesson(Guid lessonId)
        {
            IEnumerable<MessageUse> allMessageUsesList = _messageUseRepo.GetAllMessageUsesForLesson(lessonId);
            return allMessageUsesList.Select(x => BuildCompleteFromDbObject(x)).ToList();
        }

        /// <summary>
        /// Builds MessageUseDomainObject that includes a list of child objects.
        /// Does not iteratively created children of children.  That iteration needs to happen somewhere else.
        /// </summary>
        /// <param name="messageUse"></param>
        /// <returns></returns>
        public MessageUseDomainObj BuildCompleteFromDbObject(MessageUse messageUse)
        {
            if (messageUse != null)
            {
                MessageUseDomainObj messageUseObj = Build(messageUse);
                messageUseObj.ChildMessageUseObjList = GetChildMessageUseObjsForMessageUseList(messageUse );
                foreach (var xObj in messageUseObj.ChildMessageUseObjList)
                {
                    GetChildMessageUseObjsForMessageUseListFromId(xObj.Id );
                }
                return messageUseObj;
            }
            else
            {
                return null;
            }
        }


    }//End Class
}
