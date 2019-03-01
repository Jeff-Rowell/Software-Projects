using System;
using System.Collections.Generic;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// UserDomainObject abstracts the dependencies of the db from the rest of the application
    /// by taking relevant information from the User DbObject and storing it in the Domain object.
    /// Also contains lists of User and WorkingGroup basic domain objs to generate lists of Names
    /// and Ids in the view object.
    /// </summary>
    public class UserDomainObj
    {
        public UserDomainObjBasic BasicObj { get; set; }

        public Guid WorkingGroupId { get; set; }
        public string WorkingGroupName { get; set; }
        public string EmailAddress { get; set; }        
        public bool IsApplicationAdmin { get; set; }
        public bool IsWorkingGroupAdmin { get; set; }
        public bool IsMasterEditor { get; set; }
        public bool IsActive { get; set; }
        public bool HasAdminApproval { get; set; }
        public DateTime LastLogin { get; set; }
        public Guid LastDisplayedCourseId { get; set; }
        public bool ShowMastersInCourseList { get; set; }
        public bool ShowAllInCourseList { get; set; }


        public string UserName
        {
            get { return BasicObj.UserName; }
            set { BasicObj.UserName = value; }
        }

        public string DisplayName
        {
            get { return BasicObj.DisplayName; }
        }
        public Guid Id
        {
            get { return BasicObj.Id; }
            set { BasicObj.Id = value; }
        }
        public string FirstName
        {
            get { return BasicObj.FirstName; }
            set { BasicObj.FirstName = value; }
        }
        public string LastName
        {
            get { return BasicObj.LastName; }
            set { BasicObj.LastName = value; }
        }
        public string Name
        {
            get { return BasicObj.FullName; }
        }
        public string FullName
        {
            get { return BasicObj.FullName; }
        }

        /// <summary>
        /// Generates a UserDomain Object with an empty UserDomainObjectBasic
        /// </summary>
        public UserDomainObj()
        {
            BasicObj = new UserDomainObjBasic();

        }

        /// <summary>
        /// Generates a UserDomain Object and populates its UserDomainObjectBasic
        /// field with a provided userId, userName, and displayName
        /// </summary>
        public UserDomainObj(Guid userId, string userName, string firstName, string lastName)
        {
            BasicObj = new UserDomainObjBasic(userId, userName, firstName, lastName);
        }
    }
}


