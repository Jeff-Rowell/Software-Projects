using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// Provides retrieval services for use in acquiring instances of DocumentsTabViewObj that contain
    /// the desired data.
    /// </summary>
    public class DocumentsTabViewObjBuilder
    {
        private IDocumentUseDomainObjBuilder _documentUseBuilder;
        private IDomainObjBuilder<DocumentDomainObj, DocumentDomainObjBasic, Document> _documentBuilder;
        private DocumentViewObjBuilder _documentService;
        private LessonDomainObjBuilder _lessonObjBuilder;

        /// <summary>
        ///  Creates a DocumentsTabService that uses the supplied builders when acquiring data.
        /// </summary>
        /// <param name="documentUseDomainObjBuilder">An IDocumentUseDomainObjBuilder that is used when acquiring document use data.</param>
        /// <param name="documentDomainObjBuilder">An IDomainObjBuilder that is used when acquiring document data.</param>
        public DocumentsTabViewObjBuilder(IDocumentUseDomainObjBuilder documentUseDomainObjBuilder,
            IDomainObjBuilder<DocumentDomainObj, DocumentDomainObjBasic, Document> documentDomainObjBuilder,
            LessonDomainObjBuilder lessonObjBuilder)
        {
            Debug.Assert(documentUseDomainObjBuilder != null);
            Debug.Assert(documentDomainObjBuilder != null);

            _documentUseBuilder = documentUseDomainObjBuilder;
            _documentBuilder = documentDomainObjBuilder;            
            _documentService = new DocumentViewObjBuilder();
            _lessonObjBuilder = lessonObjBuilder;
        }

        /// <summary>
        ///  Retrieves a DocumentsTabViewObj built using the given arguments.
        /// </summary>
        /// <param name="lessonId">The Id of the selected lesson.</param>
        /// <param name="selectedDocumentUseId">The Id of the selected document use. Values less than 1 are evaluated as no selected document use.</param> 
        /// <param name="selectedDocumentId">The Id of the selected document. Values less than 1 are evaluated as no selected document.</param>
        /// <param name="includeArchived">Whether or not to include documents that are not active.</param>
        /// <param name="showThisManyDocs">How many documents to show if the Group Documents are displayed</param>
        /// <param name="showGroupDocStorage">Whether to retrieve the Group Documents for display.</param>
        /// <returns>A DocumentsTabViewObj</returns>
        public DocumentsTabViewObj Retrieve(Guid lessonId, Guid selectedDocumentUseId, Guid selectedDocumentId, bool includeArchived,
                                    int showThisManyDocs, bool showGroupDocStorage, bool isPdfCopyUpload, 
                                    bool showUploadDialog, bool isRename, string fileValidationMessage, bool hasComparisonDocsAvailable,
                                    bool hasBasicEditRights, bool hasMasterEditRights)
        {
            DocumentsTabViewObj viewObj;
            DocumentUseDomainObj selectedDocumentUse = null;
            DocumentDomainObj selectedDocument = null;
            List<DocumentUseDomainObj> allDocUseObjs;

            if (lessonId != Guid.Empty)
            {

                allDocUseObjs = _documentUseBuilder.ActiveDocumentUsesForLesson(lessonId);
                 
                foreach(var xDocUse in allDocUseObjs)
                {
                    xDocUse.SelectedDocumentUseId = selectedDocumentUseId;
                }
            }
            else
            {
                allDocUseObjs = new List<DocumentUseDomainObj>();
            }

            if (selectedDocumentUseId != Guid.Empty)
            {
                selectedDocumentUse = _documentUseBuilder.BuildFromId(selectedDocumentUseId);
            }
            else if (selectedDocumentId != Guid.Empty)
            {
                selectedDocument = _documentBuilder.BuildFromId(selectedDocumentId);
            }

            viewObj = new DocumentsTabViewObj(allDocUses: allDocUseObjs, 
                                               selectedDocumentUse: selectedDocumentUse, 
                                               selectedDocument: selectedDocument,
                                               allDocs: getShownDocuments(showGroupDocStorage, lessonId, includeArchived, selectedDocumentId), 
                                               takeThisMany: showThisManyDocs);

            viewObj.LessonObjBasic = _lessonObjBuilder.BuildBasicFromId(lessonId);
            viewObj.ShowArchivedInAllDocs = includeArchived;
            viewObj.ShowUploadDialog = showUploadDialog;
            viewObj.SelectedDocumentId = selectedDocumentId;
            viewObj.SelectedDocumentUseId = selectedDocumentUseId;
            viewObj.IsRename = isRename;
            viewObj.FileValidationMessage = fileValidationMessage;
            viewObj.ShowGroupDocStorage = showGroupDocStorage;
            viewObj.ShowThisManyDocs = showThisManyDocs;
            viewObj.HasBasicEditRights = hasBasicEditRights;
            viewObj.HasMasterEditRights = hasMasterEditRights;
            viewObj.IsPdfCopyUpload = isPdfCopyUpload;

            return viewObj;
        }

        private List<DocumentViewObj> getShownDocuments(bool showGroupDocStorage, Guid lessonId, bool includeArchived, Guid selectedDocumentId)
        {
            if (showGroupDocStorage)
            {
                List<DocumentUseDomainObj> allDocs = includeArchived ?
                    _documentUseBuilder.AllDocumentUsesThatShareMasterLesson(lessonId) :
                    _documentUseBuilder.ActiveDocumentUsesThatShareMasterLesson(lessonId);

                List<DocumentViewObj> allDocViewObjs = allDocs.Select(x => _documentService.Retrieve(x, lessonId)).ToList();
                foreach(var xDoc in allDocViewObjs)
                {
                    xDoc.SelectedDocumentId = selectedDocumentId;
                }

                return allDocViewObjs;
            }
            else
            {
                return null;
            }
        }        

    }

}
