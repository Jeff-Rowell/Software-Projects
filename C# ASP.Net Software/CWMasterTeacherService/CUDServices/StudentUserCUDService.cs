using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class StudentUserCUDService
    {
        private StudentUserRepo _studentUserRepo;
        private WorkingGroupRepo _workingGroupRepo;
        private WorkingGroupStudentRepo _workingGroupStudentRepo;
        private ClassSectionStudentRepo _classSectionStudentRepo;
        private ClassSectionStudentCUDService _classSectionStudentCUDService;

        public StudentUserCUDService(StudentUserRepo studentUserRepo, WorkingGroupRepo workingGroupRepo,
                                        WorkingGroupStudentRepo workingGroupStudentRepo, ClassSectionStudentRepo classSectionStudentRepo,
                                        ClassSectionStudentCUDService classSectionStudentCUDService)
        {
            _studentUserRepo = studentUserRepo;
            _workingGroupRepo = workingGroupRepo;
            _workingGroupStudentRepo = workingGroupStudentRepo;
            _classSectionStudentRepo = classSectionStudentRepo;
            _classSectionStudentCUDService = classSectionStudentCUDService;
        }


        public StudentUser StudentUserEditOrCreateProfile(Guid studentUserId, string userName, string firstName, string lastName, string emailAddress)
        {
            StudentUser studentUser = new StudentUser();
            if (studentUserId != Guid.Empty)
            {
                studentUser = _studentUserRepo.GetById(studentUserId);
            }

            studentUser = SetStudentUserValues(studentUser: studentUser, userName: userName, firstName: firstName, lastName: lastName, emailAddress: emailAddress);
            if (studentUserId != Guid.Empty)
            {
                _studentUserRepo.Update(studentUser);
            }
            else
            {
                studentUser.CreationDate = DateTime.Now;
                _studentUserRepo.Insert(studentUser);
            }

            return studentUser;
        }

        private StudentUser SetStudentUserValues(StudentUser studentUser, string userName, string firstName, string lastName, string emailAddress)
        {
            studentUser.UserName = userName;
            studentUser.FirstName = firstName;
            studentUser.LastName = lastName;
            studentUser.EmailAddress = emailAddress;
            studentUser.LastLoginDate = DateTime.Now;
            return studentUser;
        }

        public string RequestGroupAccessReturnMessage(Guid studentUserId, string accessCode)
        {
            WorkingGroup requestedWorkingGroup = _workingGroupRepo.WorkingGroupForStudentAccessCode(accessCode);
            if (requestedWorkingGroup != null)
            {
                WorkingGroupStudent workingGroupStudent = _workingGroupRepo.WorkingGroupStudentFromIds(requestedWorkingGroup.Id, studentUserId);
                if (workingGroupStudent != null)
                {
                    return "You already have access to working group: " + requestedWorkingGroup.Name + ".";
                }
                else
                {
                    WorkingGroupStudent newWorkingGroupStudent = _workingGroupStudentRepo.CreateWorkingGroupStudent(requestedWorkingGroup.Id, studentUserId);
                    return newWorkingGroupStudent == null ? "Technical problem with creating access." : "You now have access to working group: " + requestedWorkingGroup.Name + ".";
                }
            }
            else
            {
                return "Access code is not valid.";
            }
        }

        public string RequestCourseAccessReturnMessage(Guid studentUserId, Guid classSectionId)
        {
            ClassSectionStudent classSectionStudent = _classSectionStudentRepo.ClassSectionStudentForStudentAndClassSection(studentUserId, classSectionId);
            if (classSectionStudent != null)
            {
                if (classSectionStudent.HasBeenApproved)
                {
                    return "You already have access to this class section.";
                }
                else if (classSectionStudent.HasBeenDenied)
                {
                    return "Your access request to this class section was denied.  Please speak with your instructor.";
                }
                else
                {
                    return "Your access request for this class section is still waiting for approval.  Speak to your instructor if you have questions.";
                }
            }
            else
            {
                _classSectionStudentCUDService.CreateClassSectionStudent(studentUserId, classSectionId);
                return "Course access request is awaiting instructor approval.";
            }

        }

    }
}
