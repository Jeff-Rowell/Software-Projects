using CWMasterTeacherDataModel;
using System;
using System.Web.Mvc;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3
{
    public class CourseCopyAdminController : BaseController
    {

        //private CourseRepo _courseRepo;
        private CourseCopyAdminViewObjBuilder _courseCopyViewObjBuilder;
        private CourseCUDService _courseCUDService;
        private CourseCopyAdminCUDRouter _courseCopyAdminCUDRouter;


        public CourseCopyAdminController(UserDomainObjBuilder userObjBuilder, 
                                    CourseCopyAdminViewObjBuilder courseCopyViewObjBuilder, CourseCUDService courseCUDService,
                                    CourseCopyAdminCUDRouter courseCopyAdminCUDRouter) :base(userObjBuilder)
        {
            _courseCopyViewObjBuilder = courseCopyViewObjBuilder;
            _courseCUDService = courseCUDService;
            _courseCopyAdminCUDRouter = courseCopyAdminCUDRouter;
        }

        /// <summary>
        /// Copies a course
        /// </summary>
        /// <param name="successMessage">Message indicating success</param>
        /// <param name="isMaster">Indicates if the course is the master course</param>
        /// <returns></returns>
        public ActionResult Index(string successMessage = "", bool isMaster = false)
        {
            try
            {
                CourseCopyAdminViewObj viewObj = _courseCopyViewObjBuilder.Retrieve(isMaster: isMaster, successMessage: successMessage,
                                                currentUserObj: CurrentUserObj);
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.CourseCopyIndexFailed(e);
                throw;
            }

        }

        public ActionResult CopyCourse(Guid fromCourseId, Guid? masterCourseId,  Guid toUserId, Guid toTermId, bool isNewCourseMirror = false)
        {
            try
            {
                bool isMaster = (masterCourseId == null);
                string successMessage = _courseCopyAdminCUDRouter.CopyCourseReturnMessage(fromCourseId: fromCourseId,
                                                                                        masterCourseId: masterCourseId,
                                                                                        toUserId: toUserId,
                                                                                        toTermId: toTermId,
                                                                                        isNewCourseMirror: isNewCourseMirror);

                return RedirectToAction("Index", new { successMessage = successMessage, isMaster = isMaster });
            }
            catch (Exception e)
            {
                WebLogger.Log.CopyCourseFailed(e);
                throw;
            }
        }

    }
}
