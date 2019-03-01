using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// Provides services that build and return DocumentDomainObjs.
    /// </summary>
    public class DocumentDomainObjBuilder : Interfaces.IDocumentDomainObjBuilder
    {
        private DocumentRepo _documentRepo;

        /// <summary>
        /// Provides services that build and return DocumentUseDomainObjs using the provided repository.
        /// </summary>
        /// <param name="documentRepo">A document repository that is used to when retrieving the 
        /// data used to build a DocumentDomainObj</param>
        public DocumentDomainObjBuilder(DocumentRepo documentRepo)
        {
            Debug.Assert(documentRepo != null);

            _documentRepo = documentRepo;
        }

        public DocumentDomainObjBuilder()
        {        }

        /// <summary>
        /// Builds a DocumentDomainObj from the provided Document.
        /// </summary>
        /// <param name="dbObject">A Document object containing the data used to build the DocumentDomainObj</param>
        /// <returns>A new DocumentDomainObj initialized with the data from the argument.</returns>
        public static DocumentDomainObj Build(Document dbObject)
        {
            Debug.Assert(dbObject != null);

            DocumentDomainObj domainObj = new DocumentDomainObj();

            domainObj.BasicObj = BuildBasic(dbObject);
            domainObj.DateModified = dbObject.DateModified == null ? new DateTime (1001, 01, 01) : dbObject.DateModified.Value;
            domainObj.FileName = dbObject.FileName;
            domainObj.FileNameOfPdfCopy = dbObject.FileNameOfPdfCopy;
            domainObj.ModificationNotes = dbObject.ModificationNotes;
            domainObj.UserWhoModified = UserDomainObjBuilder.BuildBasic(dbObject.User);
            domainObj.OriginalDocumentId = dbObject.OriginalDocumentId == null ? Guid.Empty : dbObject.OriginalDocumentId.Value;

            return domainObj;
            
        }

        /// <summary>
        /// Builds a DocumentDomainObj from the Document retrieved using the id parameter.
        /// </summary>
        /// <param name="id">The id of the Document to be used to build the DocumentDomainObj</param>
        /// <returns>A new DocumentDomainObj initialized with the data retrieved using the argument.</returns>
        public DocumentDomainObj BuildFromId(Guid id)
        {
            Debug.Assert(id != Guid.Empty);

            return Build(_documentRepo.GetById(id));
        }

        /// <summary>
        /// Builds a DocumentDomainObjBasic from the provided Document.
        /// </summary>
        /// <param name="dbObject">A Document object containing the data used to build the DocumentDomainObjBasic.</param>
        /// <returns>A new DocumentDomainObjBasic initialized with the data from the argument.</returns>
        public static DocumentDomainObjBasic BuildBasic(Document dbObject)
        {
            Debug.Assert(dbObject != null);

            DocumentDomainObjBasic basicObj = new DocumentDomainObjBasic();
            basicObj.Name = dbObject.Name;
            basicObj.Id = dbObject.Id;

            return basicObj;
           
        }

        /// <summary>
        /// Builds a DocumentDomainObjBasic from the Document retrieved using the id parameter.
        /// </summary>
        /// <param name="id">The id of the Document to be used to build the DocumentDomainObjBasic</param>
        /// <returns>A new DocumentDomainObjBasic initialized with the data retrieved using the argument.</returns>
        public DocumentDomainObjBasic BuildBasicFromId(Guid id)
        {
            Debug.Assert(id != Guid.Empty);

            return BuildBasic(_documentRepo.GetById(id));
        }

        public DocumentDomainObj DocumentForDocumentUse(Guid documentUseId)
        {
            return Build(_documentRepo.DocumentForDocumentUse(documentUseId));
        }








    }//End Class
}
