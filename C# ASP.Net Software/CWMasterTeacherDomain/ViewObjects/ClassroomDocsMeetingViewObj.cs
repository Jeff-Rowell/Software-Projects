using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class ClassroomDocsMeetingViewObj
    {

        public string Heading { get; set; }

        public bool ShowReferenceDocs { get; set; }

        public List<ClassroomDocsLessonViewObj> DocsLessonList { get; set; }

        public Guid ClassMeetingId { get; set; }

    }
}
