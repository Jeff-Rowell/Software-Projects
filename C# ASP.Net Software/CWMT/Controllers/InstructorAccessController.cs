using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT.Controllers
{
    public class InstructorAccessController : Controller
    {
        private WorkingGroupDomainObjBuilder _workingGroupObjBuilder;

        public InstructorAccessController(WorkingGroupDomainObjBuilder workingGroupObjBuilder)
        {
            _workingGroupObjBuilder = workingGroupObjBuilder;
        }
        // GET: InstructorAccess
        public ActionResult Index(string message = "")
        {
            InstructorAccessViewObj viewObj = new InstructorAccessViewObj(message);
            return View(viewObj);
        }

        public ActionResult ConfirmAccess(string instructorAccessCode)
        {
            Guid workingGroupId = _workingGroupObjBuilder.WorkingGroupIdForInstructorAccessCode(instructorAccessCode);
            if(workingGroupId == Guid.Empty)
            {
                string message = "Access Code is not valid.";
                return RedirectToAction("Index", new { message = message });
            }
            else
            {
                return RedirectToAction("Register", "Account", routeValues: new { IsInstructorNotStudent = true });
            }
        }
    }
}