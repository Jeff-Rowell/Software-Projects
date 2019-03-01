using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    /// <summary>
    /// Interface to be implemented by classes wishing to provide services that build and return WorkingGroupDomainObj.
    /// </summary>
    public interface IWorkingGroupDomainObjBuilder : IDomainObjBuilder<WorkingGroupDomainObj, WorkingGroupDomainObjBasic, WorkingGroup>
    {
        List<WorkingGroupDomainObjBasic> AllWorkingGroupsBasicList();
    }
}
