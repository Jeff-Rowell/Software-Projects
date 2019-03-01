using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class MetaCourseRepo : BaseRepo<MetaCourse>, IMetaCourseRepo
    {
        public MetaCourseRepo (MasterTeacherContext context): base(context)
        {

        }

        public IEnumerable <MetaCourse> MetaCoursesForWorkingGroup(Guid workingGroupId)
        {
            var query = from x in _context.MetaCourses
                        where x.WorkingGroupId == workingGroupId
                        select x;

            return query.ToList();
        }

    }
}
