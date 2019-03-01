using CWMasterTeacherDataModel;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class CourseCreateAdminCUDRouter
    {
        private CourseCUDService _courseCUDService;
        private WorkingGroupCUDService _workingGroupCUDService;
        private LessonCUDService _lessonCUDService;
        
        public CourseCreateAdminCUDRouter(CourseCUDService courseCUDService, WorkingGroupCUDService workingGroupCUDService,
                                LessonCUDService lessonCUDService)
        {
            _courseCUDService = courseCUDService;
            _workingGroupCUDService = workingGroupCUDService;
            _lessonCUDService = lessonCUDService;
        }

        public Tuple<Guid, string> CreateCourseReturnIdAndMessage(string courseName, Guid workingGroupId, Guid userId, Guid termId)
        {
            return _courseCUDService.CreateMasterCourseReturnIdAndMessage(courseName, workingGroupId, userId, termId);

        }

        public void CreateWorkingGroup(string workingGroupName)
        {
            
        }

        public void RenameWorkingGroup(Guid workingGroupId, string workingGroupName)
        {
            
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
                message = "No working group was selected.  Id was <= 0";
            }
            return message;
        }

    }//End Class
}
