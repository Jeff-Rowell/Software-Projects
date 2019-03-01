using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class UserProfileViewObjBuilder
    {
        private IWorkingGroupDomainObjBuilder _workingGroupObjBuilder;

        public UserProfileViewObjBuilder(IWorkingGroupDomainObjBuilder workingGroupObjBuilder)
        {
            _workingGroupObjBuilder = workingGroupObjBuilder;

        }

        public UserProfileViewObj RetrieveUserProfileViewObj(string userName, string email, Guid userId, bool isInstructor,
                                                            string firstName = "", string lastName = "", string displayName = "",
                                                            Guid workingGroupId = new Guid(), string message = "")
        {
            UserProfileViewObj viewObj = new UserProfileViewObj();
            viewObj.FirstName = firstName;
            viewObj.LastName = lastName ;
            viewObj.DisplayName = displayName ;
            viewObj.WorkingGroupId = workingGroupId ;
            viewObj.Message = message;
            viewObj.IsInstructor = isInstructor;


            List<WorkingGroupDomainObjBasic> workingGroupList = _workingGroupObjBuilder.AllWorkingGroupsBasicList();
            viewObj.UserName = userName;
            viewObj.Email = email;

            if(workingGroupId == Guid.Empty)
            {
                viewObj.WorkingGroupObjList = workingGroupList;
            }


            return viewObj;

        }



    }
}
