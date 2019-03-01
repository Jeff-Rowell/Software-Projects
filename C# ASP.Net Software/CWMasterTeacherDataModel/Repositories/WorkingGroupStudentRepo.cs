using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class WorkingGroupStudentRepo: BaseRepo<WorkingGroupStudent>
    {
        public WorkingGroupStudentRepo(): base(new MasterTeacherContext())
        { }


        public WorkingGroupStudent CreateWorkingGroupStudent(Guid workingGroupId, Guid studentUserId)
        {
            WorkingGroupStudent workingGroupStudent = new WorkingGroupStudent();
            workingGroupStudent.WorkingGroupId = workingGroupId;
            workingGroupStudent.StudentUserId = studentUserId;

            Insert(workingGroupStudent);

            return workingGroupStudent;
        }

        public List<WorkingGroupStudent>WorkingGroupStudentsForWorkingGroup(Guid workingGroupId)
        {
            var query = from x in _context.WorkingGroupStudents
                        where x.WorkingGroupId == workingGroupId
                        select x;
            return query.ToList();
        }
    }
}
