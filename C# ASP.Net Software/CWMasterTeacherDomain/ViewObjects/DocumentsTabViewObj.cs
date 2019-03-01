using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using CWMasterTeacherDomain.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CWMasterTeacherDomain.ViewObjects
{
    /// <summary>
    /// Contains the data and logic needed to build a DocumentsTabViewModel.
    /// </summary>
    public class DocumentsTabViewObj
    {   
        private List<DocumentUseDomainObj> _teachingDocumentUses;
        private List<DocumentUseDomainObj> _instructorOnlyDocumentUses;
        private List<DocumentUseDomainObj> _referenceDocumentUses;
        private List<DocumentViewObj> _allDocuments;
        private DocumentUseDomainObj _selectedDocumentUse;
        private DocumentDomainObj _selectedDocument;
        private string _groupDocStorageButtonText;

        /// <summary>
        ///  Constructor that initializes a new instance of DocumentsTabViewObj using the values of the provided arguments.
        /// </summary>
        /// <param name="allDocUses">List of all the document uses. </param>
        /// <param name="selectedDocumentUse">The selected document use.</param>
        /// <param name="selectedDocument">The selected document.</param>
        /// <param name="allDocs">List of all the documents.</param> 
        /// <param name="takeThisMany">How many documents to show.</param> 
        public DocumentsTabViewObj (List<DocumentUseDomainObj> allDocUses, DocumentUseDomainObj selectedDocumentUse,
            DocumentDomainObj selectedDocument, List<DocumentViewObj> allDocs, int takeThisMany = 0)
        {
            _teachingDocumentUses = new List<DocumentUseDomainObj>();
            _instructorOnlyDocumentUses = new List<DocumentUseDomainObj>();
            _referenceDocumentUses = new List<DocumentUseDomainObj>();
            sortDocumentUsesIntoLists(allDocUses);
            _selectedDocumentUse = selectedDocumentUse;
            _selectedDocument = selectedDocument;
            _allDocuments = allDocs == null ? null : removeDuplicates(allDocs, takeThisMany);

            if (allDocs == null)
            {
                GroupDocStorageButtonText = "Show Group Document Storage";
            }
            else
            {
                GroupDocStorageButtonText = "Hide Group Document Storage";
            }
        }

        /// <summary>
        ///  Sorts DocumentUseViewObj's into lists according to their IsInstructorOnly and IsReference properties
        /// </summary>
        /// <param name="allDocUses">List of all the document uses.</param>
        private void sortDocumentUsesIntoLists(List<DocumentUseDomainObj> allDocUses)
        {
            Debug.Assert(allDocUses != null);

            foreach (var documentUseObj in allDocUses)
            {
                if (documentUseObj.IsInstructorOnly)
                {
                    _instructorOnlyDocumentUses.Add(documentUseObj);
                }
                else if (documentUseObj.IsReference)
                {
                    _referenceDocumentUses.Add(documentUseObj);
                }
                else
                {
                    _teachingDocumentUses.Add(documentUseObj);
                }
            }
        }

        /// <summary>
        ///  Removes duplicate documents, then sorts them by their modified dates, and then retrieves a specified 
        ///  number of documents from the sorted list.
        /// </summary>
        /// <param name="unsortedDocuments">List of all of the documents.</param>
        /// <param name="takeThisMany">The number of documents to return.</param> 
        /// <returns>A List of DocumentViewObj</returns>
        private List<DocumentViewObj> removeDuplicates(List<DocumentViewObj> unsortedDocuments, int takeThisMany)
        {
            List<DocumentViewObj> docList = new List<DocumentViewObj>();
            List<Guid> docIdList = new List<Guid>();
            List<DocumentViewObj> waitingList = new List<DocumentViewObj>();

            foreach (var xDocViewObj in unsortedDocuments)
            {
                if (xDocViewObj.Document.IsUsedInLesson)
                {
                    docList.Add(xDocViewObj);
                    docIdList.Add(xDocViewObj.DocumentId);
                }
                else
                {
                    waitingList.Add(xDocViewObj);
                }
            }

            foreach(var xDocObj in waitingList)
            {
                if (!docIdList.Contains(xDocObj.DocumentId))
                {
                    docList.Add(xDocObj);
                    docIdList.Add(xDocObj.DocumentId);
                }
            }

            return docList.OrderByDescending(x => x.Document.DateModified).ToList();          
        }

        /// <summary>
        ///  Read-only property that provides the display name associated with the selected document use
        /// </summary>
        public string SelectedDocumentUseDisplayName
        {
            get
            {
                if (_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.Document.DisplayName;
                }
                else if (_selectedDocument != null)
                {
                    return _selectedDocument.DisplayName;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///  Read-only property that provides the name associated with the selected document use.
        /// </summary>
        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        [Display(Name = "Document Name")]
        public string SelectedDocumentName
        {
            get
            {
                if(_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.Document.BasicObj.Name;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DoShowDownloadPdf
        {
            get { return _selectedDocumentUse != null && (_selectedDocumentUse.Document.IsPdf || _selectedDocumentUse.Document.HasPdfCopy); }
        }

        public bool DoShowUploadPdfCopy
        {
            get { return _selectedDocumentUse == null ? false : !_selectedDocumentUse.Document.IsPdf; }
        }
        /// <summary>
        ///  Read-only property that determines if there is a pdf copy for the selected document use
        /// </summary>
        public bool SelectedDocUseHasPdfCopy
        {
            get
            {
                if (_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.Document.HasPdfCopy;
                }
                else
                {
                    return false;
                }
            } 
        }

        /// <summary>
        ///  Read-only property that provides a download document label for the selected document use
        /// </summary>
        public string DownloadDocumentLabel
        {
            get
            {
                if(_selectedDocumentUse != null)
                {
                    return "Download ." + SelectedDocumentUse.Document.Extension;
                }
                else
                {
                    return null;
                }
            }
            
        }


        /// <summary>
        ///  Read-only property that provides a download document to edit label for the selected document use
        /// </summary>
        public string DownloadDocumentToEditLabel
        {
            get
            {
                if (_selectedDocumentUse != null && !SelectedDocumentUse.Document.IsPdf)
                {
                    return  "Download ." + SelectedDocumentUse.Document.Extension + " to Edit";
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///  Read-only property that provides a download pdf label for the selected document use
        /// </summary>
        public string DownloadPdfLabel
        {
            get
            {
                if (_selectedDocumentUse != null && SelectedDocumentUse.Document.IsPdf)
                {
                    return "View PDF";
                }
                else if (_selectedDocumentUse != null && _selectedDocumentUse.Document.IsWordDoc)
                {
                    return "View PDF copy";
                }
                else
                {
                    return null;
                }
            }

        }

        /// <summary>
        ///  Read-only property that provides a download pdf to save label for the selected document use
        /// </summary>
        public string DownloadPdfToSaveLabel
        {
            get
            {
                if (_selectedDocumentUse != null && (SelectedDocumentUse.Document.IsPdf || SelectedDocumentUse.Document.HasPdfCopy))
                {
                    return "Download PDF to save";
                }
                else if (_selectedDocumentUse != null && _selectedDocumentUse.Document.IsWordDoc)
                {
                    return "Download PDF copy to save";
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///  Read-only property that provides the DocumentUseDomainObj associated with the selected document use
        /// </summary>
        public DocumentUseDomainObj SelectedDocumentUse
        {
            get { return _selectedDocumentUse; }
        }

        /// <summary>
        ///  Read-only property that provides the DocumentDomainObj associated with the selected document 
        /// </summary>
        public DocumentDomainObj SelectedDocument
        {
            get { return _selectedDocument; }
        }

        /// <summary>
        ///  Read-only property that provides a list of all DocumentUseViewObj that are teaching document uses
        /// </summary>
        public List<DocumentUseDomainObj> TeachingDocumentUses
        {
            get { return _teachingDocumentUses; }
        }

        /// <summary>
        ///  Read-only property that provides a list of all DocumentUseDomainObj that are instructor only document uses
        /// </summary>
        public List<DocumentUseDomainObj> InstructorOnlyDocumentUses
        {
            get { return _instructorOnlyDocumentUses; }
        }

        /// <summary>
        ///  Read-only property that provides a list of all DocumentUseDomainObj that are reference document uses
        /// </summary>
        public List<DocumentUseDomainObj> ReferenceDocumentUses
        {
            get { return _referenceDocumentUses; }
        }

        /// <summary>
        ///  Read-only property that provides a list of all DocumentViewObj for this documents tab
        /// </summary>
        public List<DocumentViewObj> AllDocuments
        {
            get { return _allDocuments; }
        }

        /// <summary>
        ///  Property to read/set text for the Group Doc Storage Button
        /// </summary>
        public string GroupDocStorageButtonText
        {
            get
            {
                return _groupDocStorageButtonText;
            }

            set
            {
                _groupDocStorageButtonText = value;
            }
        }

        /// <summary>
        ///  Read-only property that deterimines if the selected document use is a reference
        /// </summary>
        [Display(Name = "Is Reference")]
        public bool SelectedDocumentUseIsReference
        {
            get
            {
                if(_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.IsReference;
                }
                else
                {
                    return false;
                }
            }            
        }

        /// <summary>
        ///  Read-only property that determines if the selected document use is instructor only
        /// </summary>
        [Display(Name = "Is Instructor Only")]
        public bool SelectedDocumentUseIsInstructorOnly
        {
            get
            {
                if (_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.IsInstructorOnly;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///  Read-only property that provides the modification notes for the associated selected document use
        /// </summary>
        [Display(Name = "Modification Notes")]
        public string SelectedDocumentUseModificationNotes
        {
            get
            {
                if (_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.Document.ModificationNotes;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///  Read-only property that determines if the selected document use is a pdf
        /// </summary>
        public bool SelectedDocUseIsPdf
        {
            get
            {
                if (_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.Document.IsPdf;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///  Read-only property that determines if the selected document use is out for edit
        /// </summary>
        public bool SelectedDocUseIsOutForEdit
        {
            get
            {
                if (_selectedDocumentUse != null)
                {
                    return _selectedDocumentUse.IsOutForEdit;
                }
                else
                {
                    return false;
                }
            }
        }


        //These are pass-throughs that only need to go to the view
        public bool ShowGroupDocStorage { get; set; }
        public string FileValidationMessage { get; set; }
        /// <summary>
        ///  Read and write property that provides access to the associated file.
        /// </summary>
        [Required(ErrorMessage = "Select a file to upload.")]
        public HttpPostedFileBase file { get; set; }

        public bool IsRename { get; set; }

        public bool IsPdfCopyUpload { get; set; }

        public Guid SelectedDocumentUseId { get; set; }

        public Guid SelectedDocumentId { get; set; }

        public bool ShowUploadDialog { get; set; }

        public bool ShowArchivedInAllDocs { get; set; }

        public bool HasBasicEditRights { get; set; }
        public bool HasMasterEditRights { get; set; }

        public string ComparisonDocumentsLinkText
        {
            get { return DomainWebUtilities.ComparisonDocumentsLinkText; }
        }

        /// <summary>
        ///   property that provides the number of documents to show if the group documents are visible.
        /// </summary>
        public int ShowThisManyDocs { get; set; }

        public LessonDomainObjBasic LessonObjBasic { get; set; }

        public Guid SelectedLessonId
        {
            get { return LessonObjBasic == null ? Guid.Empty : LessonObjBasic.Id; }
        }

        public bool ShowComparisonLink
        {
            get { return LessonObjBasic == null ? false : 
                        LessonObjBasic.DocumentsHaveNewChangesinGroup 
                        && LessonObjBasic.DoShowDocumentsNotifications
                        && HasBasicEditRights; }
        }







    }//End class
}
