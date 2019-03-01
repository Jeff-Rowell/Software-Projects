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
    public class StudentHomeViewObjBuilder
    {
        private TermDomainObjBuilder _termObjBuilder;
        private ClassSectionDomainObjBuilder _classSectionObjBuilder;
        private UserDomainObjBuilder _userObjBuilder;
        private ClassSectionStudentDomainObjBuilder _classSectionStudentObjBuilder;
        private WorkingGroupDomainObjBuilder _workingGroupObjBuilder;
        private CourseDomainObjBuilder _courseObjBuilder;
        private ClassMeetingDomainObjBuilder _classMeetingObjBuilder;
        private ClassroomViewObjBuilder _classroomViewObjBuilder;

        public StudentHomeViewObjBuilder(TermDomainObjBuilder termObjBuilder, ClassSectionDomainObjBuilder classSectionObjBuilder,
                                           UserDomainObjBuilder userObjBuilder, ClassSectionStudentDomainObjBuilder classSectionStudentObjBuilder,
                                           WorkingGroupDomainObjBuilder workingGroupObjBuilder, CourseDomainObjBuilder courseObjBuilder,
                                           ClassMeetingDomainObjBuilder classMeetingObjBuilder, ClassroomViewObjBuilder classroomViewObjBuilder)
        {
            _termObjBuilder = termObjBuilder;
            _classSectionObjBuilder = classSectionObjBuilder;
            _userObjBuilder = userObjBuilder;
            _classSectionStudentObjBuilder = classSectionStudentObjBuilder;
            _workingGroupObjBuilder = workingGroupObjBuilder;
            _courseObjBuilder = courseObjBuilder;
            _classMeetingObjBuilder = classMeetingObjBuilder;
            _classroomViewObjBuilder = classroomViewObjBuilder;
        }

        public StudentHomeViewObj Retrieve(StudentUserDomainObj studentUserObj, Guid instructorUserId, Guid classSectionId,
                                             bool doShowRequestCourseAccess, string accessRequestMessage,
                                             bool doShowRequestGroupAccess, string courseRequestMessage, Guid displayedClassSectionId,
                                             Guid selectedClassMeetingId, bool doShowAllDates)
        {
            StudentHomeViewObj viewObj = new StudentHomeViewObj();
            List<CourseDomainObjBasic> courseList = new List<CourseDomainObjBasic>();

            List<WorkingGroupDomainObjBasic> workingGroupList = _workingGroupObjBuilder.WorkingGroupsForStudentUser(studentUserObj.Id);
            viewObj.WorkingGroupList = workingGroupList;

            if(workingGroupList.Count > 0)
            {
                courseList = _courseObjBuilder.AllAvailableCoursesForWorkingGroupList(workingGroupList.Select(x => x.Id).ToList());
            }

            viewObj.InstructorList = GetInstructorList(courseList);
            viewObj.SelectedInstructor = instructorUserId == Guid.Empty ? null : _userObjBuilder.BuildBasicFromId(instructorUserId);

            viewObj.ClassSectionList = instructorUserId == Guid.Empty ? new List<ClassSectionDomainObjBasic>() : _classSectionObjBuilder.ClassSectionsForUser(instructorUserId);
            viewObj.SelectedClassSection = classSectionId == Guid.Empty ? null : _classSectionObjBuilder.BuildBasicFromId(classSectionId);

            List<ClassSectionStudentDomainObjBasic> allClassSectionsForStudent = _classSectionStudentObjBuilder.AllClassSectionStudentsForStudent(studentUserObj.Id);
            viewObj.CurrentClassesClassSecStdt = allClassSectionsForStudent.Where(x => x.HasBeenApproved).ToList();
            viewObj.ClassesAwaitingApprovalClassSecStdt = allClassSectionsForStudent.Where(x => !x.HasBeenApproved && !x.HasBeenDenied).ToList();
            viewObj.ClassesDeniedClassSecStdt = allClassSectionsForStudent.Where(x => x.HasBeenDenied).ToList();

            if(displayedClassSectionId != Guid.Empty)
            {
                List<ClassMeetingDomainObjBasic> meetingList  = _classMeetingObjBuilder.GetClassMeetingBasicsForClassSection(displayedClassSectionId, doShowAllDates);
                foreach(var xMeeting in meetingList)
                {
                    if (xMeeting.Id == selectedClassMeetingId)
                    {
                        xMeeting.IsSelected = true;
                    }
                }
                viewObj.ClassMeetingList = meetingList;
            }
            else
            {
                viewObj.ClassMeetingList = new List<ClassMeetingDomainObjBasic>();
            }

            viewObj.DisplayedClassSectionId = displayedClassSectionId;
            ClassMeetingDomainObj classMeetingObj = _classMeetingObjBuilder.BuildFromId(selectedClassMeetingId);
            viewObj.SelectedClassMeetingObj = classMeetingObj;
            viewObj.LessonList = _classroomViewObjBuilder.GetClassroomDocsLessonList(classMeetingObj, false);

            viewObj.DoShowRequestCourseAccess = doShowRequestCourseAccess;
            viewObj.AccessRequestMessage = accessRequestMessage;
            viewObj.CourseRequestMessage = courseRequestMessage;

            viewObj.DoShowRequestGroupAccess = doShowRequestGroupAccess;
            viewObj.StudentUserObj = studentUserObj;
            viewObj.InstructorUserId = instructorUserId;
            viewObj.ClassSectionId = classSectionId;
            viewObj.ShowAllDates = doShowAllDates;
            return viewObj;

        }//End Retrieve

        private List<UserDomainObjBasic> GetInstructorList(List<CourseDomainObjBasic> courseList)
        {
            if(courseList.Count == 0)
            {
                return new List<UserDomainObjBasic>();
            }
            else
            {
                HashSet<Guid> userIdHash = new HashSet<Guid>(courseList.Select(x => x.UserId));
                return userIdHash.Select(x => _userObjBuilder.BuildBasicFromId(x)).ToList();
            }
        }



    }
}
