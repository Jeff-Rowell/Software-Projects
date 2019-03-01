using System;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// UserDomainObjectBasic abstracts the dependencies of the db from the rest of the application
    /// by taking the basic information from the User DbObject and storing it in the BasicDomain object
    /// </summary>
    public class UserDomainObjBasic
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Name
        {
            get { return FullName; }
        }

        /// <summary>
        /// Creates an empty UserDomainObjectBasic instance.
        /// </summary>
        public UserDomainObjBasic() { }

        /// <summary>
        /// Generates a UserDomainObjectBasic instance initialized from the provided parameters.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public UserDomainObjBasic(Guid userId, string userName, string firstName, string lastName)
        {
            Id = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }


    }
}