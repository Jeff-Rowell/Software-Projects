using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class UserProfileViewObj
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public bool IsInstructor { get; set; }
        public string Message { get; set; }


        [Display(Name = "Select Working Group")]
        [Required(ErrorMessage = "You must select a Working Group.")]
        public Guid WorkingGroupId { get; set; }
        public Guid UserId { get; set; }

        public List<WorkingGroupDomainObjBasic> WorkingGroupObjList { get; set; }

        public IEnumerable<SelectListItem> WorkingGroupSelectList
        {
            get
            {
                return new SelectList(WorkingGroupObjList, "Id", "Name");
            }
        }
    }
}
