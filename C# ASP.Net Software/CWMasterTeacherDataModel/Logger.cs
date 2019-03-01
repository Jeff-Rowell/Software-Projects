using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class Logger
    {
        protected static Logger _log;

        private Logger() { }

        public   static Logger Log
        {
            get
            {
                if (_log == null)
                {
                    _log = new Logger();
                }
                return _log;
            }
        }

        protected void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void CreatingNewCourse(string name, int termId, int userId, int workingGroupId)
        {
            Write(string.Format("Creating New Course named: {0}, Term: {1}, User: {2}, WorkingGroup: {3}",
                name, termId, userId, workingGroupId));
        }
        public void CreatedNewCourse(string name, int term, int user, int workingGroup, int newCoursId)
        {

        }
  
    }
}
