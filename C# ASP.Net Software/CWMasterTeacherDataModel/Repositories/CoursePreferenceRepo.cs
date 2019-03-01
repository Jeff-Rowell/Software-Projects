using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class CoursePreferenceRepo : BaseRepo<CoursePreference>, ICoursePreferenceRepo
    {
        public CoursePreferenceRepo(MasterTeacherContext context)
            : base(context)
        {

        }



    }
}
