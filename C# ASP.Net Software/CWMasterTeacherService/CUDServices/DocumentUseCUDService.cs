using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class DocumentUseCUDService
    {
        private IDocumentUseRepo _documentUseRepo;
        private ILessonRepo _lessonRepo;

        public DocumentUseCUDService(IDocumentUseRepo documentUseRepo, ILessonRepo lessonRepo)
        {
            _documentUseRepo = documentUseRepo;
            _lessonRepo = lessonRepo;
        }

        /// <summary>
        /// Anything that calls this should also call LessonCUDService.SetGroupMostRecentDocModDates
        /// </summary>
        /// <returns></returns>
        public DocumentUse CreateDocumentUse(Guid lessonId, Guid documentId, bool isReference, bool isInstructorOnly)
        {
            DocumentUse docUse = new DocumentUse();
            docUse.LessonId = lessonId;
            docUse.DocumentId = documentId;
            docUse.IsActive = true;
            docUse.IsReference = isReference;
            docUse.IsInstructorOnly = isInstructorOnly;
            docUse.IsVisibleToStudents = false;
            docUse.IsOutForEdit = false;

            //DateTime date = DateTime.Now;
            //docUse.DateModified = date;
            //docUse.DateChangesAccepted = date;

            _documentUseRepo.Insert(docUse);

            return docUse;
        }

        /// <summary>
        /// Anything that calls this should also call LessonCUDService.SetGroupMostRecentDocModDates
        /// </summary>
        public void CopyAllDocumentUses_FromLessonToLesson(Guid fromLessonId, Guid toLessonId)
        {
            List<DocumentUse> docUseList = _documentUseRepo.ActiveDocumentUsesForLesson(fromLessonId).ToList();
            foreach (var xDocUse in docUseList)
            {
                Document xDocument = xDocUse.Document;
                CreateDocumentUse(toLessonId, xDocument.Id, xDocUse.IsReference, xDocUse.IsInstructorOnly);
            }

        }


        public void SetIsActive(DocumentUse docUse, bool isActive)
        {
            docUse.IsActive = isActive;
            docUse.IsOutForEdit = false;
            //docUse.DateModified = DateTime.Now;
            _documentUseRepo.Update(docUse);
        }

        /// <summary>
        /// A similar function is done in DocumentCUDService.RemoveDocumentFromLesson.  That one can also set mirror booleans.
        /// </summary>
        public void SetIsActiveFromId(Guid docUseId, bool isActive)
        {
            DocumentUse docUse = _documentUseRepo.GetById(docUseId);
            if(docUse != null)
            {
                SetIsActive(docUse, isActive);
            }
        }


    }//End Class
}
