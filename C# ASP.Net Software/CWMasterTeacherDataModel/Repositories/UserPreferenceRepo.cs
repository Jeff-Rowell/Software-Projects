using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class UserPreferenceRepo : BaseRepo<UserPreference>, IUserPreferenceRepo
    { 
        public UserPreferenceRepo(MasterTeacherContext context)
            : base(context)
        {

        }

        public UserPreference CreateUserPreference(string name)
        {
            UserPreference userPreference = new UserPreference();
            userPreference.Name = name;
            Insert(userPreference);

            return userPreference;
        }

    }

}
