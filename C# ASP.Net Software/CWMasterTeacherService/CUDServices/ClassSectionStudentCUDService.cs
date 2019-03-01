using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class ClassSectionStudentCUDService
    {
        private ClassSectionStudentRepo _classSectionStudentRepo;

        public ClassSectionStudentCUDService(ClassSectionStudentRepo classSectionStudentRepo)
        {
            _classSectionStudentRepo = classSectionStudentRepo;
        }

        public ClassSectionStudent CreateClassSectionStudent(Guid studentUserId, Guid classSectionId)
        {
            ClassSectionStudent classSectionStudent = new ClassSectionStudent();
            classSectionStudent.StudentUserId = studentUserId;
            classSectionStudent.ClassSectionId = classSectionId;
            _classSectionStudentRepo.Insert(classSectionStudent);

            return classSectionStudent;
        }

        public void UpdateClassSectionStudent(Guid classSectionStudentId, bool hasBeenApproved, bool hasBeenDenied)
        {
            ClassSectionStudent classSectionStudent = _classSectionStudentRepo.GetById(classSectionStudentId);
            if(classSectionStudent != null)
            {
                classSectionStudent.HasBeenApproved = hasBeenApproved;
                classSectionStudent.HasBeenDenied = !hasBeenApproved && hasBeenDenied;

                _classSectionStudentRepo.Update(classSectionStudent);
            }
        }
    }
}
