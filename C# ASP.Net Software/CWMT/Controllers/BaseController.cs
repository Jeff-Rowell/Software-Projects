using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMT.Models;
using Microsoft.AspNet.Identity.Owin;
using CWMasterTeacher3;
using CWMasterTeacherDomain;

namespace CWMasterTeacher3
{
    public abstract class BaseController : Controller
    {
        private UserDomainObjBuilder _userDomainObjBuilder;

        public BaseController(UserDomainObjBuilder userObjBuilder)
        {
            _userDomainObjBuilder = userObjBuilder;
        }

        public string CurrentUserName
        {
            get
            {
                return User.Identity.GetUserName();
            }
        }

        public UserDomainObj CurrentUserObj
        {
            get
            {
                string currentUserName = User.Identity.GetUserName();
                if (!String.IsNullOrEmpty(currentUserName))
                {
                    return _userDomainObjBuilder.BuildFromUserName(currentUserName);
                }
                else
                {
                    return null;
                }
            }
        }



        public ApplicationUser CurrentApplicationUser
        {
            get {  return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()); }
        }

        public bool CurrentUserIsInstructor
        {
            get
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return userManager.IsInRole(User.Identity.GetUserId(), DomainWebUtilities.Roles.Instructor);
                
            }
        }




        public Guid CurrentUserId
        {
            get
            {
                if (CurrentUserObj != null)
                {
                    return CurrentUserObj.Id;
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }

        public Guid CurrentWorkingGroupId
        {
            get
            {
                if (CurrentUserObj != null)
                {
                    return CurrentUserObj.WorkingGroupId;
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }

    }
}
