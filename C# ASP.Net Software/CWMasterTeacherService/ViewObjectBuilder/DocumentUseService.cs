using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System.Diagnostics;

namespace CWMasterTeacherService.RetrieveServices
{
    /// <summary>
    /// Provides retrieval services for use in acquiring instances of DocumentUseViewObj that contain
    /// the desired data.
    /// </summary>
    public class DocumentUseService
    {
        /// <summary>
        /// Retrieves a DocumentUseViewObj built using the DocumentUseDomainObj argument
        /// </summary>
        /// <param name="documentUse">A document use that is used to create the returned DocumentUseViewObj</param>
        /// <returns>A DocumentUseViewObj</returns>
        public DocumentUseViewObj Retrieve(DocumentUseDomainObj documentUse, int selectedDocumentUseId)
        {
            Debug.Assert(documentUse != null);

            return new DocumentUseViewObj(documentUse, selectedDocumentUseId);
        }        
    }
}
