using System.Diagnostics;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain.ViewObjects
{
    /// <summary>
    /// Contains the data and logic needed to build a DocumentUseViewModel.
    /// </summary>
    public class DocumentUseViewObj
    {
        private DocumentUseDomainObj _documentUse;
        private int _selectedDocumentUseId;

        /// <summary>
        ///  Creates a DocumentUseViewObj that contains the supplied document use and logic necessary to use the data in the
        ///  document use to produce output for a DocumentUseViewModel.
        /// </summary>
        /// <param name="documentUse">The document use that contains the underlying data.</param>
        public DocumentUseViewObj(DocumentUseDomainObj documentUse, int selectedDocumentUseId)
        {
            Debug.Assert(documentUse != null);

            _documentUse = documentUse;
            _selectedDocumentUseId = selectedDocumentUseId;
        }

        /// <summary>
        ///  Read-only property that provides the display name associated with the document use.
        /// </summary>
        public string DisplayName
        {
            get
            {
                string displayName = "";

                if (_documentUse.IsOutForEdit)
                {
                    displayName = _documentUse.Document.DisplayName + "......(OUT FOR EDIT!!!)";
                }
                else
                {
                    displayName = _documentUse.Document.ExtendedDisplayName;
                }
                return displayName;
            }            
        }

        /// <summary>
        ///  Read-only property that provides access to the associated document use.
        /// </summary>
        public DocumentUseDomainObj DocumentUse
        {
            get { return _documentUse; }
        }

        /// <summary>
        ///  Read-only property that provides the display class for the associated document use.
        /// </summary>
        public string DisplayClass
        {
            get
            {
                //Set css class to reflect IsOutForEdit
                string outForEditClass = DomainWebUtilities.GetDocOutForEditCSS(_documentUse.IsOutForEdit);
                string selectedClass = DomainWebUtilities.ListSelectedClass(_documentUse.DocumentUseBasic.DocumentUseId == _selectedDocumentUseId, false);
                return outForEditClass + " " + selectedClass;
            }
        }

        /// <summary>
        ///  Read-only property that provides the Id for the associated document use.
        /// </summary>
        public int DocumentUseId
        {
            get
            {
                return _documentUse.DocumentUseBasic.DocumentUseId;
            }
        }

        /// <summary>
        ///  Read-only property that provides the modification notes for the document attached to the associated document use.
        /// </summary>
        public string ModificationNotes
        {
            get
            {
                return _documentUse.Document.ModificationNotes;
            }
        }

    }
}

