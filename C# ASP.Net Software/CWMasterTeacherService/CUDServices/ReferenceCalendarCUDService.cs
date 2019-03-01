using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class ReferenceCalendarCUDService
    {
        private ReferenceCalendarRepo _referenceCalendarRepo;
        public ReferenceCalendarCUDService (ReferenceCalendarRepo referenceCalendarRepo)
        {
            _referenceCalendarRepo = referenceCalendarRepo;
        }

        public ReferenceCalendar CreateReferenceCalendar(Guid classSectionId, Guid referenceClassSectionId)
        {
            ReferenceCalendar referenceCalendar = new ReferenceCalendar();
            referenceCalendar.ClassSectionId = classSectionId;
            referenceCalendar.ReferenceClassSectionId = referenceClassSectionId;
            _referenceCalendarRepo.Insert(referenceCalendar);

            return referenceCalendar;
        }
    }
}
