using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface INarrativeDomainObjBuilder : IDomainObjBuilder<NarrativeDomainObj, NarrativeDomainObjBasic, Narrative>
    {
        NarrativeDomainObj CreateNarrativeObj(Guid lessonId, Guid userId);
    }
}
