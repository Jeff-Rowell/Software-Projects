using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ReleasedDocumentRepo: BaseRepo<ReleasedDocument>
    {
        public ReleasedDocumentRepo(): base(new MasterTeacherContext())
        {}

        public List<ReleasedDocument> ReleasedDocumentsForLessonUse(Guid lessonUseId)
        {
            var query = from x in _context.ReleasedDocuments
                        where x.LessonUseId == lessonUseId
                        select x;
            return query.ToList();
        }

        public List<Guid> ReleasedIdsForLessonUse(Guid lessonUseId)
        {
            return ReleasedDocumentsForLessonUse(lessonUseId).Select(x => x.Id).ToList();
        }
    }
}
