using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// CourseDomainObjBasic abstracts the dependencies of the db from the rest of the application
    /// by taking the basic information from the Cousre DbObject and storing it in the BasicDomain object
    /// </summary>
    public class CourseDomainObjBasic: IListable
    {
        private string _termName;
        private string _userDisplayName;

        public CourseDomainObjBasic()
        {

        }

        public CourseDomainObjBasic(Guid courseId, string name, bool isMaster, string termName, string userDisplayName,
                                    Guid termId, bool hasActiveMessages, bool hasImportantMessages, bool hasActiveStoredMessages,
                                    bool hasNewMessages, bool hasOutForEditDocuments, bool hasStoredMessages,
                                    DateTime termEndDate, Guid userId)
        {
            Id = courseId;
            Name = name;
            IsMaster = isMaster;
            _termName = termName;
            _userDisplayName = userDisplayName;
            TermId = termId;
            HasActiveMessages = hasActiveMessages;
            HasImportantMessages = hasImportantMessages;
            HasActiveStoredMessages = hasActiveStoredMessages;
            HasNewMessages = hasNewMessages;
            HasOutForEditDocuments = hasOutForEditDocuments;
            HasStoredMessages = hasStoredMessages;
            TermEndDate = termEndDate;
            UserId = userId;
        }

        public Guid Id { get; set; }


        public Guid CourseId
        {
            get { return Id; }
        }

        public Guid TermId { get; set; }

        public string Name { get; set; }
        public Guid UserId { get; set; }
        public bool IsMaster { get; set; }
        public bool HasImportantMessages { get; set; }
        public bool HasNewMessages { get; set; }
        public bool HasActiveMessages { get; set; }
        public bool HasActiveStoredMessages { get; set; }
        public bool HasStoredMessages { get; set; }
        public bool HasOutForEditDocuments { get; set; }
        public DateTime TermEndDate { get; set; } //This is used for sorting

        //Not set from Db, set in Curriculum Service in order to set display values
        public Guid SelectedCourseId { get; set; }

        public string DisplayName
        {
            get
            {
                return Name + "_" + NameExtension;
            }
        }

        public string NameExtension
        {
            get
            {
                string nameExtension = "";
                if (IsMaster)
                {
                    nameExtension = _termName + " Master";
                }
                else
                {
                    nameExtension = _termName + "_" + _userDisplayName;
                }
                return nameExtension;
            }
        }

        public string DisplayClass
        {
            get
            {
                string messageClass = DomainWebUtilities.GetMessageAndDocImageCSS(HasImportantMessages, HasNewMessages,
                                HasActiveMessages, HasActiveStoredMessages, HasStoredMessages, HasOutForEditDocuments, false);
                string selectedClass = DomainWebUtilities.ListSelectedClass(SelectedCourseId == Id, true);
                return messageClass + " " + selectedClass;
            }
        }

    }
}