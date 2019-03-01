using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class LessonPlanLinkRepo: BaseRepo<LessonPlanLink>
    {
        public LessonPlanLinkRepo(MasterTeacherContext context)
            : base(context)
        { }

        public IEnumerable<LessonPlanLink> LessonPlanLinksForMessage(Guid messageId)
        {
            var listQuery = from x in _context.LessonPlanLinks
                                             where x.MessageId == messageId
                                             select x;
            return listQuery.ToList();                                      
        }

    }
}
