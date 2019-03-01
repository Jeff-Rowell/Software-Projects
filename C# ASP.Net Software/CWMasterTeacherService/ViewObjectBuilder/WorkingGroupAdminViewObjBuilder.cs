using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class WorkingGroupAdminViewObjBuilder
    {

        private WorkingGroupDomainObjBuilder _workingGroupObjBuilder;
        private UserDomainObjBuilder _userObjBuilder;

        public WorkingGroupAdminViewObjBuilder(WorkingGroupDomainObjBuilder workingGroupObjBuilder, UserDomainObjBuilder userObjBuilder)
        {
            _workingGroupObjBuilder = workingGroupObjBuilder;
            _userObjBuilder = userObjBuilder;
        }

        public WorkingGroupAdminViewObj Retrieve(UserDomainObj currentUserObj, Guid selectedWorkingGroupId, 
                                                    string message, bool doShowDeleteWorkingGroup)
        {
            WorkingGroupAdminViewObj viewObj = new WorkingGroupAdminViewObj();

            viewObj.CurrentUserObj = currentUserObj;

            viewObj.WorkingGroupObj = _workingGroupObjBuilder.BuildFromId(selectedWorkingGroupId);

            viewObj.WorkingGroupObjList = currentUserObj != null && currentUserObj.IsApplicationAdmin?
                                            _workingGroupObjBuilder.AllWorkingGroupsBasicList() :
                                            _workingGroupObjBuilder.SingleWorkingGroupBasicList(currentUserObj.WorkingGroupId);

            viewObj.Message = message;
            viewObj.DoShowDeleteWorkingGroup = doShowDeleteWorkingGroup;

            return viewObj;
        }



    }
}
