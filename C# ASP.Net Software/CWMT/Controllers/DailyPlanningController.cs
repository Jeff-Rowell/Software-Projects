using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherService;
using CWMasterTeacherDomain;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacher3.ReturnModels;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3.Controllers
{
    [Authorize]
    public class DailyPlanningController : BaseController
    {
        private UserDomainObjBuilder _userObjBuilder;
        private LessonUseRepo _lessonUseRepo;
        private ReferenceCalendarRepo _referenceCalendarRepo;
        private ClassMeetingRepo _classMeetingRepo;
        private CourseRepo _courseRepo;
        private DailyPlanningViewObjBuilder _dailyPlanningViewObjBuilder;
        private ClassMeetingCUDService _classMeetingCUDService;
        private DailyPlanningCUDRouter _dailyPlanningCUDRouter;

        public DailyPlanningController(UserDomainObjBuilder userObjBuilder, LessonUseRepo lessonUseRepo,
                ReferenceCalendarRepo referenceCalendarRepo, ClassMeetingRepo classMeetingRepo, CourseRepo courseRepo,
                DailyPlanningViewObjBuilder dailyPlanningViewObjBuilder, ClassMeetingCUDService classMeetingCUDService,
                DailyPlanningCUDRouter dailyPlanningCUDRouter) : base(userObjBuilder)
        {
            _lessonUseRepo = lessonUseRepo;
            _userObjBuilder = userObjBuilder;
            _referenceCalendarRepo = referenceCalendarRepo;
            _classMeetingRepo = classMeetingRepo;
            _courseRepo = courseRepo;
            _dailyPlanningViewObjBuilder = dailyPlanningViewObjBuilder;
            _classMeetingCUDService = classMeetingCUDService;
            _dailyPlanningCUDRouter = dailyPlanningCUDRouter;
        }

        public ActionResult Index(Guid? selectedClassSectionId = null, Guid? selectedLessonId = null, Guid? referenceClassSectionId = null, 
                                bool showAllTerms = false,Guid? selectedLessonUseId = null, bool showAddReferenceCalendar = false, 
                                int numberOfPossibleRefCalSections = -1,
                                Guid? selectedClassMeetingId = null)
        {
            try
            {
                DailyPlanViewObj viewObj = _dailyPlanningViewObjBuilder.RetrieveDailyPlanViewObj(classSectionId: selectedClassSectionId ?? Guid.Empty,
                                                                                            currentUserId: CurrentUserId,
                                                                                            selectedLessonId: selectedLessonId ?? Guid.Empty,
                                                                                            referenceClassSectionId: referenceClassSectionId ?? Guid.Empty,
                                                                                            showAllTerms: showAllTerms,
                                                                                            selectedLessonUseId: selectedLessonUseId ?? Guid.Empty,
                                                                                            showAddReferenceCalendar: showAddReferenceCalendar,
                                                                                            numberOfPossibleRefCalSections: numberOfPossibleRefCalSections,
                                                                                            selectedClassMeetingId: selectedClassMeetingId ?? Guid.Empty);


                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanningIndexFailed(e);
                throw;
            }
        }

        public ActionResult AddLesson(Guid selectedClassMeetingId, Guid selectedLessonId, Guid selectedClassSectionId, Guid referenceClassSectionId)
        {
            try
            {
                Guid lessonUseId = Guid.Empty;
                if (selectedLessonId != Guid.Empty)
                {
                    lessonUseId = _dailyPlanningCUDRouter.AddLessonUseReturnId(selectedLessonId, selectedClassMeetingId);
                }

                return RedirectToAction("Index", new
                {
                    selectedClassMeetingId = selectedClassMeetingId,
                    selectedClassSectionId = selectedClassSectionId,
                    selectedLessonUseId = lessonUseId,
                    referenceClassSectionId = referenceClassSectionId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_AddLessonFailed(e);
                throw;
            }
        }
        public ActionResult RemoveLesson(Guid selectedClassMeetingId, Guid selectedLessonUseId, Guid selectedClassSectionId, Guid? referenceClassSectionId = null)
        {
            try
            {
                _dailyPlanningCUDRouter.RemoveLesson(selectedLessonUseId);
                return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId,
                                                    selectedClassSectionId = selectedClassSectionId,
                                                        referenceClassSectionId = referenceClassSectionId ?? Guid.Empty
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_RemoveLessonFailed(e);
                throw;
            }
        }
        public ActionResult MoveLessonBack(Guid selectedClassMeetingId, Guid selectedLessonUseId, Guid selectedClassSectionId, Guid referenceClassSectionId)
        {
            try
            {
                _dailyPlanningCUDRouter.MoveLessonBack(selectedLessonUseId);
                return RedirectToAction("Index", new
                {
                    selectedClassMeetingId = selectedClassMeetingId,
                    selectedClassSectionId = selectedClassSectionId,
                    selectedLessonUseId = selectedLessonUseId,
                    referenceClassSectionId = referenceClassSectionId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_MoveLessonBackFailed(e);
                throw;
            }
        }
        public ActionResult MoveLessonForward(Guid selectedClassMeetingId, Guid selectedLessonUseId, Guid selectedClassSectionId, Guid referenceClassSectionId)
        {
            try
            {
                _dailyPlanningCUDRouter.MoveLessonForward(selectedLessonUseId);
                return RedirectToAction("Index", new
                {
                    selectedClassMeetingId = selectedClassMeetingId,
                    selectedClassSectionId = selectedClassSectionId,
                    selectedLessonUseId = selectedLessonUseId,
                    referenceClassSectionId = referenceClassSectionId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_MoveLessonForwardFailed(e);
                throw;
            }
        }

        public ActionResult RaiseSequenceNumber(Guid selectedClassMeetingId, Guid selectedLessonUseId, Guid selectedClassSectionId, Guid referenceClassSectionId)
        {
            try
            {
                _dailyPlanningCUDRouter.RaiseSequenceNumber(selectedLessonUseId);
                return RedirectToAction("Index", new
                {
                    selectedClassMeetingId = selectedClassMeetingId,
                    selectedClassSectionId = selectedClassSectionId,
                    selectedLessonUseId = selectedLessonUseId,
                    referenceClassSectionId = referenceClassSectionId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_RaiseSequenceNumberFailed(e);
                throw;
            }
        }

        public ActionResult LowerSequenceNumber(Guid selectedClassMeetingId, Guid selectedLessonUseId, Guid selectedClassSectionId, Guid referenceClassSectionId)
        {
            try
            {

                _dailyPlanningCUDRouter.LowerSequenceNumber(selectedLessonUseId);

                return RedirectToAction("Index", new
                {
                    selectedClassMeetingId = selectedClassMeetingId,
                    selectedClassSectionId = selectedClassSectionId,
                    selectedLessonUseId = selectedLessonUseId,
                    referenceClassSectionId = referenceClassSectionId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_LowerSequenceNumberFailed(e);
                throw;
            }
        }

        public ActionResult AddReferenceCalendar(Guid selectedClassSectionId, Guid addToReferencesClassSectionId)
        {
            try
            {
                _dailyPlanningCUDRouter.AddReferenceCalendar(selectedClassSectionId, addToReferencesClassSectionId);
                return RedirectToAction("Index", new { selectedClassSectionId = selectedClassSectionId });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_AddReferenceCalendarFailed(e);
                throw;
            }
        }

        public ActionResult CustomizePlan(Guid selectedClassMeetingId, Guid selectedClassSectionId, Guid? selectedLessonUseId = null,
                                    int lessonUseSequenceNumber = -1, Guid? referenceClassSectionId = null)
        {
            //TODO: Pass the reference class section through
            try
            {
                return RedirectToAction("Index", "TextEditor", new
                {
                    editType = "CustomLessonPlan",
                    objectId = selectedLessonUseId ?? Guid.Empty,
                    lessonId = Guid.Empty,
                    selectedClassSectionId = selectedClassSectionId,
                    selectedClassMeetingId = selectedClassMeetingId,
                    lessonUseSequenceNumber = lessonUseSequenceNumber
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_CusomizePlanFailed(e);
                throw;
            }
        }

        public ActionResult CustomLessonPlanUpdate(TextEditorReturnModel model)
        {
            try
            {
                Guid lessonUseId = _dailyPlanningCUDRouter.CustomLessonPlanUpdateReturnId(lessonUseId: model.Id,
                                                                                        selectedClassMeetingId: model.SelectedClassMeetingId,
                                                                                        lessonUseSequenceNumber: model.LessonUseSequenceNumber,
                                                                                        customName: model.NameOrSubject,
                                                                                        customText: model.Text);
                return RedirectToAction("Index", new
                {
                    selectedClassSectionId = model.SelectedClassSectionId,
                    selectedLessonUseId = lessonUseId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_CustomLessonPlanUpdateFailed(e);
                throw;
            }
        }

        public ActionResult CustomLessonPlanUpdateCancel(Guid selectedClassSectionId)
        {
            try
            {
                return RedirectToAction("Index", new
                {
                    selectedClassSectionId = selectedClassSectionId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_CustomLessonPlanUpdateCancelFailed(e);
                throw;
            }
        }

        public ActionResult SetClassMeetingIsReady(Guid selectedClassSectionId,
                                                    Guid selectedLessonUseId,
                                                    ClassMeetingReturnReturnModel xMeeting)
        {
            try
            {
                _dailyPlanningCUDRouter.SetClassMeetingIsReady(xMeeting.ClassMeetingId, xMeeting.IsReadyToTeach);
                return RedirectToAction("Index", new
                {
                    selectedClassMeetingId = xMeeting.ClassMeetingId,
                    selectedClassSectionId = selectedClassSectionId,
                    selectedLessonUseId = selectedLessonUseId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DailyPlanning_SetClassMeetingIsReadyFailed(e);
                throw;
            }
        }

        public ActionResult ToggleLessonIsCollapsed(Guid selectedLessonId, Guid selectedClassSectionId)
        {
            _dailyPlanningCUDRouter.ToggleLessonIsCollapsed(selectedLessonId);
            return RedirectToAction("Index", new { selectedClassSectionId = selectedClassSectionId, selectedLessonId = selectedLessonId });
        }

    }//End class
}
