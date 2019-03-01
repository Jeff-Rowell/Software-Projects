using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class TermDomainObjBasic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id of the basic object</param>
        /// <param name="name">The name of the basic object</param>
        public TermDomainObjBasic(Guid id, string name)
        {
            Id = id;
            Name = name;
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