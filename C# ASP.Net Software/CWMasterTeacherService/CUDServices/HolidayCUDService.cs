using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class HolidayCUDService
    {
        private HolidayRepo _holidayRepo;
        public HolidayCUDService(HolidayRepo holidayRepo)
        {
            _holidayRepo = holidayRepo;
        }




        public Holiday CreateHoliday(Guid termId, string name, DateTime date)
        {
            Holiday holiday = new Holiday();
            holiday.TermId = termId;
            holiday.Name = name;
            holiday.Date = date;

            _holidayRepo.Insert(holiday);
            return holiday;
        }

        public void DeleteHoliday (Guid holidayId)
        {
            _holidayRepo.Delete(holidayId);
        }
    }
}
