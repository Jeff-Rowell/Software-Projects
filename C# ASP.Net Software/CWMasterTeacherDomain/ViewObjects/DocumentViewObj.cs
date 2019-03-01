using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain.ViewObjects
{
    /// <summary>
    /// Instances of this class will be pass from Service to the Controller and used by the 
    /// controller to send request to the ViewModelFatory
    /// </summary>
    public class DocumentViewObj
    {
        /// <summary>
        /// Constructor used to create instance of DocumentViewObj using object of type DocumentDoaminObj
        /// </summary>
        /// <param name="document">Instance of DocumentDomainObj</param>
        public DocumentViewObj(DocumentDomainObj document)
        {
            Debug.Assert(document != null);
            if(document != null)
            {
                Document = document;
            }
        }
        /// <summary>
        /// This constructor used to create instance of DocumentViewObj using object of type DocumentUseDoaminObj 
        /// and possibleParentLessonID. If the document used is active and possibleParentLessonID is the same as 
        /// ParentLessonID then IsReferenceInLesson is set to be the same as IsReference of the DocumentUse passed in.
        /// Also the IsUsedInLesson is set to true.
        /// </summary>
        /// <param name="documentUse">Object of data type DocumentUseDoaminObj</param>
        /// <param name="possibleParentLessonId">int data type used to compare with parentLessonid</param>
        public DocumentViewObj(DocumentUseDomainObj documentUse, Guid possibleParentLessonId)
        {
            Debug.Assert(possibleParentLessonId != Guid.Empty);
            Debug.Assert(documentUse.Document != null);

            Document = documentUse.Document;
            bool isUsedByLesson = documentUse.ParentLessonId == possibleParentLessonId && documentUse.IsActive;
            if (isUsedByLesson)
            {
                Document.IsReferenceInLesson = documentUse.IsReference;
                Document.IsUsedInLesson = true;
            }
        }

        /// <summary>
        ///  Read-only property that provides the document associated with the document view.
        /// </summary>
        public DocumentDomainObj Document { get; set; }

        /// <summary>
        ///  Read-only property that returns true if the associated document as a Pdf copy, false otherwise.
        /// </summary>
        public bool HasPdfCopy
        {
            get { return Document.HasPdfCopy; }
        }

        /// <summary>
        ///  Read-only property that returns true if the associated document is a Pdf, false otherwise.
        /// </summary>
        public bool IsPdf
        {
            get { return Document.IsPdf; }
        }

        /// <summary>
        ///  Read-only property that provides the Id for the document associated with the document view.
        /// </summary>
        public Guid DocumentId
        {
            get
            {
                return Document.BasicObj.Id;
            }
        }

        /// <summary>
        ///  Read-only property that provides the display name for the document associated with the document view.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return Document.DisplayName;
            }
        }

        /// <summary>
        /// The Id of the selected document in the display.  Set in Service and used to set DisplayClass CSS.
        /// </summary>
        public Guid SelectedDocumentId { get; set; }

        /// <summary>
        ///  Read-only property that provides the display class for the associated document.
        /// </summary>
        public string DisplayClass
        {
            get
            {
                return DomainWebUtilities.ListSelectedClass(DocumentId == SelectedDocumentId, false);
            }
        }

        /// <summary>
        /// If the document is used in lesson and is referenced in the lesson, then the display name is set with the prefix "*   "
        /// If the document is used in lesson and is not referenced in lesson, then the display name is set with the prefix "*** "
        /// If the document is not in used then the prefix is set with four white space.
        /// </summary>
        public string DisplayNameExtended
        {
            get
            {
                string listPrefix = "";
                if (Document.IsUsedInLesson)// Which means it's being actively used
                {
                    if (Document.IsReferenceInLesson)//Which means it's a referenceDoc
                    {
                        listPrefix = "*   ";
                    }
                    else//Which means it's a teachingDoc
                    {
                        listPrefix = "*** ";
                    }
                }
                else
                {
                    listPrefix = "    ";
                }
                return listPrefix + Document.DisplayName;
            }
        }




    }        
}
