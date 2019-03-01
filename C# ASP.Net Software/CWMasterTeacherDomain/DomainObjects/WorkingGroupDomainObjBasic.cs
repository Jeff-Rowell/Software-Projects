using System;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// WorkingGroupDomainObjectBasic abstracts the dependencies of the db from the rest of the application
    /// by taking the basic information from the WorkingGroup DbObject and storing it in the BasicDomain object
    /// </summary>
    public class WorkingGroupDomainObjBasic
    {

        public Guid WorkingGroupId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Initializes an empty instance of the WorkingGroupDomainObjectBasic
        /// </summary>
        public WorkingGroupDomainObjBasic() { }

        /// <summary>
        /// Initializes an instance of the WorkingGroupDomainObjectBasic and populates
        /// member variables with the provided parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public WorkingGroupDomainObjBasic(Guid id, string name)
        {
            WorkingGroupId = id;
            Name = name;
        }

        public Guid Id
        {
            get
            {
                return WorkingGroupId;
            }
        }
    }
}