using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class HolidayDomainObjBasic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id of the basic object</param>
        /// <param name="name">The name of the basic object</param>
        public HolidayDomainObjBasic(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id
        {
            get; set;
        }

        public string Name
        {

            get; set;
        }
    }
}
