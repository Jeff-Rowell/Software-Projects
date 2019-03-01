using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public static class ContextFactory
    {
        //private static MasterTeacherContext _context;

        public static MasterTeacherContext GetContext
        {
            get
            {
                //if (_context == null)
                //{
                    //_context = new MasterTeacherContext();
                //}
                //return _context;
                return null;
            }
        }


    }
}
