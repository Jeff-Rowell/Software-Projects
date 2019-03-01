using CWMasterTeacher3;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMT.ReturnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT.Controllers
{
    public class WorkingGroupAdminController : BaseController
    {
        private WorkingGroupAdminViewObjBuilder _workingGroupViewObjBuilder;
        private WorkingGroupAdminCUDRouter _workingGroupCUDRouter;
        private UserDomainObjBuilder _userObjBuilder;

        public WorkingGroupAdminController(WorkingGroupAdminViewObjBuilder workingGroupViewObjBuilder,
                                            WorkingGroupAdminCUDRouter workingGroupCUDRouter,
                                            UserDomainObjBuilder userObjBuilder) : base(userObjBuilder)
        {
            _workingGroupViewObjBuilder = workingGroupViewObjBuilder;
            _workingGroupCUDRouter = workingGroupCUDRouter;
            _userObjBuilder = userObjBuilder;
        }



        // GET: WorkingGroupAdmin
        public ActionResult Index(Guid? workingGroupId = null, string message = "", bool doShowDeleteWorkingGroup = false)
        {

            WorkingGroupAdminViewObj viewObj = _workingGroupViewObjBuilder.Retrieve(currentUserObj: CurrentUserObj,
                                                            selectedWorkingGroupId: workingGroupId == null ? Guid.Empty : workingGroupId.Value,
                                                            message: message, doShowDeleteWorkingGroup: doShowDeleteWorkingGroup );

            return View(viewObj);
        }


        public ActionResult AddOrUpdateWorkingGroup(WorkingGroupAdminReturnModel model)
        {
            string message = _workingGroupCUDRouter.AddOrUpdateWorkingGroupReturnMessage(workingGroupId: model.WorkingGroupId,
                                                                    name: model.WorkingGroupName, studentAccessCode: model.StudentAccessCode,
                                                                    instructorAccessCode: model.InstructorAccessCode);

            return RedirectToAction("Index", new { workingGroupId = model.WorkingGroupId, message = message });
        }

        public ActionResult DeleteWorkingGroup(CourseCreateAdminReturnModel model)
        {
            try
            {

                string message = _workingGroupCUDRouter.DeleteWorkingGroupReturnMessage(model.WorkingGroupId);
                return RedirectToAction("Index", routeValues: new { message = message, doShowDeleteWorkingGroup = true });
            }
            catch (Exception e)
            {
                WebLogger.Log.DeleteWorkingGroupFailed(e);
                throw;
            }
        }


    }
}