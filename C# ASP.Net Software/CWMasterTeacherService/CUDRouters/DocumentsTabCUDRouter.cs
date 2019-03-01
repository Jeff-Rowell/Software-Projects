using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherService.CUDRouters
{
    public class DocumentsTabCUDRouter
    {
        private const string _containerName = "msudenver";

        private DocumentCUDService _documentCUDService;
        private DocumentDomainObjBuilder _documentObjBuilder;
        private LessonCUDService _lessonCUDService;

        public DocumentsTabCUDRouter(DocumentCUDService documentCUDService, DocumentDomainObjBuilder documentObjBuilder,
                                        LessonCUDService lessonCUDService)
        {
            _documentCUDService = documentCUDService;
            _documentObjBuilder = documentObjBuilder;
            _lessonCUDService = lessonCUDService;
        }


        /// <summary>
        /// Sets the IsReference and IsInstructorOnly booleans on a DocumentUse
        /// </summary>
        public void MoveDocument(Guid documentUseId, bool isReference, bool isInstructorOnly)
        {
            _documentCUDService.MoveDocument(documentUseId, isReference, isInstructorOnly);
        }

        /// <summary>
        /// Sets the IsActive boolean on a DocumentUse to false
        /// Dates and mirrors are updated in the service
        /// </summary>
        public void RemoveDocumentFromLesson(Guid documentUseId)
        {
            _documentCUDService.RemoveDocumentFromLesson(documentUseId);
        }

        /// <summary>
        /// Looks for a DocumentUse for Lesson and Document.  If one is found, it is activated, if not, one is created. 
        /// </summary>
        public void AddDocumentToLesson(Guid lessonId, Guid documentId)
        {
            _documentCUDService.AddDocumentToLesson(lessonId, documentId);
        }
        /// <summary>
        /// Sets IsOutForEdit on a documentUse
        /// </summary>
        public void SetIsOutForEdit(Guid documentUseId, bool isOutForEdit)
        {
            _documentCUDService.SetIsOutForEdit(documentUseId, false);
        }

        public void CreateOrUpdateDocument(string name, Guid lessonId, Guid userId, string fileName, bool isReference,
                                bool isInstructorOnly, Guid documentUseId, string modificationNotes)
        {
            _documentCUDService.CreateOrUpdateDocument(name: name, lessonId: lessonId,
                                                        userId:  userId, fileName: fileName, 
                                                        isReference: isReference, isInstructorOnly:  isInstructorOnly,
                                                        documentUseId: documentUseId, modificationNotes: modificationNotes);
        }

        public void AddPdfCopy(Guid documentUseId, string fileNameOfPdfCopy)
        {
            _documentCUDService.AddPdfCopy(documentUseId, fileNameOfPdfCopy);
        }

        public void RenameDocument(Guid documentUseId, string newName, string newModificationNotes)
        {
            _documentCUDService.RenameDocument(documentUseId, newName, newModificationNotes);
        }

        public bool ValidateExtension(string fileName)
        {
            return _documentCUDService.ValidateExtension(fileName);
        }

        public bool ValidateIsPdf(string fileName)
        {
            return _documentCUDService.ValidateIsPdf(fileName);
        }

        public string UserFileNameToStorageFileName(string userFileName)
        {
            return _documentCUDService.UserFileNameToStorageFileName(userFileName);
        }



        }//End Class
}
