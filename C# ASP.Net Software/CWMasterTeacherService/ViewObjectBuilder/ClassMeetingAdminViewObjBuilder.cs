using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel.Interfaces;


namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// ClassMeetingService acts as an intermediate layer between the controller/factories and the database.
    /// It generates a view object to be used by the factory by gathering needed parameters from domain
    /// object builders. 
    /// </summary>
    public class ClassMeetingAdminViewObjBuilder
    {
        IClassMeetingDomainObjBuilder _classMeetingBuilder;
        IUserDomainObjBuilder _userBuilder;
        ITermDomainObjBuilder _termBuilder;
        ICourseDomainObjBuilder _courseBuilder;
        IClassSectionDomainObjBuilder _classSectionBuilder;

        /// <summary>
        /// Constructor that takes in the needed domain object builders as parameters.
        /// </summary>
        /// <param name="classMeetingBuilder"></param>
        /// <param name="userBuilder"></param>
        /// <param name="termBuilder"></param>
        /// <param name="courseBuilder"></param>
        /// <param name="classSectionBuilder"></param>
        public ClassMeetingAdminViewObjBuilder(IClassMeetingDomainObjBuilder classMeetingBuilder, IUserDomainObjBuilder userBuilder,
            ITermDomainObjBuilder termBuilder, ICourseDomainObjBuilder courseBuilder, IClassSectionDomainObjBuilder classSectionBuilder)
        {
            _classMeetingBuilder = classMeetingBuilder;
            _userBuilder = userBuilder;
            _termBuilder = termBuilder;
            _courseBuilder = courseBuilder;
            _classSectionBuilder = classSectionBuilder;
        }

        /// <summary>
        /// Generates and returns a ClassMeetingViewObj
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="selectedUserId"></param>
        /// <param name="selectedTermId"></param>
        /// <param name="selectedClassSectionId"></param>
        /// <param name="selectedCourseId"></param>
        /// <param name="selectedClassMeetingId"></param>
        /// <returns>A full constructed classMeetingViewObj</returns>
        public ClassMeetingAdminViewObj buildClassMeetingViewObj(Guid currentUserId, Guid selectedUserId, 
            Guid selectedTermId, Guid selectedClassSectionId, Guid selectedCourseId, Guid selectedClassMeetingId,
            bool addSingleClassMeeting, bool showEditNoClass, bool showRenameClassSection, bool showCreateMirrorClassSection)
        {
            ClassMeetingAdminViewObj viewObj = new ClassMeetingAdminViewObj();
            UserDomainObj currentUserObj = _userBuilder.BuildFromId(currentUserId);
            viewObj.CurrentUserObj = currentUserObj;

            viewObj.SelectedUserId = selectedUserId;
            viewObj.SelectedTermId = selectedTermId;

            Guid currentWorkingGroupId = currentUserObj.WorkingGroupId;

            List<UserDomainObjBasic>userList = _userBuilder.GetAllUsersForWorkingGroupList(currentWorkingGroupId);
            List<TermDomainObj>termList = _termBuilder.GetTermsForWorkingGroupList(currentWorkingGroupId);
            viewObj.genUserAndTermSelectLists(userList, termList);

            if (selectedUserId != Guid.Empty)
            {
                if (selectedTermId != Guid.Empty)
                {
                    viewObj.generateCourseSelectList(_courseBuilder.GetCoursesForTermAndUserList(selectedTermId, selectedUserId));
                }
                else
                {
                    viewObj.generateCourseSelectList(_courseBuilder.GetIndividualCoursesForUserList(selectedUserId));
                }
            }
            else if (selectedTermId != Guid.Empty)
            {
                viewObj.generateCourseSelectList(_courseBuilder.GetIndividualCoursesForTermList(selectedTermId));
            }
            else
            {
                viewObj.generateCourseSelectList(_courseBuilder.GetCoursesAllForWorkingGroupList(currentWorkingGroupId));
            }

            if (selectedClassSectionId != Guid.Empty)
            {
                viewObj.genClassMeetingSelectList(_classMeetingBuilder.GetClassMeetingsForClassSection(selectedClassSectionId));
                ClassSectionDomainObjBasic classSection = _classSectionBuilder.BuildBasicFromId(selectedClassSectionId);
                selectedCourseId = classSection.CourseId;
                viewObj.SelectedClassSectionName = classSection.Name;
                viewObj.SelectedClassSectionId = selectedClassSectionId;
                if (showCreateMirrorClassSection)
                {
                    viewObj.PossibleMirrorCourses = _courseBuilder.CoursesThatShareMaster(classSection.CourseId);
                }
            }
            else
            {
                viewObj.genClassMeetingSelectList(null);
            }
            
            if (selectedCourseId != Guid.Empty)
            {
                viewObj.genClassSectionSelectList(_classSectionBuilder.GetClassSectionsForCourseList(selectedCourseId));
                viewObj.SelectedCourseId = selectedCourseId;
            }
            else
            {
                viewObj.genClassSectionSelectList(null);
            }

            if (selectedClassMeetingId != Guid.Empty)
            {
                ClassMeetingDomainObj classMeeting = _classMeetingBuilder.BuildFromId(selectedClassMeetingId);
                viewObj.SelectedClassMeetingHasNoClass = classMeeting.IsNoClass;
                viewObj.SelectedClassMeetingId = selectedClassMeetingId;
                viewObj.classMeetingDomainObj = classMeeting;
                viewObj.setStartAndEndTime(classMeeting);
               
                if (classMeeting.Comment != null && classMeeting.Comment.Length > 0)
                {
                    viewObj.NoClassComment = classMeeting.Comment;
                }
                else
                {
                    viewObj.NoClassComment = "NA";
                }
            }



            //pass-through parameters          
            viewObj.AddSingleClassMeeting = addSingleClassMeeting;
            viewObj.ShowEditNoClass = showEditNoClass;
            viewObj.ShowRenameClassSection = showRenameClassSection;
            viewObj.ShowCreateMirrorClassSection = showCreateMirrorClassSection;

            return viewObj;
        }
    }
}
