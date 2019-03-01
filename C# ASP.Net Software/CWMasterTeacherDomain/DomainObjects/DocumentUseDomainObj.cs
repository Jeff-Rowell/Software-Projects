using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{   /// <summary>
    ///  Holds the rest of data and properties from DocumentUseDomainObjBasic.
    /// </summary>
    public class DocumentUseDomainObj
    {

        /// <summary>
        ///  Basic constructor that initializes the new instance of DocumentUseDomainObj with the values of the provided arguments.
        /// </summary>
        /// <param name="documentUseBasic">The basic data from DocumentUseDomainOBjBasic class that is associated of the document.</param>
        /// <param name="document">The document that is associated of the document.</param>
        /// <param name="parentLessonId">The parent lesson ID of the associated document.</param>
        /// <param name="isActive">The document that is active of the associated document.</param> 
        /// <param name="isReference">The document that gets referenced of the associated document.</param> 
        /// <param name="isInstructorOnly">The document that is instructor use only of the associated document.</param>
        /// <param name="isVisibleToStudents">The document that is visible for student use of the associated document.</param>
        /// <param name="isOutForEdit">The document that is out for edit of the associated document.</param> 
        public DocumentUseDomainObj(DocumentUseDomainObjBasic documentUseBasic, DocumentDomainObj document, Guid parentLessonId, bool isActive,
                                    bool isReference, bool isInstructorOnly, bool isVisibleToStudents, bool isOutForEdit, string documentName,
                                    DateTime dateChangesAccepted)
        {
            DocumentUseBasic = documentUseBasic;
            Document = document;
            ParentLessonId = parentLessonId;
            IsActive = isActive;
            IsReference = isReference;
            IsInstructorOnly = isInstructorOnly;
            IsVisibleToStudents = isVisibleToStudents;
            IsOutForEdit = isOutForEdit;
            SelectedDocumentUseId = Guid.Empty;
            IsUsedInEditableLesson = true;
            HasBeenUpdated = false;
            DateChangesAccepted = dateChangesAccepted;
        }

        /// <summary>
        ///  Read-only property representing the basic data from DocumentUseDomainOBjBasic class that associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public DocumentUseDomainObjBasic DocumentUseBasic { get; }

        /// <summary>
        ///  Read-only property representing the document associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public DocumentDomainObj Document { get; }

        /// <summary>
        ///  Read-only property representing the parent lesson ID of the document associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public Guid ParentLessonId { get; }

        /// <summary>
        ///  Read-only property representing the document that is active which associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        ///  Read-only property representing the document that gets referenced which associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public bool IsReference { get; }

        /// <summary>
        ///  Read-only property representing the document that is only for instructor use which associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public bool IsInstructorOnly { get; }


        /// <summary>
        ///  Read-only property representing the document that is visible for students use which associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public bool IsVisibleToStudents { get; }


        /// <summary>
        ///  Read-only property representing the document that is out for edit which associated with the instance of DocumentUseDomainObj.
        /// </summary>
        public bool IsOutForEdit { get; }

        /// <summary>
        /// Not set by constructor, set in Service and used for setting correct CSS.
        /// </summary>
        public Guid SelectedDocumentUseId { get; set; }


        /// <summary>
        /// The name of the document
        /// </summary>
        public string DocumentName
        {
            get { return Document == null ? "" : Document.Name; }
        }

        public string DisplayName
        {
            get
            {
                if (Document != null)
                {
                    string displayName = Document.DisplayName;
                    if (!IsUsedInEditableLesson)
                    {
                        displayName = displayName + " ***Not In Lesson***";
                    }
                    else if (HasBeenUpdated)
                    {
                        displayName = displayName + " ***Updated***";
                    }
                    return displayName;
                }
                else
                {
                    return null;
                }
            }
        }

        public Guid DocumentUseId
        {
            get { return DocumentUseBasic.DocumentUseId; }
        }

        public string ModificationNotes
        {
            get { return Document == null ? "" : Document.ModificationNotes; }
        }

        /// <summary>
        ///  Read-only property representing the extended display name of the document associated with the instance of DocumentUse.
        ///  The extended display name is built using the display name and the first 50 characters of the modification notes.
        /// </summary>
        public string ExtendedDisplayName
        {
            get
            {
                return Document.ExtendedDisplayName;
            }
        }

        /// <summary>
        ///  Read-only property that provides the display name along with "Out To Edit" flag if appropriate.
        /// </summary>
        public string ExtendedDisplayNameWithEditFlag
        {
            get
            {
                string displayName = "";

                if (IsOutForEdit)
                {
                    displayName = ExtendedDisplayName + "......(OUT FOR EDIT!!!)";
                }
                else
                {
                    displayName = ExtendedDisplayName;
                }
                return displayName;
            }
        }

        /// <summary>
        ///  Read-only property that provides the display class for the associated document use.
        /// </summary>
        public string DisplayClass
        {
            get
            {
                //Set css class to reflect IsOutForEdit
                string outForEditClass = DomainWebUtilities.GetDocOutForEditCSS(IsOutForEdit);
                string selectedClass = DomainWebUtilities.ListSelectedClass(DocumentUseBasic.DocumentUseId == SelectedDocumentUseId, false);
                return outForEditClass + " " + selectedClass;
            }
        }

        public Guid DocumentId
        {
            get { return Document == null ? Guid.Empty : Document.Id; }
        }

        /// <summary>
        /// This is used in Comparison Lessons to set the DisplayName
        /// </summary>
        public bool HasBeenUpdated { get; set; }

        /// <summary>
        /// This is used in Comparison Lessons to set the DisplayName
        /// </summary>
        public bool IsUsedInEditableLesson { get; set; }

        public DateTime DateChangesAccepted { get; set; }

        public DateTime DateModified
        {
            get { return Document == null ? new DateTime() : Document.DateModified; }

        }

    }
}
