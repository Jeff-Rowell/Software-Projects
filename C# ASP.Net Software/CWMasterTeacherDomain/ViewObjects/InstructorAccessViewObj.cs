using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class InstructorAccessViewObj
    {
        public InstructorAccessViewObj(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public string InstructorAccessCode { get; set; }
    }
}
