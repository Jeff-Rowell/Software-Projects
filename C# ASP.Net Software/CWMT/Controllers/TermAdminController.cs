using System;
using System.Web.Mvc;
using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3
{
    public class TermAdminController : BaseController
    {
        private TermDomainObj _term;
        private HolidayDomainObj _holiday;
        private TermAdminViewObjBuilder _termAdminViewObjBuilder;
        private TermRepo _termRepo;
        private HolidayRepo _holidayRepo;
        private TermAdminViewObj _termViewObj;
        private TermAdminCUDRouter _termAdminCUDRouter;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepo">A Repository of User Data</param>
        /// <param name="termRepo">A Repository of Term Data</param>
        /// <param name="holidayRepo">A Repository of Holiday Data</param>
        public TermAdminController(UserDomainObjBuilder userObjBuilder, TermRepo termRepo,  
                                HolidayRepo holidayRepo, TermAdminViewObjBuilder termAdminViewObjBuilder, TermAdminCUDRouter termCUDRouter) : base(userObjBuilder)
        {
            _termRepo = termRepo;
            _holidayRepo = holidayRepo;
            _termAdminViewObjBuilder = termAdminViewObjBuilder;
            _termAdminCUDRouter = termCUDRouter;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="selectedTermId">The selected Term's id</param>
        /// <param name="showDeleteHoliday">Whether to show Delete Holiday or not</param>
        /// <param name="selectedHolidayId">The selected Holiday's id</param>
        /// <returns>The ActionResult object</returns>
        //public ActionResult Index(int selectedTermId = -1, bool showDeleteHoliday = false, int selectedHolidayId = -1)
        public ActionResult Index(bool showDeleteHoliday = false, Guid? selectedTermId = null, Guid? selectedHolidayId = null)
        {
            try
            {
                TermAdminViewObj viewObj = _termAdminViewObjBuilder.RetrieveTermAdminViewObj(selectedTermId: selectedTermId ?? Guid.Empty,
                                                                            selectedHolidayId: selectedHolidayId ?? Guid.Empty,
                                                                            currentUserId: CurrentUserId,
                                                                            showDeleteHoliday: showDeleteHoliday);

                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.Term_IndexFailed(e);
                throw;
            }
        }

        //TODO: Look at UI to see if we can do an edit.  The code here should support it if the termId > 0.
        /// <summary>
        /// Creates or edits term
        /// </summary>
        /// <param name="model">Current State of the ViewModel</param>
        /// <returns>The ActionResult object</returns>
        public ActionResult CreateOrEditTerm(TermAdminReturnModel model)
        {
            Guid returnTermId = Guid.Empty;
            try
            {
                returnTermId = _termAdminCUDRouter.CreateOrEditTermReturnId(termId: model.SelectedTermId,
                                                                        name: model.Name,
                                                                        startDate: model.StartDate,
                                                                        endDate: model.EndDate,
                                                                        currentUserId: CurrentUserId
                                                                    );
            }
            catch (Exception e)
            {
                WebLogger.Log.Term_CreateOrEditTermFailed(e);
                throw;
            }
            return RedirectToAction("Index", new { selectedTermId = returnTermId });
        }

        /// <summary>
        /// Adds a Holiday to the database
        /// </summary>
        /// <param name="model">Current State of the ViewModel</param>
        /// <returns>The ActionResult object</returns>
        public ActionResult AddHoliday(TermAdminReturnModel model)
        {
            try
            {
                _termAdminCUDRouter.AddHoliday(model.SelectedTermId, model.HolidayName, model.AddThisHolidayDate);
                return RedirectToAction("Index", new { selectedTermId = model.SelectedTermId });
            }
            catch (Exception e)
            {
                WebLogger.Log.Term_AddHolidayFailed(e);
                throw;
            }
        }

        /// <summary>
        /// Deletes a Holiday from the Database
        /// </summary>
        /// <param name="model">Current State of the ViewModel</param>
        /// <returns>The ActionResult object</returns>
        public ActionResult DeleteHoliday(TermAdminReturnModel model)
        {
            try
            {
                _termAdminCUDRouter.DeleteHoliday(model.SelectedHolidayId);
                return RedirectToAction("Index", new { selectedTermId = model.SelectedTermId });
            }
            catch (Exception e)
            {
                WebLogger.Log.Term_DeleteHolidayFailed(e);
                throw;
            }
        }

        /// <summary>
        /// Marks the current Term as being so
        /// </summary>
        /// <param name="model">Current state of the TermViewModel</param>
        /// <returns></returns>
        public ActionResult MarkTermAsCurrent(TermAdminReturnModel model)
        {
            
            _termAdminCUDRouter.MarkTermAsCurrent(model.SelectedTermId);

            return RedirectToAction("Index", new { selectedTermId = model.SelectedTermId  });
        }

    }
}
