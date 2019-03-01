using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class HolidayRepo : BaseRepo<Holiday>
    {
        public HolidayRepo(MasterTeacherContext context)
            : base(context)
        { }

        public IEnumerable<Holiday> HolidaysForTerm(Guid termId)
        {
            var listQuery = from x in _context.Holidays
                            where x.TermId == termId
                            orderby x.Name
                            select x;
            return listQuery.ToList();
        }

        public List<DateTime> HolidayDatesForTerm(Guid termId)
        {
            IEnumerable<Holiday> holidays = HolidaysForTerm(termId);
            List<DateTime> dateList = new List<DateTime>();
            foreach (var xHoliday in holidays)
            {
                dateList.Add(xHoliday.Date);
            }
            return dateList;
        }





    }//End Class
}
