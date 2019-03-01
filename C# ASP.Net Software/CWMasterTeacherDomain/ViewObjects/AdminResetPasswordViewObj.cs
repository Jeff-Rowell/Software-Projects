using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class AdminResetPasswordViewObj
    {
        public Guid WorkingGroupId { get; set; }

        [Display(Name = "Instructor, not Student")]
        public bool IsInstructorNotStudent { get; set; }
        public string message { get; set; }

        [Display(Name = "Select User")]
        public Guid SelectedUserId { get; set; }

        [Display(Name = "Select Instructor")]
        public UserDomainObj SelectedInstructorUserObj { get; set; }
        public List<UserDomainObjBasic>InstructorList { get; set; }
        public Guid SelectedInstructorUserId
        {
            get { return SelectedInstructorUserObj == null ? Guid.Empty : SelectedInstructorUserObj.Id; }
        }

        [Display(Name = "Select Student")]
        public StudentUserDomainObj SelectedStudentUserObj { get; set; }
        public List<StudentUserDomainObjBasic> StudentList { get; set; }

        public Guid SelectedStudentUserId
        {
            get { return SelectedStudentUserObj == null ? Guid.Empty : SelectedStudentUserObj.Id; }
        }

        public IEnumerable<SelectListItem> UserSelectList
        {
            get
            {
                if(IsInstructorNotStudent)
                {
                    return InstructorList == null ? new SelectList(new List<UserDomainObjBasic>(), "Id", "FullName") : new SelectList(InstructorList, "Id", "FullName");
                }
                else
                {
                    return StudentList == null ? new SelectList(new List<UserDomainObjBasic>(), "Id", "FullName") : new SelectList(StudentList, "Id", "FullName");
                }
            }
        }

        [Display(Name = "Full Name")]
        public string SelectedUserFullName
        {
            get
            {
                if (IsInstructorNotStudent)
                {
                    return SelectedInstructorUserObj == null ? "" : SelectedInstructorUserObj.FullName;
                }
                else
                {
                    return SelectedStudentUserObj == null ? "" : SelectedStudentUserObj.FullName;
                }
            }
        }

        [Display(Name = "User Name")]
        public string UserName
        {
            get
            {
                if (IsInstructorNotStudent)
                {
                    return SelectedInstructorUserObj == null ? "" : SelectedInstructorUserObj.UserName;
                }
                else
                {
                    return SelectedStudentUserObj == null ? "" : SelectedStudentUserObj.UserName;
                }
            }
        }

        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public UserDomainObj CurrentUserObj { get; set; }

        public bool UserIsEditor
        {
            get { return CurrentUserObj.IsMasterEditor; }
        }

        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

        public string Heading
        {
            get
            {
                if(IsInstructorNotStudent)
                {
                    return "Reset Instructor Password";
                }
                else
                {
                    return "Reset Student Password";
                }
            }
        }


    }
}
