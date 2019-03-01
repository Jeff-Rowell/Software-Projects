using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWMasterTeacher3.ReturnModels
{
    public class UserProfileReturnModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public Guid WorkingGroupId { get; set; }
        public Guid UserId { get; set; }
        public bool IsInstructor { get; set; }

    }
}