using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain
{
    public class StashedNarrativeDomainObj
    {
        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public Guid NarrativeId { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsSaved { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsLessonMaster { get; set; }

        public string DisplayName
        {
            get
            {
                string prefix = "";
                if (IsLessonMaster)
                {
                    prefix = "Master ";
                }
                else
                {
                    prefix = UserDisplayName + "'s ";
                }
                return prefix + NarrativeName + " (" + DateModified.ToString("M/d/yyyy h:m") + ")";
            }
        }

        public string UserDisplayName { private get; set; }
        public string NarrativeName { private get; set; }

        /// <summary>
        /// Used for testing
        /// </summary>
        public string NarrativeIdString
        {
            get { return NarrativeId.ToString(); }
        }

    }
}
