using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class CoursePreferenceDomainObjBasic
    {
        public Guid CoursePreferenceId { get; set; }

        public string Name { get; set; }

        public bool DoShowNarrativeNotifications { get; set; }

        
        public bool DoShowLessonPlanNotifications { get; set; }

        public bool DoShowDocumentNotifications { get; set; }

    }
}
