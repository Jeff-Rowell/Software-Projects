using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CWMasterTeacher3
{
    public class AdminChangePasswordReturnModel
    {
        private int _userId;
        private string _newPassword;
        private string _confirmPassword;
        private string _heading;

        public string Heading
        {
            get { return _heading; }
            set { _heading = value; }
        }



        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
        }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; }
        }



        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

    }
}