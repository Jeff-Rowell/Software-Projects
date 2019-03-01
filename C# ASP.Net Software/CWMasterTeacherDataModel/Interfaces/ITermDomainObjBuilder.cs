using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ITermDomainObjBuilder : Interfaces.IDomainObjBuilder<TermDomainObj, TermDomainObjBasic, Term>
    {
        List<TermDomainObj> GetTermsForWorkingGroupList(Guid workingGroupId);
        TermDomainObj CurrentTerm();
        List<TermDomainObjBasic> TermsForWorkingGroup(Guid currentWorkingGroupId);
    }
}
