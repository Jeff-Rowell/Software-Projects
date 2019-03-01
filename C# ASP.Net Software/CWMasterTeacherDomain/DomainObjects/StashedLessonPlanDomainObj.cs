using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain
{
    public class StashedLessonPlanDomainObj
    {
        public StashedLessonPlanDomainObj()
        {
        }

        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public Guid LessonPlanId { get; set; }

        public bool IsSaved { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsLessonMaster { get; set; }

        public string DisplayName
        {
            get
            {
                string prefix = "";
                if(IsLessonMaster)
                {
                    prefix = "Master ";
                }
                else
                {
                    prefix = UserDisplayName + "'s ";
                }
                return prefix + LessonPlanName + " (" + DateModified.ToString("M/d/yyyy h:m") + ")";
            }
        }

        public string UserDisplayName { private get; set; }
        public string LessonPlanName { private get; set; }

        public string LessonIdString
        {
            get { return LessonId.ToString(); }
        }



    }
}
