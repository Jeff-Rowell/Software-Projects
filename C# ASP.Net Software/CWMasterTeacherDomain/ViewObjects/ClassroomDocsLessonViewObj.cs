using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class ClassroomDocsLessonViewObj
    {
        public List<DocumentUseDomainObj> ReferenceDocs { get; set; }

        public List<DocumentUseDomainObj> TeachingDocs { get; set; }

        public List<DocumentUseDomainObj> InstructorOnlyDocs { get; set; }

        public List<ReleasedDocumentDomainObjBasic> ReleasedDocs { get; set; }

        public LessonUseDomainObj LessonUseObj { get; set; }

        public Guid LessonUseId
        {
            get { return LessonUseObj == null ? Guid.Empty : LessonUseObj.Id; }
        }

        public Guid LessonId
        {
            get { return LessonUseObj == null ? Guid.Empty : LessonUseObj.LessonId; }
        }



        public string LessonName { get; set; }

        private List<Guid> ReleasedDocIds
        {
            get { return ReleasedDocs.Select(x => x.DocumentId).ToList(); }
        }

        public List<DocumentUseDomainObj> UnreleasedReferenceDocs
        {
            get
            {
                List<DocumentUseDomainObj> returnList = new List<DocumentUseDomainObj>();
                foreach (var xDoc in ReferenceDocs)
                {
                    if (!ReleasedDocIds.Contains(xDoc.DocumentId))
                    {
                        returnList.Add(xDoc);
                    }
                }
                return returnList;
            }
        }

        public List<DocumentUseDomainObj> UnreleasedTeachingDocs
        {
            get
            {
                List<DocumentUseDomainObj> returnList = new List<DocumentUseDomainObj>();
                foreach(var xDoc in TeachingDocs)
                {
                    if (!ReleasedDocIds.Contains(xDoc.DocumentId))
                    {
                        returnList.Add(xDoc);
                    }
                }
                return returnList;
            }
        }


    }
}
