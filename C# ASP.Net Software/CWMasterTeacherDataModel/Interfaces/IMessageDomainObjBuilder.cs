using System;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDataModel.Interfaces
{
    /// <summary>
    /// Interface to be implemented by classes that provide services to build and return MessageDomainObjs.
    /// </summary>
    public interface IMessageDomainObjBuilder : Interfaces.IDomainObjBuilder<MessageDomainObj,
                                                                             MessageDomainObjBasic,
                                                                             Message>
    {
        void UpdateIsNew(Guid messageUseId, bool isNew);
    }
}
