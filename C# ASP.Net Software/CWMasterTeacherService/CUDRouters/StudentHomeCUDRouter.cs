using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class StudentHomeCUDRouter
    {
        private StudentUserCUDService _studentUserCUDService;
        public StudentHomeCUDRouter(StudentUserCUDService studentUserCUDService)
        {
            _studentUserCUDService = studentUserCUDService;
        }


        public string RequestGroupAccessReturnMessage(Guid studentUserId, string accessCode)
        {
            return _studentUserCUDService.RequestGroupAccessReturnMessage(studentUserId, accessCode);
        }

        public string RequestCourseAccessReturnMessage(Guid studentUserId, Guid classSectionId)
        {
            return _studentUserCUDService.RequestCourseAccessReturnMessage(studentUserId, classSectionId);
        }
    }
}
