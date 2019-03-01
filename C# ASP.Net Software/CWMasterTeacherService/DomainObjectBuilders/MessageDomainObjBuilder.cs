using System;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// Builds MessageDomainObjs out of DB objects
    /// </summary>
    public class MessageDomainObjBuilder : IMessageDomainObjBuilder
    {
        private MessageRepo _messageRepo;
        private MessageUseRepo _messageUseRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageRepo">A repository containing the Message data</param>
        public MessageDomainObjBuilder(MessageRepo messageRepo, MessageUseRepo messageUseRepo)
        {
            _messageRepo = messageRepo;
            _messageUseRepo = messageUseRepo;
        }

        /// <summary>
        /// Builds a MessageDomainObj from the provided Message.
        /// </summary>
        /// <param name="dbObject">A Message object containing the data used to build the MessageDomainObj</param>
        /// <returns>A new MessageDomainObj initialized with the data from the argument.</returns>
        public static MessageDomainObj Build(Message dbObject)
        {
            return new MessageDomainObj
            (
                BuildBasic(dbObject),
                dbObject.ThreadParentId,
                dbObject.Text,
                dbObject.Subject,
                dbObject.TimeStamp
            );
        }

        /// <summary>
        /// Builds a MessageDomainObj from the Message, retrieved using the id parameter.
        /// </summary>
        /// <param name="id">The id of the Message to be used to build the MessageDomainObj</param>
        /// <returns>A new MessageDomainObj initialized with the data retrieved using the argument.</returns>
        public MessageDomainObj BuildFromId(Guid id)
        {
            Message message = _messageRepo.GetById(id);
            return Build(message);
        }

        /// <summary>
        /// Builds a MessageDomainObjBasic from the provided Message.
        /// </summary>
        /// <param name="dbObject">A Message object containing the data used to build the MessageDomainObjBasic</param>
        /// <returns>A new MessageDomainObjBasic initialized with the data from the argument.</returns>
        public static MessageDomainObjBasic BuildBasic(Message dbObject)
        {
            return new MessageDomainObjBasic
            (
                dbObject.Id,
                dbObject.Subject,
                dbObject.User.DisplayName,
                dbObject.TimeStamp
            );
        }

        /// <summary>
        /// Builds a MessageDomainObjBasic from the Message, retrieved using the id parameter.
        /// </summary>
        /// <param name="id">The id of the Message to be used to build the MessageDomainObjBasic</param>
        /// <returns>A new MessageDomainObjBasic initialized with the data retrieved using the argument.</returns>
        public MessageDomainObjBasic BuildBasicFromId(Guid id)
        {
            Message message = _messageRepo.GetById(id);
            return BuildBasic(message);
        }

        /// <summary>
        /// Updates the IsNew property of the selected MessageUse in the database.
        /// </summary>
        /// <param name="messageUseId">The id of the MessageUse to be updated</param>
        /// <param name="isNew">The property of the selected MessageUse to be updated</param>
        public void UpdateIsNew(Guid messageUseId, bool isNew)
        {
            MessageUse messageUse = _messageUseRepo.GetById(messageUseId);
            messageUse.IsNew = isNew;
            _messageUseRepo.Update(messageUse);
        }
    }
}
