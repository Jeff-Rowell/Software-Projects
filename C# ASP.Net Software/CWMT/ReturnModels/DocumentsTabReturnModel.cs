using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherDomain.ViewObjects;

namespace CWMasterTeacher3
{
    public class DocumentsTabReturnModel
    {

        public Guid SelectedDocumentUseId { get; set; }

        public string SelectedDocumentName { get; set; }

        public string SelectedDocumentUseModificationNotes { get; set; }

        public bool IsPdfCopyUpload { get; set; }

        public Guid SelectedLessonId { get; set; }

        public bool SelectedDocumentUseIsReference { get; set; }

        public bool SelectedDocumentUseIsInstructorOnly { get; set; }

    }//End Class
}