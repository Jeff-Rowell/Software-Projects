using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class StudentUserRepo: BaseRepo<StudentUser>
    {

        public StudentUserRepo() : base(new MasterTeacherContext())
        {

        }

        public StudentUser StudentUserByUserName(string studentUserName)
        {
            var listQuery = from x in _context.StudentUsers
                            where x.UserName == studentUserName
                            select x;
            return listQuery.FirstOrDefault();
        }

        public List<WorkingGroupStudent> WorkingGroupStudentsForStudent(Guid studentUserId)
        {
            var query = from x in _context.WorkingGroupStudents
                        where x.StudentUserId == studentUserId
                        select x;
            return query.ToList();
        }
    }
}
