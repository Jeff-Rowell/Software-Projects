using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    ///  Holds the basic data required to identify a DocumentUseDomainObject. While designed to be used as a 
    ///  property in an instance of DocumentUseDomainObject, it can also be instantiated as a stand alone object
    ///  in order to provide a lightweight representation of a document that gets used for display purposes.
    /// </summary>
    public class DocumentUseDomainObjBasic
    {
        private Guid _documentUseId;

        /// <summary>
        ///  Basic constructor that initializes the new instance of DocumenUsetDomainObjBasic with the values of the provided arguments.
        /// </summary>
        /// <param name="documentUseId">The document use id that identifies the document to which the newly constructed instance refers.</param>
        /// </param>
        public DocumentUseDomainObjBasic(Guid documentUseId)
        {
            _documentUseId = documentUseId;
        }

        /// <summary>
        ///  Read-only property representing the Id of the document that gets used associating with the instance of DocumentUseDomainObjBasic.
        /// </summary>
        public Guid DocumentUseId
        {
            get { return _documentUseId; }
        } 
    }
}
