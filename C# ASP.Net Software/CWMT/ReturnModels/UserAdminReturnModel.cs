using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMasterTeacher3
{
    public class UserAdminReturnModel
    {

        public string Password { get; set; }

        public bool SelectedUserIsApplicationAdmin { get; set; }

        public bool SelectedUserIsWorkingGroupAdmin { get; set; }

        public bool SelectedUserIsEditor { get; set; }

        public bool SelectedUserIsActive { get; set; }

        public bool SelectedUserHasAdminApproval { get; set; }

        public string EmailAddress { get; set; }


        public string DisplayName { get; set; }


        public string LastName { get; set; }


        public string FirstName { get; set; }

        public Guid WorkingGroupId { get; set; }


        public Guid UserId { get; set; }

        public string UserName { get; set; }





    }//End class
}