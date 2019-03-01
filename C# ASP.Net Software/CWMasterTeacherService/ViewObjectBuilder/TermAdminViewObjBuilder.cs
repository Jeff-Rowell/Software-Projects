using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System.Web.Mvc;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// Manages CRUD for the Terms
    /// </summary>
    public class TermAdminViewObjBuilder
    {
        private TermDomainObjBuilder _termObjBuilder;
        private HolidayDomainObjBuilder _holidayObjBuilder;
        private UserDomainObjBuilder _userObjBuilder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="termRepo">A Repo containing the Term data, this is just passed on and not used</param>
        public TermAdminViewObjBuilder(TermRepo termRepo, HolidayRepo holidayRepo, TermDomainObjBuilder termBuilder, 
                            HolidayDomainObjBuilder holidayBuilder, UserDomainObjBuilder userObjBuilder)
        {
            _termObjBuilder = termBuilder;
            _holidayObjBuilder = holidayBuilder;
            _userObjBuilder = userObjBuilder;
        }



        /// <summary>
        /// Builds a TermViewObj given a Term Id and a Holiday Id.
        /// </summary>
        /// <param name="termId">The Id of the Term to retrieve</param>
        /// <param name="holidayId">The Id of the Holiday to retrieve</param>
        /// <returns>The view object needed to build a model</returns>
        public TermAdminViewObj RetrieveTermAdminViewObj(Guid selectedTermId, Guid selectedHolidayId, Guid currentUserId, bool showDeleteHoliday)
        {
            TermAdminViewObj viewObj = new TermAdminViewObj();
            List<HolidayDomainObj> holidayList = new List<HolidayDomainObj>();

            UserDomainObj currentUserObj = _userObjBuilder.BuildFromId(currentUserId);
            viewObj.CurrentUser = currentUserObj;

            List<TermDomainObjBasic> termsForInstituionObjList = _termObjBuilder.TermsForWorkingGroup(currentUserObj.WorkingGroupId);

            if (selectedTermId != Guid.Empty) //Which means this is an edit
            {
                TermDomainObj selectedTermObj = _termObjBuilder.BuildFromId(selectedTermId);
                viewObj.TermObj = selectedTermObj;
                holidayList = selectedTermObj.Holidays.ToList();
                viewObj.SubmitButtonText = "Save Changes";
            }
            else
            {
                viewObj.SubmitButtonText = "Create New Term";
            }

            viewObj.HolidaySelectList = new SelectList(holidayList, "Id", "DisplayName");
            viewObj.ShowDeleteHoliday = showDeleteHoliday;
            viewObj.SelectedHolidayId = selectedHolidayId;

            if (selectedHolidayId != Guid.Empty)
            {
                HolidayDomainObj holidayObj = _holidayObjBuilder.BuildFromId(selectedHolidayId);
                viewObj.HolidayName = holidayObj.Name;
            }

            viewObj.TermSelectList = new SelectList(termsForInstituionObjList, "Id", "Name");

            return viewObj;


        }
    }
}
