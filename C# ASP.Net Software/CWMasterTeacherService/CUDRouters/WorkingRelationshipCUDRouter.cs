using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class WorkingRelationshipCUDRouter
    {
        private WorkingRelationshipCUDService _workingRelationshipCUDService;

        public WorkingRelationshipCUDRouter (WorkingRelationshipCUDService workingRelationshipCUDService)
        {
            _workingRelationshipCUDService = workingRelationshipCUDService;
        }

        public void UpdateWorkingRelationship(Guid workRelId, bool followNarrative, int sequenceNumber)
        {
            _workingRelationshipCUDService.UpdateWorkingRelationshipValues(workRelId, followNarrative, sequenceNumber);
        }

    }
}
