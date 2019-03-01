using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ClassSectionStudentRepo : BaseRepo<ClassSectionStudent>
    {
        public ClassSectionStudentRepo() : base(new MasterTeacherContext())
        { }

        public List<ClassSectionStudent> AllClassSectionStudentsForStudent(Guid studentUserId)
        {
            var query = from x in _context.ClassSectionStudents
                        where x.StudentUserId == studentUserId
                        orderby x.ClassSection.Name
                        select x;
            return query.ToList();
        }

        public List<ClassSectionStudent> AllClassSectionStudentsForClassSection(Guid classSectionId)
        {
            var query = from x in _context.ClassSectionStudents
                        where x.ClassSectionId  == classSectionId
                        orderby x.StudentUser.LastName 
                        select x;
            return query.ToList();
        }

        public ClassSectionStudent ClassSectionStudentForStudentAndClassSection(Guid studentUserId, Guid classSectionId)
        {
            var query = from x in _context.ClassSectionStudents
                        where x.ClassSectionId == classSectionId && x.StudentUserId == studentUserId 
                        orderby x.StudentUser.LastName
                        select x;
            return query.FirstOrDefault();
        }


    }
}
