using CWMasterTeacher3;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT.Controllers
{
    public class AdminResetPasswordController : BaseController
    {
        private AdminResetPasswordViewObjBuilder _viewObjBuilder;

        public AdminResetPasswordController(UserDomainObjBuilder userObjBuilder, AdminResetPasswordViewObjBuilder viewObjBuilder) :base (userObjBuilder)
        { _viewObjBuilder = viewObjBuilder; }



        // GET: AdminResetPassword
        public ActionResult Index(bool isInstructorNotStudent = false, Guid? selectedUserId = null, string userName = "", string message = "")
        {
            AdminResetPasswordViewObj viewObj = _viewObjBuilder.Retrieve(currentUserObj: CurrentUserObj,
                                                                        isInstructorNotStudent: isInstructorNotStudent,
                                                                        selectedUserId: selectedUserId == null ? Guid.Empty : selectedUserId.Value,
                                                                        userName: userName,
                                                                        message: message);
            return View(viewObj);
        }
    }
}