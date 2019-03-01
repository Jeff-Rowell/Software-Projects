using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.DomainObjectBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService
{
    public class ClassSectionStudentDomainObjBuilder
    {
        private ClassSectionStudentRepo _classSectionStudentRepo;

        public ClassSectionStudentDomainObjBuilder(ClassSectionStudentRepo classSectionStudentRepo)
        {
            _classSectionStudentRepo = classSectionStudentRepo;
        }

        public static ClassSectionStudentDomainObjBasic BuildBasic(ClassSectionStudent dbObj)
        {
            ClassSectionStudentDomainObjBasic domainObj = new ClassSectionStudentDomainObjBasic();

            domainObj.Id = dbObj.Id;
            domainObj.StudentUserObj = StudentUserDomainObjBuilder.BuildBasic(dbObj.StudentUser);
            domainObj.ClassSectionObj = ClassSectionDomainObjBuilder.BuildBasic(dbObj.ClassSection);

            domainObj.HasBeenApproved = dbObj.HasBeenApproved;
            domainObj.HasBeenDenied = dbObj.HasBeenDenied;

            return domainObj;
        }

        public ClassSectionStudentDomainObjBasic BuildBasicFromId(Guid classSectionStudentId)
        {
            ClassSectionStudent classSectionStudent = _classSectionStudentRepo.GetById(classSectionStudentId);
            return classSectionStudent == null ? null : BuildBasic(classSectionStudent);
        }

        public List<ClassSectionStudentDomainObjBasic>AllClassSectionStudentsForStudent(Guid studentUserId)
        {
            List<ClassSectionStudent> list = _classSectionStudentRepo.AllClassSectionStudentsForStudent(studentUserId);
            return list.Count == 0 ? new List<ClassSectionStudentDomainObjBasic>() :  list.Select(x => BuildBasic(x)).ToList();
        }

        public List<ClassSectionStudentDomainObjBasic> AllClassSectionStudentsForClassSection(Guid classSectionId)
        {
            List<ClassSectionStudent> list = _classSectionStudentRepo.AllClassSectionStudentsForClassSection(classSectionId);
            return list.Count == 0 ? new List<ClassSectionStudentDomainObjBasic>() : list.Select(x => BuildBasic(x)).ToList();
        }


    }
}
