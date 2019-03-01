using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// Provides retrieval services to get an instance of a MessageUseViewObj containing the desired data.
    /// </summary>
    public class CourseCopyAdminViewObjBuilder
    {
        private ICourseDomainObjBuilder _courseDomainObjBuilder;
        private IUserDomainObjBuilder _userDomainObjBuilder;
        private ITermDomainObjBuilder _termDomainObjBuilder;

        /// <summary>
        ///  Creates a CourseCopyService that uses the supplied builder when acquiring data.
        /// </summary>
        /// <param name="courseDomainObjBuilder">An ICourseDomainObjBuilder that is used when acquiring course data.</param>
        /// <param name="userDomainObjBuilder">An IUserDomainObjBuilder that is used when acquiring user data.</param>
        /// <param name="termDomainObjBuilder">An ITermDomainObjBuilder that is used when acquiring term data.</param>
        public CourseCopyAdminViewObjBuilder(ICourseDomainObjBuilder courseDomainObjBuilder, IUserDomainObjBuilder userDomainObjBuilder,
            ITermDomainObjBuilder termDomainObjBuilder)
        {
            _courseDomainObjBuilder = courseDomainObjBuilder;
            _userDomainObjBuilder = userDomainObjBuilder;
            _termDomainObjBuilder = termDomainObjBuilder;
        }

        /// <summary>
        ///  Retrieves a CourseCopyViewObj built using the given arguments.
        /// </summary>
        /// <param name="isMaster">Indicates if the course is the master course</param>
        /// <param name="successMessage">Message indicating success of copying the course</param>
        /// <param name="isAdmin">A boolean flag dictating whether the user is an Admin.</param>
        /// <param name="workingGroupId">The Id of the institudion associated with the course.</param> 
        /// <returns>A CourseCopyViewObj</returns>
        public CourseCopyAdminViewObj Retrieve(bool isMaster, string successMessage, UserDomainObj currentUserObj)
        {
            Guid workingGroupId = currentUserObj.WorkingGroupId;
            return new CourseCopyAdminViewObj(isMaster: isMaster, successMessage: successMessage,
                                            currentUserObj: currentUserObj,
                                            masterCourseForWorkingGroupList: getMasterCourseForWorkingGroupList(currentUserObj.WorkingGroupId),
                                            coursesAllForWorkingGroupList: getCoursesAllForWorkingGroupList(workingGroupId),
                                            usersForWorkingGroupList: getUsersForWorkingGroupList(workingGroupId),
                                            termsForWorkingGroupList: getTermsForWorkingGroupList(workingGroupId));
        }

        private IEnumerable<SelectListItem> getMasterCourseForWorkingGroupList(Guid workingGroupId)
        {
            List<CourseDomainObjBasic> masterCourseForWorkingGroupList = _courseDomainObjBuilder.GetMasterCourseForWorkingGroupList(workingGroupId);
            return new SelectList(masterCourseForWorkingGroupList, "Id", "DisplayName");
        }

        private IEnumerable<SelectListItem> getCoursesAllForWorkingGroupList(Guid workingGroupId)
        {
            List<CourseDomainObjBasic> coursesAllForWorkingGroupList = _courseDomainObjBuilder.GetCoursesAllForWorkingGroupList(workingGroupId);
            return new SelectList(coursesAllForWorkingGroupList, "Id", "DisplayName");
        }

        private IEnumerable<SelectListItem> getUsersForWorkingGroupList(Guid workingGroupId)
        {
            List<UserDomainObjBasic> usersForWorkingGroupList = _userDomainObjBuilder.GetAllUsersForWorkingGroupList(workingGroupId);
            return new SelectList(usersForWorkingGroupList, "Id", "DisplayName");
        }

        private IEnumerable<SelectListItem> getTermsForWorkingGroupList(Guid workingGroupId)
        {
            List<TermDomainObj> termsForWorkingGroupnList = _termDomainObjBuilder.GetTermsForWorkingGroupList(workingGroupId);
            return new SelectList(termsForWorkingGroupnList, "Id", "Name");
        }
    }
}
