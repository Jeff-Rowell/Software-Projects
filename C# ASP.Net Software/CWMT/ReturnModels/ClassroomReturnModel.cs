using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMasterTeacher3
{
    public class ClassroomReturnModel
    {
      

        public bool ShowReferenceDocs { get; set; }

        //public string DateLabelString { get; set; }

        //public List<ClassMeetingDomainObjBasic> ClassMeetingObjList { get; set; }

        public bool ShowAllDates { get; set; }

        //public List<DateViewObj> PossibleDatesObjList { get; set; }

        //public bool UserIsEditor { get; set; }

        //public bool UserIsAdmin { get; set; }

        public int SelectedClassMeetingId { get; set; }

        public int SelectedTermId { get; set; }

        //public IEnumerable<SelectListItem> TermSelectList { get; set; }

        public DateTime SelectedDate { get; set; }

        //public string LessonPlanTypeName { get; set; }

    }//End Class
}