using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacher3;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacher3.ReturnModels;

namespace CWMasterTeacher3.Controllers
{
    public class UserProfileController : BaseController
    {
        private UserProfileViewObjBuilder _viewObjBuilder;
        private UserProfileCUDRouter _router;
        public UserProfileController(UserDomainObjBuilder userObjBuilder, UserProfileViewObjBuilder viewObjBuilder, UserProfileCUDRouter router) : base(userObjBuilder)
        {
            _viewObjBuilder = viewObjBuilder;
            _router = router;
        }

        [Route ("UserProfile/Index") ]
        public ActionResult Index(string userName = "", string email = "", Guid? userId = null, bool isInstructor = false, string message = "")
        {
            UserProfileViewObj viewObj = _viewObjBuilder.RetrieveUserProfileViewObj(userName: userName, email: email, 
                                                                                userId: userId ?? Guid.Empty, 
                                                                                isInstructor: isInstructor,
                                                                                message: message);
            return View(viewObj);
        }

        //[Route ("UserProfile/EditOrCreateUserProfile")]
        public ActionResult EditOrCreateUserProfile(UserProfileReturnModel model)
        {
            string message = _router.EditOrCreateUserProfile_ReturnMessage(userId: model.UserId, workingGroupId: model.WorkingGroupId,
                                                            userName: model.UserName, firstName: model.FirstName,
                                                            lastName: model.LastName, displayName: model.DisplayName,
                                                            emailAddress: model.Email, isInstructor: model.IsInstructor);
            if (model.IsInstructor)
            {
                return RedirectToAction("Index", "Home", routeValues: new { message = message});
            }
            else
            {
                return RedirectToAction("Index", "Home", routeValues: new {message = message});
            }


        }



    }
}