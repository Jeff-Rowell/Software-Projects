using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ReferenceCalendarRepo: BaseRepo<ReferenceCalendar>
    {
        private ClassSectionRepo _classSectionRepo;

        public ReferenceCalendarRepo(MasterTeacherContext context, ClassSectionRepo classSectionRepo)
            : base(context)
        {
            _classSectionRepo = classSectionRepo;
        }


        //TODO:  Remove this after redesign is finished.  I moved it to ClassSectionRepo
        public IEnumerable<ClassSection> ReferenceSectionsForClassSection(Guid classSectionId)
        {
            var listQuery = from x in _context.ReferenceCalendars
                                             where x.ClassSectionId == classSectionId
                                             orderby x.ClassSection.Course.Term.StartDate descending
                                             select x;
            List<ReferenceCalendar> calendarList = listQuery.ToList();

            List<ClassSection> sectionList = new List<ClassSection>();

            foreach (var xCalendar in calendarList)
            {
                ClassSection classSection = _classSectionRepo.GetById(xCalendar.ReferenceClassSectionId);
                if (classSection != null)
                {
                    sectionList.Add(classSection);
                }
            }
            return sectionList;
        }




    }
}
