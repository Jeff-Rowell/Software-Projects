using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.DomainObjectBuilders
{
    public class StudentUserDomainObjBuilder
    {

        private StudentUserRepo _studentUserRepo;
        private WorkingGroupStudentRepo _workingGroupStudentRepo;


        public StudentUserDomainObjBuilder(StudentUserRepo studentUserRepo, WorkingGroupStudentRepo workingGroupStudentRepo)
        {
            _studentUserRepo = studentUserRepo;
            _workingGroupStudentRepo = workingGroupStudentRepo;
        }

        public static StudentUserDomainObjBasic BuildBasic(StudentUser dbObj)
        {
            StudentUserDomainObjBasic basicObj = new StudentUserDomainObjBasic();

            basicObj.Id = dbObj.Id;

            basicObj.FirstName = dbObj.FirstName;
            basicObj.LastName = dbObj.LastName;

            return basicObj;
        }

        public static StudentUserDomainObj Build (StudentUser dbObject)
        {
            StudentUserDomainObj domainObj = new StudentUserDomainObj();

            domainObj.StudentUserBasic = BuildBasic(dbObject);
            domainObj.EmailAddress = dbObject.EmailAddress;
            domainObj.ClassSectionStudents = dbObject.ClassSectionStudents.Select(x => ClassSectionStudentDomainObjBuilder.BuildBasic(x)).ToList();
            domainObj.UserName = dbObject.UserName;
            return domainObj;
        }

        public void SetLastLogin(Guid studentUserId)
        {
            StudentUser studentUser = _studentUserRepo.GetById(studentUserId);

            if(studentUser != null)
            {
                studentUser.LastLoginDate = DateTime.Now;
                _studentUserRepo.Update(studentUser);
            }
        }

        public StudentUserDomainObj BuildFromUserName(string userName)
        {
            StudentUser studentUser = _studentUserRepo.StudentUserByUserName(userName);
            return studentUser == null ? null : Build(studentUser);
        }

        public StudentUserDomainObj BuildFromId(Guid studentUserId)
        {
            StudentUser studentUser = _studentUserRepo.GetById(studentUserId);
            return studentUser == null ? null : Build(studentUser);
        }

        public List<StudentUserDomainObjBasic>StudentUsersForWorkingGroup(Guid workingGroupId)
        {
            List<WorkingGroupStudent> workingGroupStudentList = _workingGroupStudentRepo.WorkingGroupStudentsForWorkingGroup(workingGroupId);
            return workingGroupStudentList.Select(x => BuildBasic(x.StudentUser)).ToList();
        }
    }
}
