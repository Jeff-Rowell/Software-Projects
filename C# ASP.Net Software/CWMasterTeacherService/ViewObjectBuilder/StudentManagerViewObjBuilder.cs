using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class StudentManagerViewObjBuilder
    {
        private ClassSectionStudentDomainObjBuilder _classSectionStudentObjBuilder;

        public StudentManagerViewObjBuilder(ClassSectionStudentDomainObjBuilder classSectionStudentObjBuilder)
        {
            _classSectionStudentObjBuilder = classSectionStudentObjBuilder;
        }


        public StudentManagerViewObj Retrieve (Guid classSectionId, Guid selectedClassMeetingId)
        {
            StudentManagerViewObj viewObj = new StudentManagerViewObj();

            viewObj.StudentList = _classSectionStudentObjBuilder.AllClassSectionStudentsForClassSection(classSectionId);
            viewObj.ClassSectionId = classSectionId;
            viewObj.SelectedClassMeetingId = selectedClassMeetingId;

            return viewObj;

        }



    }
}
