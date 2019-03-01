using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CWMasterTeacherService.RetrieveServices
{
    /// <summary>
    /// Provides retrieval services to get an instance of a MessageUseViewObj containing the desired data.
    /// </summary>
    public class MessageUseService
    {
        private IMessageUseDomainObjBuilder _messageUseDomainObjBuilder;

        /// <summary>
        ///  Creates a MessageUseService that uses the supplied builder when acquiring data.
        /// </summary>
        /// <param name="messageUseDomainObjBuilder">An IMessageUseDomainObjBuilder that is used when acquiring message use data.</param>
        public MessageUseService(IMessageUseDomainObjBuilder messageUseDomainObjBuilder)
        {
            _messageUseDomainObjBuilder = messageUseDomainObjBuilder;
        }

        /// <summary>
        ///  Retrieves a MessageUseViewObj built using the given arguments.
        /// </summary>
        /// <param name="id">The id of the selected message use.</param>
        /// <returns>A MessageUseViewObj</returns>
        public MessageUseViewObj Retrieve(int id)
        {
            MessageUseDomainObj messageUseDomainObj = _messageUseDomainObjBuilder.BuildFromId(id);

            return new MessageUseViewObj(messageUseDomainObj, getChildMessageUsesForMessageUseList(messageUseDomainObj.MessageUseBasic.Id));
        }

        private List<MessageUseViewObj> getChildMessageUsesForMessageUseList(int messageUseId)
        {
            List<MessageUseDomainObj> childMessageUses = _messageUseDomainObjBuilder.GetChildMessageUseObjsForMessageUseListFromId(messageUseId);
            return childMessageUses == null? new List<MessageUseViewObj>() : childMessageUses.Select(x => Retrieve(x.MessageUseBasic.Id)).ToList();
        }
    }
}