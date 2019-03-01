using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{ 
    public class WorkingGroupAdminCUDRouter
    {
        private WorkingGroupCUDService _workingGroupCUDService;

        public WorkingGroupAdminCUDRouter(WorkingGroupCUDService workingGroupCUDService)
        {
            _workingGroupCUDService = workingGroupCUDService;
        }

        public string AddOrUpdateWorkingGroupReturnMessage(Guid workingGroupId, string name, string studentAccessCode, string instructorAccessCode)
        {
            string message = _workingGroupCUDService.AddOrUpdateWorkingGroup(workingGroupId: workingGroupId,
                                                                               name: name, studentAccessCode: studentAccessCode,
                                                                               instructorAccessCode: instructorAccessCode);
            return message;
        }

        public string DeleteWorkingGroupReturnMessage(Guid workingGroupId)
        {
            string message = "";
            if (workingGroupId != Guid.Empty)
            {
                message = _workingGroupCUDService.DeleteWorkingGroupReturnMessage(workingGroupId);
            }
            else
            {
                message = "No working group was selected.  Id was empty.";
            }
            return message;
        }

    }
}
