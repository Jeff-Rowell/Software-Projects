using CWMasterTeacherDataModel;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Web.Mvc;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using CWMT.Services;

namespace CWMasterTeacher3
{
    /// <summary>
    /// Populates User View Model from User View Object, which is populated from User Domain Obj
    /// </summary>
    [Authorize]
    public class UserAdminController : BaseController
    {
        private UserDomainObjBuilder _userObjBuilder;
        private WorkingGroupRepo _workingGroupRepo;
        private UserDomainObjBuilder _userDomainObjBuilder;
        private WorkingGroupDomainObjBuilder _workingGroupObjBuilder;
        private UserAdminViewObjBuilder _userViewObjBuilder;
        private UserAdminCUDRouter _userAdminCUDRouter;
        private UserManagementService _userManagementService;

        public UserAdminController(UserDomainObjBuilder userObjBuilder, WorkingGroupRepo workingGroupRepo, 
                                UserDomainObjBuilder userDomainObjBuilder, WorkingGroupDomainObjBuilder workingGroupDomainObjBuilder,
                                UserAdminViewObjBuilder userViewObjBuilder, UserAdminCUDRouter userAdminCUDRouter,
                                UserManagementService userManagementService) :base(userObjBuilder)
        {
            _userObjBuilder = userObjBuilder ;
            _workingGroupRepo = workingGroupRepo;
            _userDomainObjBuilder = userDomainObjBuilder;
            _workingGroupObjBuilder = workingGroupDomainObjBuilder;
            _userViewObjBuilder = userViewObjBuilder;
            _userAdminCUDRouter = userAdminCUDRouter;
            _userManagementService = userManagementService;
        }

        public ActionResult Index(Guid? userId = null, Guid? workingGroupId = null, bool showAllWorkingGroups = false, 
                                    bool doChangePassword = false, string message = "")
        {
            try
            {
                if (workingGroupId == Guid.Empty)
                {
                    workingGroupId = CurrentWorkingGroupId;
                }

                UserAdminViewObj viewObj = _userViewObjBuilder.RetrieveUserAdminViewObject(userId : userId ?? Guid.Empty , 
                                                                                    workingGroupId: workingGroupId ?? Guid.Empty, 
                                                                                    currentUserName: CurrentUserName, 
                                                                                    showAllWorkingGroups: showAllWorkingGroups, 
                                                                                    doChangePassword: doChangePassword, 
                                                                                    message: message,
                                                                                    currentUserObj: CurrentUserObj );

                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.UserIndexFailed(e);
                throw;
            }
        }

        //Create User happens in RegisterCWUser in the Account Controller
        public ActionResult EditUser(UserAdminReturnModel model)
        {
            try
            {
                _userManagementService.SynchronizeRoles(model);



                string message = _userAdminCUDRouter.EditUser_ReturnMessage(userId: model.UserId, workingGroupId: model.WorkingGroupId, 
                                                                    userName: model.UserName, firstName: model.FirstName, 
                                                                    lastName: model.LastName, displayName: model.DisplayName,
                                                                    isApplicationAdmin: model.SelectedUserIsApplicationAdmin, 
                                                                    isWorkingGroupAdmin: model.SelectedUserIsWorkingGroupAdmin,
                                                                    isEditor: model.SelectedUserIsEditor, 
                                                                    emailAddress: model.EmailAddress,
                                                                    isActive: model.SelectedUserIsActive,
                                                                    hasAdminApproval: model.SelectedUserHasAdminApproval);

                return RedirectToAction("Index", new { userId = model.UserId, message = message });
            }
            catch (Exception e)
            {
                WebLogger.Log.User_EditUserFailed(e);
                throw;
            }
        }







    }
}
