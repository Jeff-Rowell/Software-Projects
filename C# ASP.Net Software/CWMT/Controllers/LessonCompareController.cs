using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacher3;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.ViewObjectBuilder;

namespace CWMT
{
    public class LessonCompareController : BaseController
    {

        private LessonCompareViewObjBuilder _lessonCompareBuilder;
        private LessonCompareCUDRouter _lessonCompareCUDRouter;
        private EmptyEditableViewObjBuilder _emptyEditableBuilder;

        public LessonCompareController(UserDomainObjBuilder userObjBuilder,
                                        LessonCompareViewObjBuilder lessonCompareBuilder,
                                        LessonCompareCUDRouter lessonCompareCUDRouter,
                                        EmptyEditableViewObjBuilder emptyEditableBuilder)
            : base(userObjBuilder)
        {
            _lessonCompareBuilder = lessonCompareBuilder;
            _lessonCompareCUDRouter = lessonCompareCUDRouter;
            _emptyEditableBuilder = emptyEditableBuilder;
        }

        //[HttpGet]
        public ActionResult Index(Guid? editableLessonId = null, Guid? comparisonLessonId = null, Guid? specifiedLessonPlanId = null,
                                    Guid? specifiedNarrativeId = null,
                                    DateTime? lessonPlanReferenceTime = null, DateTime? narrativeDateTime = null,
                                    DateTime? documentsReferenceTime = null, bool doSuggestUseMaster = false,
                                    bool isDisplayModeNarrative = false, bool isDisplayModeLessonPlan = false, bool isDisplayModeDocuments = false,
                                    Guid? comparisonSelectedDocumentUseId = null, Guid? editableSelectedDocumentUseId = null,
                                    bool doLockEditableLesson = false, bool doLockComparisonLesson = false, Guid? editableCourseId = null,
                                    Guid? lastEditableLessonId = null, Guid? containerLessonId = null, Guid? containerCourseId = null,
                                    int comparisonModeInt = -1)
        {

            LessonCompareViewObj viewObj = new LessonCompareViewObj();
            viewObj = _lessonCompareBuilder.Retrieve(editableLessonId: editableLessonId == null ? Guid.Empty : editableLessonId.Value,
                                                                            comparisonLessonId: comparisonLessonId == null ? Guid.Empty : comparisonLessonId.Value,
                                                                            specifiedLessonPlanId: specifiedLessonPlanId == null ? Guid.Empty : specifiedLessonPlanId.Value,
                                                                            specifiedNarrativeId: specifiedNarrativeId == null ? Guid.Empty : specifiedNarrativeId.Value,
                                                                            currentUserObj: CurrentUserObj, lessonPlanReferenceTime: lessonPlanReferenceTime,
                                                                            narrativeReferenceTime: narrativeDateTime, documentsReferenceTime: documentsReferenceTime,
                                                                            doSuggestUseMaster: doSuggestUseMaster, isDisplayModeNarrative: isDisplayModeNarrative,
                                                                            isDisplayModeLessonPlan: isDisplayModeLessonPlan, isDisplayModeDocuments: isDisplayModeDocuments,
                                                                            comparisonSelectedDocumentUseId: comparisonSelectedDocumentUseId == null ? Guid.Empty : comparisonSelectedDocumentUseId.Value,
                                                                            editableSelectedDocumentUseId: editableSelectedDocumentUseId == null ? Guid.Empty : editableSelectedDocumentUseId.Value,
                                                                            doLockEditableLesson: doLockEditableLesson, doLockComparisonLesson: doLockComparisonLesson,
                                                                            lastEditableLessonId: lastEditableLessonId == null ? Guid.Empty : lastEditableLessonId.Value,
                                                                            containerLessonId: containerLessonId == null ? Guid.Empty : containerLessonId.Value,
                                                                            containerCourseId: containerCourseId == null ? Guid.Empty : containerCourseId.Value,
                                                                            comparisonModeInt: comparisonModeInt
                                                                            );

            ModelState.Clear();
            return View(viewObj);
        }

        public ActionResult ViewComparisonLessonsMode(Guid? editableLessonId = null,
                                         bool isDisplayModeNarrative = false,
                                         bool isDisplayModeLessonPlan = false,
                                         bool isDisplayModeDocuments = false)
        {
            try
            {

                return RedirectToAction("Index", new
                {
                    editableLessonId = editableLessonId,
                    isDisplayModeNarrative = isDisplayModeNarrative,
                    isDisplayModeLessonPlan = isDisplayModeLessonPlan,
                    isDisplayModeDocuments = isDisplayModeDocuments,
                    comparisonModeInt = DomainUtilities.ComparisonModeGetIntFromEnum( DomainUtilities.ComparisonMode.ShowComparisonLessons) 
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult ViewArchiveMode(Guid? editableLessonId = null,
                                 bool isDisplayModeNarrative = false,
                                 bool isDisplayModeLessonPlan = false,
                                 bool isDisplayModeDocuments = false)
        {
            try
            {

                return RedirectToAction("Index", new
                {
                    editableLessonId = editableLessonId,
                    comparisonLessonId = editableLessonId,  //This is important for things to work right.
                    isDisplayModeNarrative = isDisplayModeNarrative,
                    isDisplayModeLessonPlan = isDisplayModeLessonPlan,
                    isDisplayModeDocuments = isDisplayModeDocuments,
                    comparisonModeInt = DomainUtilities.ComparisonModeGetIntFromEnum(DomainUtilities.ComparisonMode.ViewArchive) 
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult ViewImportContentMode(Guid? editableLessonId = null,
                                 Guid? comparisonLessonId = null,
                                 bool isDisplayModeNarrative = false,
                                 bool isDisplayModeLessonPlan = false,
                                 bool isDisplayModeDocuments = false)
        {
            try
            {
                return RedirectToAction("Index", "CompareSelect", new
                {referenceEditableLessonId = editableLessonId,
                    isDisplayModeNarrative = isDisplayModeNarrative,
                    isDisplayModeLessonPlan = isDisplayModeLessonPlan,
                    isDisplayModeDocuments = isDisplayModeDocuments,
                    comparisonModeInt = DomainUtilities.ComparisonModeGetIntFromEnum(DomainUtilities.ComparisonMode.ImportContent)
                });

            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult ViewImportLessonMode(Guid editableLessonId, 
                                 bool isDisplayModeNarrative = false,
                                 bool isDisplayModeLessonPlan = false,
                                 bool isDisplayModeDocuments = false
                                 )
        {
            try
            {
                return RedirectToAction("Index", "CompareSelect", new
                {
                    referenceEditableLessonId = editableLessonId,
                    isDisplayModeNarrative = isDisplayModeNarrative,
                    isDisplayModeLessonPlan = isDisplayModeLessonPlan,
                    isDisplayModeDocuments = isDisplayModeDocuments,
                    comparisonModeInt = DomainUtilities.ComparisonModeGetIntFromEnum(DomainUtilities.ComparisonMode.ImportLesson)
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult ViewReviewChangesToMasterMode(Guid? editableLessonId = null,
                                 bool isDisplayModeNarrative = false,
                                 bool isDisplayModeLessonPlan = false,
                                 bool isDisplayModeDocuments = false)
        {
            try
            {
                return RedirectToAction("Index", "CompareSelect", new
                {
                    referenceEditableLessonId = editableLessonId,
                    isDisplayModeNarrative = isDisplayModeNarrative,
                    isDisplayModeLessonPlan = isDisplayModeLessonPlan,
                    isDisplayModeDocuments = isDisplayModeDocuments,
                    comparisonModeInt = DomainUtilities.ComparisonModeGetIntFromEnum(DomainUtilities.ComparisonMode.UpdateIndivFromMaster)
                });

            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult ViewReviewChangesToIndividualMode(Guid? editableLessonId = null,
                                 bool isDisplayModeNarrative = false,
                                 bool isDisplayModeLessonPlan = false,
                                 bool isDisplayModeDocuments = false)
        {
            try
            {
                return RedirectToAction("Index", "CompareSelect", new
                {
                    referenceEditableLessonId = editableLessonId,
                    isDisplayModeNarrative = isDisplayModeNarrative,
                    isDisplayModeLessonPlan = isDisplayModeLessonPlan,
                    isDisplayModeDocuments = isDisplayModeDocuments,
                    comparisonModeInt = DomainUtilities.ComparisonModeGetIntFromEnum(DomainUtilities.ComparisonMode.UpdateMasterFromIndiv)
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult SetLockEditableOrComparisonLesson(LessonCompareReturnModel model)
        {
            try
            {

                return RedirectToAction("Index", new
                {
                    editableLessonId = model.EditableLessonId,
                    comparisonLessonId = model.ComparisonLessonId,
                    isDisplayModeNarrative = model.IsDisplayModeNarrative,
                    isDisplayModeLessonPlan = model.IsDisplayModeLessonPlan,
                    isDisplayModeDocuments = true,
                    doLockEditableLesson = model.DoLockEditableLesson,
                    doLockComparisonLesson = model.DoLockComparisonLesson,
                    comparisonModeInt = model.ComparisonModeInt
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        //Lesson Plan Stuff ******************************************************************************************************
        public ActionResult LessonPlanUpdate(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.AddOrUpdateLessonPlan(lessonPlanId: model.EditableLessonPlanId, lessonId: model.EditableLessonId, userId: CurrentUserId,
                                                            text: model.EditableLessonPlanText, name: "", isPreNote: false,
                                                            isPostNote: false);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId ,
                                                        comparisonLessonId = model.ComparisonLessonId,
                                                        doLockEditableLesson = model.DoLockEditableLesson,
                                                        doLockComparisonLesson = model.DoLockComparisonLesson,
                                                        comparisonModeInt = model.ComparisonModeInt 
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure (e);
                throw;
            }
        }

        public ActionResult StashEditableLessonPlan(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.CreateStashedLessonPlan(model.EditableLessonId, model.EditableLessonPlanId, true);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId,
                                                        comparisonLessonId = model.ComparisonLessonId,
                                                        doLockEditableLesson = model.DoLockEditableLesson,
                                                        doLockComparisonLesson = model.DoLockComparisonLesson,
                                                        comparisonModeInt = model.ComparisonModeInt
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }
        public ActionResult StashComparisonLessonPlan(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.CreateStashedLessonPlan(model.EditableLessonId, model.ComparisonLessonPlanId, true);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId,
                                                        comparisonLessonId = model.ComparisonLessonId,
                                                        doLockEditableLesson = model.DoLockEditableLesson,
                                                        doLockComparisonLesson = model.DoLockComparisonLesson,
                                                        comparisonModeInt = model.ComparisonModeInt
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult ImportLessonPlanAsCurrent(LessonCompareReturnModel model)
        {
            try
            {
                bool doCheckStashForDuplicate = model.ComparisonLessonId == model.EditableLessonId;
                _lessonCompareCUDRouter.ImportLessonPlanAsCurrent(model.EditableLessonId, model.ComparisonLessonPlanId, doCheckStashForDuplicate);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId,
                                                        comparisonLessonId = model.ComparisonLessonId,
                                                        doLockEditableLesson = model.DoLockEditableLesson,
                                                        doLockComparisonLesson = model.DoLockComparisonLesson,
                                                        comparisonModeInt = model.ComparisonModeInt
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult UpdateLessonPlanChangesAcceptedDate(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.UpdateLessonPlanChangesAcceptedDate(editableLessonId: model.EditableLessonId,
                                                                        editableLessonPlanId: model.EditableLessonPlanId,
                                                                        comparisonLessonPlanId: model.ComparisonLessonPlanId, 
                                                                        doSuggestUseMaster: model.DoSuggestUseMaster);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, comparisonModeInt = model.ComparisonModeInt});
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult LessonPlanGoBackInTime(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.SetLessonPlanDateChangesAcceptedBackInTime(editableLessonId: model.EditableLessonId,
                                                                                    editableLessonPlanId: model.EditableLessonPlanId,
                                                                                    numberOfDaysBack: model.BackInTimeValue);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId,
                                                    isDisplayModeLessonPlan = true,
                                                    comparisonModeInt = model.ComparisonModeInt});
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult LessonPlanReturnToReferenceDate(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.ReturnToLessonPlanReferenceChangesAcceptedDate(model.EditableLessonId);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, comparisonModeInt = model.ComparisonModeInt });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        //Narrative Stuff*********************************************************************************************************************
        public ActionResult NarrativeUpdate(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.AddOrUpdateNarrative(narrativeId: model.EditableNarrativeId, lessonId: model.EditableLessonId, userId: CurrentUserId,
                                                            text: model.EditableNarrativeText, name: "", isPreNote: false,
                                                            isPostNote: false);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, comparisonLessonId = model.ComparisonLessonId,
                                            isDisplayModeNarrative = true, comparisonModeInt = model.ComparisonModeInt});
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult StashEditableNarrative(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.CreateStashedNarrative(model.EditableLessonId, model.EditableNarrativeId, true);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, comparisonLessonId = model.ComparisonLessonId,
                                                        isDisplayModeNarrative = true, comparisonModeInt = model.ComparisonModeInt});
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult RemoveNarrativeFromArchive(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.RemoveStashedNarrativeFromArchive(model.ComparisonLessonId, model.ComparisonNarrativeId);
                return RedirectToAction("Index", new
                {
                    editableLessonId = model.EditableLessonId,
                    comparisonLessonId = model.ComparisonLessonId,
                    isDisplayModeNarrative = true,
                    comparisonModeInt = model.ComparisonModeInt
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult StashComparisonNarrative(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.CreateStashedNarrative(model.EditableLessonId, model.ComparisonNarrativeId, true);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId,
                                                    comparisonLessonId = model.ComparisonLessonId,
                                                    isDisplayModeNarrative = true,
                                                    comparisonModeInt = model.ComparisonModeInt
                                                    });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult ImportNarrativeAsCurrent(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.ImportNarrativeAsCurrent(model.EditableLessonId, model.ComparisonNarrativeId);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId,
                                                    comparisonLessonId = model.ComparisonLessonId,
                                                    isDisplayModeNarrative = true,
                                                    comparisonModeInt = model.ComparisonModeInt});
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult UpdateNarrativeChangesAcceptedDate(LessonCompareReturnModel model, Guid comparisonLessonId)
        {
            try
            {
                _lessonCompareCUDRouter.UpdateNarrativeChangesAcceptedDate(editableLessonId: model.EditableLessonId,
                                                                        comparisonLessonId: model.ComparisonLessonId,
                                                                        doSuggestRemoveComment: model.DoSuggestRemoveComment);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, isDisplayModeNarrative = true,
                                                        comparisonModeInt = model.ComparisonModeInt });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult NarrativeGoBackInTime(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.SetNarrativeDateChangesAcceptedBackInTime(editableLessonId: model.EditableLessonId,
                                                                        numberOfDaysBack: model.NarrativeBackInTimeValue);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, isDisplayModeNarrative = true,
                                                        comparisonModeInt = model.ComparisonModeInt });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult NarrativeReturnToReferenceDate(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.ReturnToNarrativeReferenceChangesAcceptedDate(editableLessonId: model.EditableLessonId);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, isDisplayModeNarrative = true,
                                                    comparisonModeInt = model.ComparisonModeInt });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }


        //Documents Stuff ************************************************************************************************************************

        public ActionResult UpdateDocumentsChangesAcceptedDate(LessonCompareReturnModel model)
        {
            _lessonCompareCUDRouter.UpdateDocumentsChoiceConfirmedDate(model.EditableLessonId, model.ComparisonLessonId);
            return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, isDisplayModeDocuments = true,
                                                    comparisonModeInt = model.ComparisonModeInt });
        }

        public ActionResult ImportSelectedDocument(LessonCompareReturnModel model)
        {
            _lessonCompareCUDRouter.ImportDocumentIntoLesson(model.EditableLessonId, model.ComparisonSelectedDocumentUseId);
            return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId,
                                                    comparisonLessonId = model.ComparisonLessonId,
                                                    doLockEditableLesson = model.DoLockEditableLesson,
                                                    doLockComparisonLesson = model.DoLockComparisonLesson,
                                                    isDisplayModeDocuments = true,
                                                    comparisonModeInt = model.ComparisonModeInt
            });
        }

        public ActionResult DocumentsGoBackInTime(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.SetDocumentsDateChoiceConfirmeddBackInTime(editableLessonId: model.EditableLessonId, 
                                                                        numberOfDaysBack: model.BackInTimeValue);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, isDisplayModeDocuments = true,
                                                       comparisonModeInt = model.ComparisonModeInt });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult DocumentsReturnToReferenceDate(LessonCompareReturnModel model)
        {
            try
            {
                _lessonCompareCUDRouter.ReturnToReferenceDateDocumentChoiceConfirmed(model.EditableLessonId);
                return RedirectToAction("Index", new { editableLessonId = model.EditableLessonId, isDisplayModeDocuments = true,
                                                        comparisonModeInt = model.ComparisonModeInt });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult EmptyEditablePartial(Guid? containerLessonId = null, 
                                                 Guid? comparisonLessonId = null, Guid? lastEditableLessonId = null,
                                                 bool isDisplayModeNarrative = false, bool isDisplayModeLessonPlan = false,
                                                 bool isDisplayModeDocuments = false, bool doLockEditableLesson = false,
                                                 bool doLockComparisonLesson = false, Guid? containerCourseId = null,
                                                 int comparisonModeInt = -1)
        {
            EmptyEditableViewObj viewObj = _emptyEditableBuilder.Retrieve(comparisonLessonId: comparisonLessonId == null ? Guid.Empty : comparisonLessonId.Value,
                                                                          lastEditableLessonId: lastEditableLessonId == null ? Guid.Empty : lastEditableLessonId.Value,
                                                                          isDisplayModeNarrative: isDisplayModeNarrative,
                                                                          isDisplayModeLessonPlan: isDisplayModeLessonPlan,
                                                                          isDisplayModeDocuments: isDisplayModeDocuments,
                                                                          containerLessonId: containerLessonId == null ? Guid.Empty : containerLessonId.Value,
                                                                          doLockEditableLesson: doLockEditableLesson,
                                                                          doLockComparisonLesson: doLockComparisonLesson,
                                                                          containerCourseId: containerCourseId == null ? Guid.Empty : containerCourseId.Value,
                                                                          comparisonModeInt: comparisonModeInt 
                                                                            );
            return View(viewObj);
        }

        public ActionResult ReturnToEditableLesson(bool doLockEditableLesson, Guid lastEditableLessonId, Guid comparisonLessonId,
                                                   bool isDisplayModeNarrative, bool isDisplayModeLessonPlan, bool isDisplayModeDocuments,
                                                   bool doLockComparisonLesson, int comparisonModeInt = -1)
        {
            Guid editableLessonId = doLockEditableLesson ? lastEditableLessonId : Guid.Empty;

            return RedirectToAction("Index", new
            {
                editableLessonId = editableLessonId,
                comparisonLessonId = comparisonLessonId,
                doLockEditableLesson = doLockEditableLesson,
                doLockComparisonLesson = doLockComparisonLesson,
                isDisplayModeNarrative = isDisplayModeNarrative,
                isDisplayModeLessonPlan = isDisplayModeLessonPlan,
                isDisplayModeDocuments = isDisplayModeDocuments,
                lastEditableLessonId = lastEditableLessonId,
                comparisonModeInt = comparisonModeInt
            });
        }

        public ActionResult ImportLesson(bool doLockEditableLesson, Guid lastEditableLessonId, Guid comparisonLessonId,
                                                   bool isDisplayModeNarrative, bool isDisplayModeLessonPlan, bool isDisplayModeDocuments,
                                                   bool doLockComparisonLesson, Guid containerLessonId, int comparisonModeInt = -1)
        {
            Guid newLessonId = _lessonCompareCUDRouter.ImportLesson_ReturnId(fromLessonId: comparisonLessonId,
                                                                             containerLessonId: containerLessonId,
                                                                             toCourseLessonId: lastEditableLessonId);

            return RedirectToAction("Index", "Curriculum", new {lessonId = newLessonId });
        }

        public ActionResult SetIsHidden(LessonCompareReturnModel model)
        {
            _lessonCompareCUDRouter.SetIsHidden(lessonId: model.EditableLessonId, isHidden: model.EditableLessonIsHidden);
            return RedirectToAction("Index", new {
                editableLessonId = model.EditableLessonId,
                comparisonLessonId = model.ComparisonLessonId,
                isDisplayModeNarrative = model.IsDisplayModeNarrative,
                isDisplayModeLessonPlan = model.IsDisplayModeLessonPlan,
                isDisplayModeDocuments = model.IsDisplayModeDocuments,
                comparisonModeInt = model.ComparisonModeInt
            });
        }


        public ActionResult RemoveDocumentFromLesson(LessonCompareReturnModel model)
        {
            _lessonCompareCUDRouter.RemoveDocumentFromLesson(documentUseId: model.EditableSelectedDocumentUseId);
            return RedirectToAction("Index", new
            {
                editableLessonId = model.EditableLessonId,
                comparisonLessonId = model.ComparisonLessonId,
                isDisplayModeNarrative = model.IsDisplayModeNarrative,
                isDisplayModeLessonPlan = model.IsDisplayModeLessonPlan,
                isDisplayModeDocuments = model.IsDisplayModeDocuments,
                comparisonModeInt = model.ComparisonModeInt
            });
        }

        

    }//End Class
}