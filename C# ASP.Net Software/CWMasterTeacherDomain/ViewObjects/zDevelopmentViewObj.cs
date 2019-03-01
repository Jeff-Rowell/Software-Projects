using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class zDevelopmentViewObj
    {
        private bool _userIsAdmin;
        private bool _userIsEditor;
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public bool UserIsEditor
        {
            get { return _userIsEditor; }
            set { _userIsEditor = value; }
        }

        public bool UserIsApplicationAdmin
        {
            get { return _userIsAdmin; }
            set { _userIsAdmin = value; }
        }
    }
}
