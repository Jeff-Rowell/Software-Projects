using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using System;

namespace CWMasterTeacherDataModel.Interfaces
{
    /// <summary>
    /// Interface to be implemented by classes that provide services to build and return MessageUseDomainObjs.
    /// </summary>
    public interface IMessageUseDomainObjBuilder : Interfaces.IDomainObjBuilder<MessageUseDomainObj, MessageUseDomainObjBasic, MessageUse>
    {
        List<MessageUseDomainObj> GetChildMessageUseObjsForMessageUseListFromId(Guid messageUseId);
        List<MessageUseDomainObj> GetParentMessageUsesList(Guid lessonId);
        MessageUseDomainObj BuildFromId(Guid id);
        MessageUseDomainObj BuildFromId_IncludeArchived(Guid id);
        MessageUseDomainObjBasic BuildBasicFromId(Guid id);
        List<MessageUseDomainObj> GetChildMessageUseObjsForMessageUseList(MessageUse messageUse);
        List<MessageUseDomainObj> GetAllMessageUseObjsForLesson(Guid lessonId);
        MessageUseDomainObj BuildCompleteFromDbObject(MessageUse messageUse);
    }
}
