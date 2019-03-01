using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWMT.ReturnModels
{
    public class WorkingGroupAdminReturnModel
    {
        public Guid WorkingGroupId { get; set; }

        public string WorkingGroupName { get; set; }

        public string StudentAccessCode { get; set; }

        public string InstructorAccessCode { get; set; }

    }
}