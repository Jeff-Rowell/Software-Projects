using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class ClassroomPlanViewObj
    {

        public bool IsPreview { get; set; }

        public Guid SelectedClassSectionId { get; set; }

        public string HeadingString { get; set; }

        public string LessonPlanText { get; set; }

        public Guid SelectedClassMeetingId { get; set; }

    }
}
