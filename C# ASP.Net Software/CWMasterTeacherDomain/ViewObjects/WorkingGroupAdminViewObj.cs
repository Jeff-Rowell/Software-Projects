using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class WorkingGroupAdminViewObj
    {
        public WorkingGroupAdminViewObj()
        { }

        public UserDomainObj CurrentUserObj { get; set; }

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


        public List<WorkingGroupDomainObjBasic>WorkingGroupObjList { get; set; }

        public IEnumerable<SelectListItem> WorkingGroupSelectList
        {
            get
            {
                return new SelectList(WorkingGroupObjList, "Id", "Name");
            }
        }


        public WorkingGroupDomainObj WorkingGroupObj { get; set; }

        public Guid WorkingGroupId
        {
            get { return WorkingGroupObj == null ? Guid.Empty : WorkingGroupObj.Id; }
        }

        public string WorkingGroupName
        {
            get { return WorkingGroupObj == null ? "" : WorkingGroupObj.Name; }
        }

        public string StudentAccessCode
        {
            get { return WorkingGroupObj == null ? "" : WorkingGroupObj.StudentAccessCode; }
        }

        public string InstructorAccessCode
        {
            get { return WorkingGroupObj == null ? "" : WorkingGroupObj.InstructorAccessCode ; }
        }

        public bool DoAllowCreate
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsApplicationAdmin; }
        }

        public string SubmitButtonText
        {
            get
            {
                if (WorkingGroupId != Guid.Empty)
                {
                    return "Update Working Group";
                }
                else
                {
                    return "Create New Working Group";
                }
            }
        }

        public string DropdownDefaultText
        {
            get { return DoAllowCreate ? "Create New Working Group" : "Selecte Working Group"; }

        }

        public string Message { get; set; }

        public bool DoShowDeleteWorkingGroup { get; set; }
    }
}
