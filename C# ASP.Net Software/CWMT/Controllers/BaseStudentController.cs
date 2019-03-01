using CWMasterTeacher3;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.DomainObjectBuilders;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT.Controllers
{
    public class BaseStudentController : BaseController
    {
        private StudentUserDomainObjBuilder _studentUserObjBuilder;

        public BaseStudentController(UserDomainObjBuilder userObjBuilder, StudentUserDomainObjBuilder studentUserObjBuilder) : base(userObjBuilder)
        {
            _studentUserObjBuilder = studentUserObjBuilder;
        }

        public StudentUserDomainObj CurrentStudentUserObj
        {
            get
            {
                string currentUserName = User.Identity.GetUserName();
                if (!String.IsNullOrEmpty(currentUserName))
                {
                    return _studentUserObjBuilder.BuildFromUserName(currentUserName);
                }
                else
                {
                    return null;
                }
            }
        }

        public Guid CurrentStudentUserId
        {
            get { return CurrentStudentUserObj == null ? Guid.Empty : CurrentStudentUserObj.Id; }
        }

    }
}