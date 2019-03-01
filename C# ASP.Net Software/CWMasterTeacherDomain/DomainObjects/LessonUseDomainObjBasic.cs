using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class LessonUseDomainObjBasic
    {
        private Guid _id;
        private String _name;

        /// <summary>
        /// The name of this LessonUseDomainObjBasic.
        /// </summary>
        public String Name
        {
            get
            {
                if (_name != null)
                    return _name;
                else
                    return "_?_";
            }
            set
            {
                _name = value;
            }
        }


        /// <summary>
        /// Unique identifier of this LessonUseDomainObjBasic.
        /// </summary>
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



    }//End Class
}
