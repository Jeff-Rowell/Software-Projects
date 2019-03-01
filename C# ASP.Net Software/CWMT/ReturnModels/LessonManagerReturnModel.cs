using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMasterTeacher3
{
    public class LessonManagerReturnModel
    {

        public bool ShowConfirmDelete { get; set; }

        public bool SelectedLessonIsFolder { get; set; }

        public int SequenceNumber { get; set; }

        public Guid ParentId { get; set; }
        
        public bool IsDelete { get; set; }

        public bool IsCreate { get; set; }

        public Guid CourseId { get; set; }

        public string LessonName { get; set; }

        public Guid SelectedLessonId { get; set; }

    }//End Class
}