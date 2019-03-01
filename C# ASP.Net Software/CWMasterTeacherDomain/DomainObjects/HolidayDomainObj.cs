using System;
using System.Collections.Generic;


namespace CWMasterTeacherDomain.DomainObjects
{
    public class HolidayDomainObj
    {
        private HolidayDomainObjBasic _basicObj;

        /// <summary>
        /// Constructor that takes a basicObj as its parameter
        /// </summary>
        /// <param name="basicObj"></param>
        public HolidayDomainObj(HolidayDomainObjBasic basicObj)
        {
            _basicObj = basicObj;
        }

        /// <summary>
        /// Fully Loaded Constructor that creates a new basicObj and
        /// fills it with the parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="termId"></param>
        /// <param name="name"></param>
        public HolidayDomainObj(Guid id, Guid termId, string name, DateTime date)
        {
            _basicObj = new HolidayDomainObjBasic(id, name);
            Id = id;
            TermId = termId;
            Name = name;
            Date = date;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public HolidayDomainObj()
        {
        }

        /// <summary>
        /// Fully loaded constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="termId"></param>
        /// <param name="name"></param>
        /// <param name="date"></param>
        public HolidayDomainObj(Guid id, Guid termId, string name, DateTime date, TermDomainObj term)
        {
            Id = id;
            TermId = termId;
            Name = name;
            Date = date;
            Term = term;
        }

        /// <summary>
        /// Identifier that is unique to the holiday domain object
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
        /// Holiday identifier
        /// </summary>
        public Guid HolidayId { get; set; }

        /// <summary>
        /// Term idenifier associated with the holiday
        /// </summary>
        public Guid TermId { get; set; }

        /// <summary>
        /// Name of the holiday
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date of the holiday
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Term associated with the holiday
        /// </summary>
        public virtual TermDomainObj Term { get; set; }

        /// <summary>
        /// Display of the holiday
        /// </summary>
        public string DisplayName
        {
            get
            {
                return Date.Date.ToString("MM/dd/yyyy") + "__" + Name;
            }
        }
    }
}

