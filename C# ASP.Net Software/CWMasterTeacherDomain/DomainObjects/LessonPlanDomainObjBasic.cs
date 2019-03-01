using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain
{
    public class LessonPlanDomainObjBasic
    {
        private Guid _id;
        private string _name;

        public LessonPlanDomainObjBasic(Guid id, string name)
        {
            _id = id;
            _name = name;
        }


        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
    }
}
