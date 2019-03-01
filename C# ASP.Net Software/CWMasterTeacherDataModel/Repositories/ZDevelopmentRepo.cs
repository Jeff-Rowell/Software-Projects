using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ZDevelopmentRepo
    {
        private MasterTeacherContext _context;
        private CourseRepo _courseRepo;

        public ZDevelopmentRepo(MasterTeacherContext context, CourseRepo courseRepo)
        {
            _context = context;
            _courseRepo = courseRepo;
        }











    }//End Class
}
