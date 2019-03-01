using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class ReleasedDocumentDomainObjBasic
    {

        public Guid Id { get; set; }

        public LessonUseDomainObjBasic LessonUseObj { get; set; }
        public DocumentDomainObj DocumentObj { get; set; }

        public string ModificationNotes { get; set; }

        public Guid DocumentId
        {
            get { return DocumentObj == null ? Guid.Empty : DocumentObj.BasicObj.Id; }
        }

        public string DocumentName
        {
            get { return DocumentObj == null ? "" : DocumentObj.Name; }
        }



    }
}
