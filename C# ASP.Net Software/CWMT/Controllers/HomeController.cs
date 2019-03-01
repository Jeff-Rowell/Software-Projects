using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMT.Models;
using CWMT.Controllers;
using CWMasterTeacherService.DomainObjectBuilders;

namespace CWMasterTeacher3
{

    //This only exists to do the redirect below
    [Authorize]
    public class HomeController : BaseStudentController
    {
        private UserDomainObjBuilder _userObjBuilder;
        private StudentUserDomainObjBuilder _studentUserObjBuilder;

        public HomeController(UserDomainObjBuilder userObjBuilder, StudentUserDomainObjBuilder studentUserObjBuilder) 
                                : base(userObjBuilder, studentUserObjBuilder)
        {
            _userObjBuilder = userObjBuilder;
            _studentUserObjBuilder = studentUserObjBuilder;
        }

        public ActionResult Index()
        {
            if (CurrentUserIsInstructor)
            {
                _userObjBuilder.SetLastLogin(CurrentUserId);
                if(CurrentUserObj != null && CurrentUserObj.HasAdminApproval)
                {
                    return RedirectToAction("Index", "Curriculum");
                }
                else if(CurrentUserObj != null)
                {
                    return RedirectToAction("AwaitingApproval");
                }
                else
                {
                    ApplicationUser appUser = CurrentApplicationUser;
                    return RedirectToAction("Index", "UserProfile", routeValues: new { userName = appUser.UserName, email = appUser.Email, isInstructor = true });
                }
            }
            else
            {
                _studentUserObjBuilder.SetLastLogin(CurrentStudentUserId);
                if(CurrentStudentUserObj != null)
                {
                    return RedirectToAction("Index", "StudentHome");
                }
                else
                {
                    ApplicationUser appUser = CurrentApplicationUser;
                    return RedirectToAction("Index", "UserProfile", routeValues: new { userName = appUser.UserName, email = appUser.Email });

                }
            }

        }

        public ActionResult AwaitingApproval()
        {
            return View();
        }


    }
}
