using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class WorkingGroupPreferenceRepo : BaseRepo<WorkingGroupPreference>
    {
        public WorkingGroupPreferenceRepo(MasterTeacherContext context)
            : base(context)
        {

        }
        public WorkingGroupPreference CreateWorkingGroupPreference(string name)
        {
            WorkingGroupPreference workGroupPreference = new WorkingGroupPreference();
            workGroupPreference.Name = name;
            Insert(workGroupPreference);

            return workGroupPreference;
        }
    }
}
