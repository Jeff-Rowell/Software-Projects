using CWMasterTeacherDomain.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class zDevelopmentViewObjBuilder
    {
        public zDevelopmentViewObj BuildZDevelopmentViewModel(string message)
        {
            zDevelopmentViewObj viewObj = new zDevelopmentViewObj();
            viewObj.Message = message;
            return viewObj;
        }



    }
}
