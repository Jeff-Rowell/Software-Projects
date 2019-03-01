using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class TermAdminCUDRouter
    {
        private TermCUDService _termCUDService;
        private HolidayCUDService _holidayCUDService;
        private UserDomainObjBuilder _userObjBuilder;

        public TermAdminCUDRouter (TermCUDService termCUDService, UserDomainObjBuilder userObjBuilder,
                                    HolidayCUDService holidayCUDService)
        {
            _termCUDService = termCUDService;
            _userObjBuilder = userObjBuilder;
            _holidayCUDService = holidayCUDService;
        }



        public Guid CreateOrEditTermReturnId(Guid termId, string name, DateTime startDate, DateTime endDate, Guid currentUserId)
        {
            if(termId == Guid.Empty) //Which means this is not an edit.
            {
                UserDomainObj userObj = _userObjBuilder.BuildFromId(currentUserId);
                Guid workingGroupId = userObj.WorkingGroupId;
                return _termCUDService.CreateTermReturnId(workingGroupId: workingGroupId,
                                            name: name,
                                            startDate: startDate,
                                            endDate: endDate);
            }
            else
            {
                return _termCUDService.UpdateTermValuesReturnId(termId: termId,
                                                         name: name,
                                                         startDate: startDate,
                                                         endDate: endDate);
            }
        }

        public void AddHoliday(Guid termId, string holidayName, DateTime date)
        {
            _holidayCUDService.CreateHoliday(termId, holidayName, date);
        }

        public void DeleteHoliday(Guid selectedHolidayId)
        {
            _holidayCUDService.DeleteHoliday(selectedHolidayId);
        }

        public void MarkTermAsCurrent(Guid termId)
        {
            _termCUDService.MarkTermAsCurrent(termId);
        }





    }// End Class
}
