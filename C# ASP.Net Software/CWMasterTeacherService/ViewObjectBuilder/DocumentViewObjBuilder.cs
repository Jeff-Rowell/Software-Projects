using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System.Diagnostics;
using System;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// Provides retrieval services for use in acquiring instances of DocumentViewObj that contain
    /// the desired data.
    /// </summary>
    public class DocumentViewObjBuilder
    {
        /// <summary>
        ///  Retrieves a DocumentViewObj built using the DocumentUseDomainObj argument and initializes
        ///  IsReferenceInLesson and IsUsedInLesson in the Document property to reflect the document's status
        ///  in the designated lesson.
        /// </summary>
        /// <param name="documentUse">A document use that is used to create the returned DocumentViewObj.</param>
        /// <param name="possibleParentLessonId">A lesson Id that may use the retrieved document. This is typically the currently selected lesson.</param>
        /// <returns>A DocumentViewObj</returns>
        public DocumentViewObj Retrieve(DocumentUseDomainObj documentUse, Guid possibleParentLessonId)
        {
            Debug.Assert(documentUse != null);
            Debug.Assert(possibleParentLessonId != Guid.Empty);

            return new DocumentViewObj(documentUse, possibleParentLessonId);
        }
    }
}
