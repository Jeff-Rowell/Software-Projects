using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CWMasterTeacher3
{
    public class TermAdminReturnModel
    {

        public bool ShowDeleteHoliday { get; set; }



        public Guid SelectedTermId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AddThisHolidayDate { get; set; }
        public string HolidayName { get; set; }
        public Guid SelectedHolidayId { get; set; }
        public bool SelectedTermIsCurrent { get; set; }



    }//End Class
}