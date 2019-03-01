using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    /// <summary>
    /// A view object to store relevant information from the related Domain Object to be passed
    /// to the View Model.
    /// </summary>
    public class UserAdminViewObj
    {
        /// <summary>
        /// Constructs a new UserViewObj, assigns the userDomainObj and two other fields. The
        /// others are set in the service after creation.
        /// </summary>
        /// <param name="user"></param>
        public UserAdminViewObj()
        {}

        public UserDomainObj UserObj { get; set; }

        public List<UserDomainObjBasic> UserObjBasicList { private get; set; }
        public List<WorkingGroupDomainObjBasic> WorkingGroupObjList { private get; set; }

        public IEnumerable<SelectListItem> WorkingGroupSelectList
        {
            get
            {
                return new SelectList(WorkingGroupObjList, "Id", "Name"); 
            }
        }
        public IEnumerable<SelectListItem> UserSelectList
        {
            get
            {
                return new SelectList(UserObjBasicList , "Id", "Name");
            }
        }




        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

        public bool UserIsEditor
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsMasterEditor; }
        }

        public string ActionName
        {
            get
            {
                if(UserObj.Id != Guid.Empty)
                {
                    return "EditUser";
                }
                else
                {
                    return "RegisterCWUser";
                }
            }
        }

        public string ControllerName
        {
            get
            {
                if (UserObj.Id != Guid.Empty )
                {
                    return "UserAdmin";
                }
                else
                {
                    return "Account";
                }
            }
        }

        /// <summary>
        /// Generates two lists for drop down menus from corresponding lists of DomainObjects. User for testing.
        /// </summary>
        /// <param name="userList"></param>
        /// <param name="workingGroupList"></param>
        public void genSelectLists(List<UserDomainObjBasic> userList, 
            List<WorkingGroupDomainObjBasic> workingGroupList)
        {
            UserObjBasicList = userList;
            WorkingGroupObjList = workingGroupList;
        }

        public string Message { get; set; }


        [Display(Name = "Reset Password")]
        public bool DoChangePassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool ShowAllWorkingGroups { get; set; }

        [Display(Name = "Is Application Admin")]
        public bool SelectedUserIsApplicationAdmin
        {
            get { return UserObj == null ? false : UserObj.IsApplicationAdmin; }
            set
            {
                if(UserObj != null)
                {
                    UserObj.IsApplicationAdmin = value;
                }
            }
        }

        [Display(Name = "Is Working Group Admin")]
        public bool SelectedUserIsWorkingGroupAdmin
        {
            get { return UserObj == null ? false : UserObj.IsWorkingGroupAdmin; }
            set
            {
                if (UserObj != null)
                {
                    UserObj.IsWorkingGroupAdmin  = value;
                }
            }
        }


        [Display(Name = "Is Narrative Editor")]
        public bool SelectedUserIsEditor
        {
            get { return UserObj == null ? false : UserObj.IsMasterEditor ; }
            set
            {
                if (UserObj != null)
                {
                    UserObj.IsMasterEditor  = value;
                }
            }
        }

        [Display(Name = "Is Active")]
        public bool SelectedUserIsActive
        {
            get { return UserObj == null ? false : UserObj.IsActive ; }
            set
            {
                if (UserObj != null)
                {
                    UserObj.IsActive  = value;
                }
            }
        }

        [Display(Name = "Has Admin Approval")]
        public bool SelectedUserHasAdminApproval
        {
            get { return UserObj == null ? false : UserObj.HasAdminApproval; }
            set
            {
                if (UserObj != null)
                {
                    UserObj.HasAdminApproval  = value;
                }
            }
        }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "This is not a valid email address")]
        public string EmailAddress { get { return UserObj == null ? "" : UserObj.EmailAddress; } }

        [Required]
        [StringLength(255)]
        [Display(Name = "Display Name")]
        public string DisplayName { get { return UserObj == null ? "" : UserObj.DisplayName; } }

            [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get { return UserObj == null ? "" : UserObj.LastName; } }


        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get { return UserObj == null ? "" : UserObj.FirstName; } }

        public Guid WorkingGroupId { get; set; }

        [Display(Name = "Working Group")]
        public string WorkingGroupName { get; set; }


        public Guid UserId { get { return UserObj == null ? Guid.Empty : UserObj.Id; } }

        [Required]
        [StringLength(255)]
        [Display(Name = "UserName")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string UserName { get { return UserObj == null ? "" : UserObj.UserName; } }

        ////***********************************************************************************************



        public string SubmitButtonText
        {
            get
            {
                if (UserId != Guid.Empty)
                {
                    return "Update";
                }
                else
                {
                    return "Create";
                }
            }
        }

        public string Heading
        {
            get
            {
                if (UserId != Guid.Empty)
                {
                    return "Edit User";
                }
                else
                {
                    return "Create User";
                }
            }
        }

        public UserDomainObj CurrentUserObj { get; set; }
    }
}
