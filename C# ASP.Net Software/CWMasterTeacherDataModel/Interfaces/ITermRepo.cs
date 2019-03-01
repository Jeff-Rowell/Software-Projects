using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ITermRepo : IRepository<Term>
    {
        IEnumerable<Term> TermsForWorkingGroup(Guid workingGroupId);
        Term CurrentTerm { get; }
        IEnumerable<Term> AllCurrentTermsForWorkingGroup(Guid workingGroupId);
        Term CreateTerm(Guid workingGroupId, string name, DateTime startDate, DateTime endDate);
        List<Term> TermsThatHaveNotEndedYet();
    }
}
