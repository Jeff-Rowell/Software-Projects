using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.DomainObjectBuilders
{
    public class ReleasedDocumentDomainObjBuilder
    {
        private ReleasedDocumentRepo _releasedDocRepo;

        public ReleasedDocumentDomainObjBuilder(ReleasedDocumentRepo releasedDocRepo)
        {
            _releasedDocRepo = releasedDocRepo;
        }

        public static ReleasedDocumentDomainObjBasic BuildBasic(ReleasedDocument dbObj)
        {
            ReleasedDocumentDomainObjBasic domainObj = new ReleasedDocumentDomainObjBasic();

            domainObj.Id = dbObj.Id;
            domainObj.LessonUseObj = LessonUseDomainObjBuilder.BuildBasic(dbObj.LessonUse);
            domainObj.DocumentObj = DocumentDomainObjBuilder.Build(dbObj.Document);
            domainObj.ModificationNotes = dbObj.ModificationNotes;

            return domainObj;
        }

        public List<ReleasedDocumentDomainObjBasic>ReleasedDocumentsForLessonUse(Guid lessonUseId)
        {
            List<ReleasedDocument> releasedDocList = _releasedDocRepo.ReleasedDocumentsForLessonUse(lessonUseId);
            return releasedDocList.Select(x => BuildBasic(x)).ToList();
        }
    }
}
