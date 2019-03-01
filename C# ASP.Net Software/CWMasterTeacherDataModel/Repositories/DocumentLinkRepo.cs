using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class DocumentLinkRepo : BaseRepo<DocumentLink>
    {
        public DocumentLinkRepo(MasterTeacherContext context)
            : base(context)
        { }

        public IEnumerable<DocumentLink> DocumentLinksForMessage(Guid messageId)
        {
            var listQuery = from x in _context.DocumentLinks
                                             where x.MessageId == messageId
                                             orderby x.Document.Name
                                             select x;
            return listQuery.ToList();                                      
        }

    }
}
