using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel;
using System.Web.Mvc;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// Returns an instance of the CourseCreate View Object with appropriate information
    /// constructed from a CourseCreate Domain Object.
    /// </summary>
    public class CourseCreateAdminViewObjBuilder
    {

        private IWorkingGroupDomainObjBuilder _workingGroupObjBuilder;
        private ICourseDomainObjBuilder _courseObjBuilder;
        private IUserDomainObjBuilder _userObjBuilder;
        private ITermDomainObjBuilder _termObjBuilder;

        /// <summary>
        /// Constructs an instance of CourseCreateService from a series of object builders
        /// for which a CourseCreate Domain Object should be built.
        /// </summary>
        /// <param name="workingGroupBuilder"></param>
        /// <param name="courseBuilder"></param>
        /// <param name="userBuilder"></param>
        /// <param name="termBuilder"></param>
        public CourseCreateAdminViewObjBuilder(IWorkingGroupDomainObjBuilder workingGroupBuilder,
                                     ICourseDomainObjBuilder courseBuilder,
                                     IUserDomainObjBuilder userBuilder,
                                     ITermDomainObjBuilder termBuilder)
        {
            _workingGroupObjBuilder = workingGroupBuilder;
            _courseObjBuilder = courseBuilder;
            _userObjBuilder = userBuilder;
            _termObjBuilder = termBuilder;
           
        }

        /// <summary>
        ///  Retrieves a CourseCreateViewObj built using the given arguments.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="workingGroupId"></param>
        /// <param name="userId"></param>
        /// <param name="termId"></param>
        /// <param name="message"></param>
        /// <param name="showDeleteCourse"></param>
        /// <param name="showNewWorkingGroup"></param>
        /// <param name="showDeleteWorkingGroup"></param>
        /// <param name="showAllCourses"></param>
        /// <param name="isCourseDeletable"></param>
        /// <param name="showRenameWorkingGroup"></param>
        /// <param name="narrativeMessage"></param>
        /// <returns></returns>
        public CourseCreateAdminViewObj Retrieve(Guid courseId, Guid workingGroupId, Guid userId, Guid termId,
                                                    string message, bool showDeleteCourse, bool showNewWorkingGroup, bool showDeleteWorkingGroup,
                                                    bool showAllCourses, bool isCourseDeletable, bool showRenameWorkingGroup, string narrativeMessage,
                                                    string currentUserName)
        {

            List<WorkingGroupDomainObjBasic> workingGroupList = _workingGroupObjBuilder.AllWorkingGroupsBasicList();
          
            List<UserDomainObjBasic> userList = new List<UserDomainObjBasic>();
            List<TermDomainObj> termList = new List<TermDomainObj>();
            List<CourseDomainObjBasic> courseList = new List<CourseDomainObjBasic>();

            UserDomainObj currentUser = _userObjBuilder.BuildFromUserName(currentUserName);
            if (workingGroupId == Guid.Empty)
            {
                workingGroupId = currentUser.WorkingGroupId;
            }

            string workingGroupName = null;

            if (workingGroupId != Guid.Empty)
            {
                if (!showNewWorkingGroup)
                {
                    workingGroupName = _workingGroupObjBuilder.BuildFromId(workingGroupId).WorkingGroupBasicObj.Name;
                    userList = _userObjBuilder.GetAllUsersForWorkingGroupList(workingGroupId);
                    termList = _termObjBuilder.GetTermsForWorkingGroupList(workingGroupId);
                }
                else if (showDeleteWorkingGroup)
                {
                    workingGroupName = _workingGroupObjBuilder.BuildFromId(workingGroupId).WorkingGroupBasicObj.Name;
                }

                if (termId != Guid.Empty)
                {
                    if (showAllCourses)
                    {
                        courseList = _courseObjBuilder.GetCoursesForTermWorkingGroupList(termId, workingGroupId);
                    }
                    else
                    {
                        courseList = _courseObjBuilder.GetMasterCourseForWorkingGroupList(workingGroupId);
                    }
                }
                else
                {
                    if (showAllCourses)
                    {
                        courseList = _courseObjBuilder.GetCoursesAllForWorkingGroupList(workingGroupId);
                    }
                    else
                    {
                        courseList = _courseObjBuilder.GetMasterCourseForWorkingGroupList(workingGroupId);
                    }
                }
            }

            string courseName = "";
            string courseDisplayName = "";

            if (courseId != Guid.Empty)
            {
                CourseDomainObj course = _courseObjBuilder.BuildFromId(courseId);
                courseDisplayName = course.CourseDomainObjBasic.DisplayName;
                courseName = course.CourseDomainObjBasic.Name;
            }

            CourseCreateAdminViewObj viewObj = new CourseCreateAdminViewObj();


            viewObj.WorkingGroupSelectList = new SelectList(workingGroupList, "Id", "Name");

            viewObj.UserSelectList = new SelectList(userList, "Id", "FullName");
            viewObj.TermSelectList = new SelectList(termList, "Id", "Name");
            List<CourseDomainObjBasic> orderedCourseList = courseList.OrderByDescending(x => x.TermEndDate).ThenBy(x => x.Name).ToList();
            viewObj.CourseSelectList = new SelectList(orderedCourseList, "Id", "DisplayName");

            viewObj.CourseId = courseId;
            viewObj.WorkingGroupId = workingGroupId;
            viewObj.UserId = userId;
            viewObj.TermId = termId;

            viewObj.Message = message;
            viewObj.ShowDeleteCourse = showDeleteCourse;
            viewObj.ShowNewWorkingGroup = showNewWorkingGroup;
            viewObj.ShowAllCourses = showAllCourses;
            viewObj.IsCourseDeletable = isCourseDeletable;
            viewObj.ShowRenameWorkingGroup = showRenameWorkingGroup;
            viewObj.NarrativeMessage = narrativeMessage;

            viewObj.ShowDeleteWorkingGroup = showDeleteWorkingGroup;

            viewObj.WorkingGroupName = workingGroupName;
            viewObj.CourseName = courseName;
            viewObj.CourseDisplayName = courseDisplayName;

            viewObj.CurrentUser = currentUser;

            return viewObj;

        }
    }
}
