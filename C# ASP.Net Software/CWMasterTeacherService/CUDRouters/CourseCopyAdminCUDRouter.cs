using CWMasterTeacherDataModel;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class CourseCopyAdminCUDRouter
    {
        private CourseCUDService _courseCUDService;

        public CourseCopyAdminCUDRouter(CourseCUDService courseCUDService)
        {
            _courseCUDService = courseCUDService;
        }

        public string CopyCourseReturnMessage(Guid fromCourseId, Guid? masterCourseId, Guid toUserId, Guid toTermId, bool isNewCourseMirror)
        {
            return _courseCUDService.CopyCourseReturnMessage(fromCourseId: fromCourseId,
                                                                masterCourseId: masterCourseId,
                                                                toUserId: toUserId,
                                                                toTermId: toTermId);                                                          
        }

    }
}

