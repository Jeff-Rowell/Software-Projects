using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class ReleasedDocumentCUDService
    {
        private ReleasedDocumentRepo _releasedDocumentRepo;
        private LessonUseRepo _lessonUseRepo;
        private DocumentUseRepo _documentUseRepo;

        public ReleasedDocumentCUDService(ReleasedDocumentRepo releasedDocumentRepo,
                                             LessonUseRepo lessonUseRepo,
                                             DocumentUseRepo documentUseRepo)
        {
            _releasedDocumentRepo = releasedDocumentRepo;
            _lessonUseRepo = lessonUseRepo;
            _documentUseRepo = documentUseRepo;
        }

        public void CreateReleasedDocument(Guid documentId, Guid lessonUseId)
        {
            ReleasedDocument releasedDoc = new ReleasedDocument();
            releasedDoc.DocumentId = documentId;
            releasedDoc.LessonUseId = lessonUseId;
            releasedDoc.ModificationNotes = "_";

            _releasedDocumentRepo.Insert(releasedDoc);
        }

        public void DeleteReleasedDocument (Guid releasedDocumentId)
        {
            _releasedDocumentRepo.Delete(releasedDocumentId);
        }

        public void DeleteReleasedDocumentById(Guid releasedDocumentId)
        {
            ReleasedDocument releasedDoc = _releasedDocumentRepo.GetById(releasedDocumentId);
            _releasedDocumentRepo.Delete(releasedDocumentId);
        }

        public void DeleteAllReleasedDocumentsForLessonUse(Guid lessonUseId)
        {
            List<Guid> idList = _releasedDocumentRepo.ReleasedIdsForLessonUse(lessonUseId);
            foreach(var xId in idList)
            {
                _releasedDocumentRepo.Delete(xId);
            }
        }

        public void ReleaseAllDocumentsForLessonUse(Guid lessonUseId)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            List<Guid> releasedIdList = lessonUse.ReleasedDocuments.Select(x => x.DocumentId).ToList();
            if(lessonUse != null)
            {
                Guid lessonId = lessonUse.LessonId == null ? Guid.Empty : lessonUse.LessonId.Value;
                List<DocumentUse> docUseList = _documentUseRepo.TeachingDocumentUsesForLesson(lessonId).ToList();
                foreach(var xDocUse in docUseList)
                {
                    if(!releasedIdList.Contains(xDocUse.DocumentId)){
                        CreateReleasedDocument(xDocUse.DocumentId, lessonUseId);
                    }
                    
                }
            }
            

        }
    }
}
