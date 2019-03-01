using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{   /// <summary>
    ///  Holds the rest of data and properties from DocumentDomainObjBasic.
    /// </summary>
    public class DocumentDomainObj
    {
        private DocumentDomainObjBasic _documentBasic;
        private DateTime? _dateModified;
        private bool _isUsedInLesson;
        private bool _isReferenceInLesson;
        private bool _isPdf;
        private bool _hasPdfCopy;
        private string _extension;


        //TODO:  Get rid of this constructor and get everything going through the no-argument constructor
        /// <summary>
        ///  Basic constructor that initializes the new instance of DocumentDomainObj with the values of the provided arguments.
        /// </summary>
        /// <param name="documentBasic">The basic data from DocumentDomainOBjBasic class that is associated of the document.</param>
        /// <param name="dateModified">The modified date that is associated of the document.</param>
        /// <param name="isUsedInLesson">The document that is used in lesson of the associated document.</param>
        /// <param name="isReferenceInLesson">The document that gets reference in lesson of the associated document.</param> 
        /// <param name="isPdf">The document that is pdf of the associated document.</param> 
        /// <param name="hasPdfCopy">The document has pdf copy of the associated document.</param>
        /// <param name="extension">The document that is the extension of the associated document.</param>
        public DocumentDomainObj(DocumentDomainObjBasic documentBasic, 
                                        DateTime? dateModified, 
                                        bool isUsedInLesson, 
                                        bool isReferenceInLesson,
                                        bool isPdf, 
                                        bool hasPdfCopy, 
                                        string extension)
        {
            _documentBasic = documentBasic;
            _dateModified = dateModified;
            _isUsedInLesson = isUsedInLesson;
            _isReferenceInLesson = isReferenceInLesson;
            _isPdf = isPdf;
            _hasPdfCopy = hasPdfCopy;
            _extension = extension;
        }


        //TODO:  Move everything to this constructor
        /// <summary>
        /// This should be the basic constructor.  Should usually only be called through Build except perhaps in tests.
        /// </summary>
        public DocumentDomainObj() { }

        /// <summary>
        ///  Read-only property representing the basic data from DocumentDomainOBjBasic class that associated with the instance of DocumentDomainObj.
        /// </summary>
        public DocumentDomainObjBasic BasicObj
        {
            get { return _documentBasic; }
            set { _documentBasic = value; }
        }

        /// <summary>
        /// Used for assembling Display Names
        /// </summary>
        public string Name
        {
            get { return BasicObj.Name; }
        }

        public string FileName { get; set; }

        public string FileNameOfPdfCopy { get; set; }


        /// <summary>
        /// Not set in the Build.  These are set in the Repo and used in displaying Documents
        /// </summary>
        public bool IsUsedInLesson { get; set; }

        /// <summary>
        /// Not set in the Build.  These are set in the Repo and used in displaying Documents
        /// </summary>
        public bool IsReferenceInLesson { get; set; }

        public string ModificationNotes { get; set; }

        public DateTime DateModified { get; set; }

        public UserDomainObjBasic UserWhoModified { get; set; }

        public Guid OriginalDocumentId { get; set; }

        public string DisplayName
        {
            get
            {
                string name = Name;
                name = name.Substring(0, Math.Min(name.Length, 45));

                name = name + " (" + DisplayExtension + ")";

                int difference = 47 - name.Length;

                for (int i = 0; i < difference; i++)
                {
                    name = name + ".";
                }

                string info = UserWhoModified.DisplayName + "  (" + DateModified.ToString() + ")";

                return name + info;

            }
        }

        public string Extension
        {
            get
            {
                return FileName.Substring(FileName.LastIndexOf(".") + 1);
            }
        }

        private string DisplayExtension
        {
            get
            {
                if (HasPdfCopy)
                {
                    return Extension + "/pdf";
                }
                else
                {
                    return Extension;
                }
            }
        }

        public bool IsPdf
        {
            get
            {
                return Extension == "pdf";
            }
        }


        public bool HasPdfCopy
        {
            get
            {
                if (String.IsNullOrEmpty(FileNameOfPdfCopy))
                {
                    return false;
                }
                else
                {
                    return FileNameOfPdfCopy.Length > 2;
                }
            }
        }

        public bool IsWordDoc
        {
            get { return Extension == "doc" || Extension == "docx"; }
        }

        public string ExtendedDisplayName
        {
            get
            {
                string notes;
                if (ModificationNotes != null)
                {
                    notes = ModificationNotes;
                }
                else
                {
                    notes = "";
                }
                notes = notes.Substring(0, Math.Min(notes.Length, 50));
                return DisplayName + "......(" + notes + ")";
            }
        }

        public Guid Id
        {
            get { return BasicObj.Id; }
        }



    }//End Class
}
