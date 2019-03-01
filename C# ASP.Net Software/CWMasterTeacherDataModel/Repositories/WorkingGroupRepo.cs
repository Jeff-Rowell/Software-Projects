using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class WorkingGroupRepo : BaseRepo<WorkingGroup>, IWorkingGroupRepo
    {
        public WorkingGroupRepo(MasterTeacherContext context)
            : base(context)
        { }

        public IEnumerable<WorkingGroup> GetWorkingGroups()
        {
            var listQuery = from x in _context.WorkingGroups
                                             orderby x.Name
                                             select x;
            return listQuery.ToList();                                      
        }

        public WorkingGroup WorkingGroupForInstructorAccessCode(string instructorAccessCode)
        {
            var listQuery = from x in _context.WorkingGroups
                            where x.InstructorAccessCode == instructorAccessCode 
                            orderby x.Name
                            select x;
            return listQuery.FirstOrDefault();
        }

        public WorkingGroup WorkingGroupForStudentAccessCode(string studentAccessCode)
        {
            var listQuery = from x in _context.WorkingGroups
                            where x.StudentAccessCode == studentAccessCode 
                            orderby x.Name
                            select x;
            return listQuery.FirstOrDefault();
        }

        public bool AccessCodeIsBeingUsed(string accessCode)
        {
            var listQuery = from x in _context.WorkingGroups
                            where x.StudentAccessCode == accessCode || x.InstructorAccessCode == accessCode 
                            orderby x.Name
                            select x;
            return listQuery.Count() > 0;
        }

        public List<WorkingGroup>WorkingGroupsForStudentUser(Guid studentUserId)
        {
            var listQuery = from x in _context.WorkingGroupStudents
                            where x.StudentUserId == studentUserId
                            select x;
            return listQuery.Select(x => x.WorkingGroup).ToList();
        }

        public WorkingGroupStudent WorkingGroupStudentFromIds(Guid workingGroupId, Guid studentUserId)
        {
            var query = from x in _context.WorkingGroupStudents
                        where x.WorkingGroupId == workingGroupId && x.StudentUserId == studentUserId
                        select x;
            return query.FirstOrDefault();

        }





    }//End Class
}
