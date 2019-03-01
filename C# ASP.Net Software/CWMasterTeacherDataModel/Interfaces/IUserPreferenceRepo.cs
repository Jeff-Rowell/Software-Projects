using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IUserPreferenceRepo : IRepository<UserPreference>
    {
        UserPreference CreateUserPreference(string name);
    }
}
