using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// Represents a Term Object that lives in the Domain. A Term is Term for School. ex. Fall 2016, Spring 2017
    /// </summary>
    public class TermDomainObj
    {
        private TermDomainObjBasic _basicObj;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="basicObj">A Basic Term Object which holds basic information about the Term.</param>
        public TermDomainObj(TermDomainObjBasic basicObj)
        {
            _basicObj = basicObj;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The UID of the Term</param>
        /// <param name="name">The name of the Term</param>
        /// <param name="startDate">Start Date for the Term</param>
        /// <param name="endDate">End Date for the Term</param>
        /// <param name="workingGroupId">The Current WorkingGroup Id</param>
        public TermDomainObj(Guid id, String name, DateTime startDate, DateTime endDate, Guid workingGroupId)
        {
            _basicObj = new TermDomainObjBasic(id, name);
            StartDate = startDate;
            EndDate = endDate;
            WorkingGroupId = workingGroupId;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TermDomainObj()
        {
        }

        /// <summary>
        /// Fully Loaded Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="termId"></param>
        /// <param name="workingGroupId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isCurrent"></param>
        /// <param name="courses"></param>
        /// <param name="holidays"></param>
        /// <param name="workingGroup"></param>
        public TermDomainObj(Guid id, String name, Guid termId, Guid workingGroupId, DateTime startDate, DateTime endDate, bool isCurrent, 
                                    List<CourseDomainObjBasic> courses, List<HolidayDomainObj> holidays, WorkingGroupDomainObjBasic workingGroup)
        {
            Id = id;
            Name = name;
            TermId = termId;
            WorkingGroupId = workingGroupId;
            StartDate = startDate;
            EndDate = endDate;
            IsCurrent = isCurrent;
            Courses = courses;
            Holidays = holidays;
            WorkingGroup = workingGroup;
        }

        /// <summary>
        /// Identifier that is unique to the term domain object
        /// </summary>
        public Guid Id
        {
            set
            {
                _basicObj.Id = value;
            }
            get
            {
                return _basicObj.Id;
            }
        }

        /// <summary>
        /// Name of the term
        /// </summary>
        public string Name {
            set
            {
                _basicObj.Name = value;
            }
            get
            {
                return _basicObj.Name;
            }
        }

        /// <summary>
        /// Term identifer
        /// </summary>
        public Guid TermId { get; set; }

        /// <summary>
        /// WorkingGroup identifier
        /// </summary>
        public Guid WorkingGroupId { get; set; }

        /// <summary>
        /// Specific date associated with the starting date of the term
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Specific date associated with the ending date of the term
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Boolean flag to set whether the term is current
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// List of courses in the term
        /// </summary>
        public List<CourseDomainObjBasic> Courses { get; set; }

        /// <summary>
        /// List of holidays within the term
        /// </summary>
        public List<HolidayDomainObj> Holidays { get; set; }

        /// <summary>
        /// WorkingGroup associated with the term
        /// </summary>
        public WorkingGroupDomainObjBasic WorkingGroup { get; set; }


        /// <summary>
        /// This gets used to build lists of Courses by Term.
        /// </summary>
        public List<CourseDomainObjBasic> CourseList { get; set; }

        /// <summary>
        /// This gets used to build lists of Users by Term.
        /// </summary>
        public List<UserDomainObjBasic> UserList { get; set; }

    }
}