using System;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// WorkingGroupDomainObj abstracts the dependencies of the db from the rest of the application
    /// by taking relevant information from the WorkingGroup DbObject and storing it in the Domain object
    /// </summary>
    public class WorkingGroupDomainObj
    {
        /// <summary>
        /// Generate an empty instance of the WorkingGroupDomainObject
        /// </summary>
        public WorkingGroupDomainObj()
        {
            WorkingGroupBasicObj = new WorkingGroupDomainObjBasic();
        }

        /// <summary>
        /// Generates an instance of the WorkingGroupDomainObject and initializes
        /// the NameAndId param from a provided WorkingGroupDomainObjectBasicObject
        /// </summary>
        /// <param name="basicObject"></param>
        public WorkingGroupDomainObj(WorkingGroupDomainObjBasic basicObject)
        {
            WorkingGroupBasicObj = basicObject;
        }

        public WorkingGroupDomainObjBasic WorkingGroupBasicObj { get; set; }

        public Guid Id
        {
            get { return WorkingGroupBasicObj == null ? Guid.Empty : WorkingGroupBasicObj.Id; }
        }

        public string Name
        {
            get { return WorkingGroupBasicObj == null ? "" : WorkingGroupBasicObj.Name; }
        }

        public string StudentAccessCode { get; set; }

        public string InstructorAccessCode { get; set; }
    }
}
