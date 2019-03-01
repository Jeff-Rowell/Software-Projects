using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    ///  Holds the basic data required to identify a DocumentDomainObject. While designed to be used as a 
    ///  property in an instance of DocumentDomainObject, it can also be instantiated as a stand alone object
    ///  in order to provide a lightweight representation of a document for display purposes.
    /// </summary>
    public class DocumentDomainObjBasic
    {
        private Guid _documentId;
        private string _name;


        //TODO: Get rid of this constructor.  This will require dealing with a bunch of tests.
        /// <summary>
        ///  Basic constructor that initializes the new instance of DocumentDomainObjBasic with the values of the provided arguments.
        /// </summary>
        /// <param name="documentId">The document id that identifies the document to which the newly constructed instance refers.</param>
        /// <param name="name">The name of the associated document.</param> 
        /// <param name="displayName">The display name of the associated document.</param>
        /// <param name="modificationNotes">The modification notes belonging to the associated document. These notes are used in creating the
        /// extended display name.</param>
        public DocumentDomainObjBasic(Guid documentId, string name, string displayName, string modificationNotes)
        {
            _documentId = documentId;
            _name = name;
        }

        /// <summary>
        /// This no parameter constructor is the one you want to keep.
        /// </summary>
        public DocumentDomainObjBasic() { }

        public Guid Id { get; set; }


        /// <summary>
        ///  Read-only property that provides the name for the associated document.
        /// </summary>
        public string Name { get; set; }



    }//End Class
}
