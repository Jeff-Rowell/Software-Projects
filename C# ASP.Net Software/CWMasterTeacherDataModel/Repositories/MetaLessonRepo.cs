using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class MetaLessonRepo : BaseRepo<MetaLesson>, IMetaLessonRepo
    {
        public MetaLessonRepo(MasterTeacherContext context): base (context)
        {

        }

        public void RemoveOrphanMetaLessons()
        {
            var query = from x in _context.MetaLessons
                        where x.Lessons.Count == 0
                        select x;
            List<Guid> orphanIdList = query.Select(x => x.Id).ToList();

            foreach(var xId in orphanIdList)
            {
                Delete(xId);
            }
        }


    }
}
