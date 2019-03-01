using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.DomainObjectBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class AdminResetPasswordViewObjBuilder
    {
        private UserDomainObjBuilder _userObjBuilder;
        private StudentUserDomainObjBuilder _studentUserObjBuilder;

        public AdminResetPasswordViewObjBuilder(UserDomainObjBuilder userObjBuilder, StudentUserDomainObjBuilder studentUserObjBuilder)
        {
            _userObjBuilder = userObjBuilder;
            _studentUserObjBuilder = studentUserObjBuilder;
        }

        public AdminResetPasswordViewObj Retrieve(UserDomainObj currentUserObj, bool isInstructorNotStudent, Guid selectedUserId, string userName, string message)
        {
            AdminResetPasswordViewObj viewObj = new AdminResetPasswordViewObj();
            viewObj.CurrentUserObj = currentUserObj;
            viewObj.WorkingGroupId = currentUserObj.WorkingGroupId;
            viewObj.IsInstructorNotStudent = isInstructorNotStudent;

            if(isInstructorNotStudent)
            {
                UserDomainObj userObj = new UserDomainObj();
                if(selectedUserId != Guid.Empty)
                {
                    userObj = _userObjBuilder.BuildFromId(selectedUserId);
                }
                else if (userName != null && userName.Length > 0)
                {
                    userObj = _userObjBuilder.BuildFromUserName(userName);
                }
                viewObj.SelectedInstructorUserObj = userObj;

                viewObj.InstructorList = _userObjBuilder.GetAllUsersForWorkingGroupList(currentUserObj.WorkingGroupId);
            }
            else
            {
                StudentUserDomainObj studentUserObj = new StudentUserDomainObj();
                if (selectedUserId != Guid.Empty)
                {
                    studentUserObj = _studentUserObjBuilder.BuildFromId(selectedUserId);
                }
                else if (userName != null && userName.Length > 0)
                {
                    studentUserObj = _studentUserObjBuilder.BuildFromUserName(userName);
                }
                viewObj.SelectedStudentUserObj = studentUserObj;

                viewObj.StudentList = _studentUserObjBuilder.StudentUsersForWorkingGroup(currentUserObj.WorkingGroupId);
            }

            viewObj.message = message;

            return viewObj;
        }
    }
}
