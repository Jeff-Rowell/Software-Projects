using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class ClassSectionAdminViewObjBuilder
    {
        private ICourseDomainObjBuilder _courseObjBuilder;
        private IUserDomainObjBuilder _userObjBuilder;
        private ITermDomainObjBuilder _termObjBuilder;
        private IClassMeetingDomainObjBuilder _classMeetingObjBuilder;
        private IClassSectionDomainObjBuilder _classSectionObjBuilder;

        public ClassSectionAdminViewObjBuilder(ICourseDomainObjBuilder courseObjBuilder,
                                    IUserDomainObjBuilder userObjBuilder,
                                    ITermDomainObjBuilder termObjBuilder,
                                    IClassMeetingDomainObjBuilder classMeetingObjBuilder,
                                    IClassSectionDomainObjBuilder classSectionObjBuilder)
        {
            _courseObjBuilder = courseObjBuilder;
            _userObjBuilder = userObjBuilder;
            _termObjBuilder = termObjBuilder;
            _classMeetingObjBuilder = classMeetingObjBuilder;
            _classSectionObjBuilder = classSectionObjBuilder;
        }

        public ClassSectionAdminViewObj RetrieveClassSectionViewObj(Guid currentUserId, Guid selectedUserId, Guid selectedTermId, Guid selectedCourseId,
                                                        Guid selectedClassSectionId,  bool addSingleClassMeeting)
        {
            ClassSectionAdminViewObj viewObj = new ClassSectionAdminViewObj();
            UserDomainObj currentUser = _userObjBuilder.BuildFromId(currentUserId);
            viewObj.CurrentUser = currentUser;

            Guid currentWorkingGroupId = currentUser.WorkingGroupId;
            viewObj.UserSelectList = new SelectList(_userObjBuilder.GetAllUsersForWorkingGroupList(currentWorkingGroupId), "Id", "DisplayName");
            viewObj.TermSelectList = new SelectList(_termObjBuilder.TermsForWorkingGroup(currentWorkingGroupId), "Id", "Name");
            if (selectedUserId != Guid.Empty)
            {
                if (selectedTermId != Guid.Empty)
                {
                    viewObj.CourseSelectList = new SelectList(_courseObjBuilder.CoursesForTermAndUser(selectedTermId, selectedUserId), "Id", "DisplayName");
                }
                else
                {
                    viewObj.CourseSelectList = new SelectList(_courseObjBuilder.GetIndividualCoursesForUserList(selectedUserId), "Id", "DisplayName");
                }
            }
            else if (selectedTermId != Guid.Empty)
            {
                viewObj.CourseSelectList = new SelectList(_courseObjBuilder.GetIndividualCoursesForTermList(selectedTermId), "Id", "DisplayName");
            }
            else
            {
                viewObj.CourseSelectList = new SelectList(_courseObjBuilder.GetCoursesAllForWorkingGroupList(currentWorkingGroupId), "Id", "DisplayName");
            }

            if (selectedClassSectionId != Guid.Empty)
            {
                viewObj.ClassMeetingSelectList = new SelectList(_classMeetingObjBuilder.GetClassMeetingsForClassSection(selectedClassSectionId), "Id", "DisplayName");
                ClassSectionDomainObj classSectionObj = _classSectionObjBuilder.BuildFromId(selectedClassSectionId);
                selectedCourseId = classSectionObj.CourseId;
                viewObj.SelectedClassSectionName = classSectionObj.Name;
                viewObj.SelectedClassSectionId = selectedClassSectionId;
            }
            else
            {
                viewObj.ClassMeetingSelectList = Enumerable.Empty<SelectListItem>();
            }

            if (selectedCourseId != Guid.Empty)
            {
                viewObj.ClassSectionSelectList = new SelectList(_classSectionObjBuilder.GetClassSectionsForCourseList(selectedCourseId), "Id", "Name");
                viewObj.SelectedCourseId = selectedCourseId;
            }
            else
            {
                viewObj.ClassSectionSelectList = Enumerable.Empty<SelectListItem>();
            }

            viewObj.MeetsSunday = false;
            viewObj.MeetsMonday = false;
            viewObj.MeetsTuesday = false;
            viewObj.MeetsWednesday = false;
            viewObj.MeetsThursday = false;
            viewObj.MeetsFriday = false;
            viewObj.MeetsSaturday = false;

            viewObj.StartTimeLocal = new DateTime().ToString("t");
            viewObj.EndTimeLocal = new DateTime().ToString("t");

            viewObj.AddSingleClassMeeting = addSingleClassMeeting;

            return viewObj;
        }


    }

}
