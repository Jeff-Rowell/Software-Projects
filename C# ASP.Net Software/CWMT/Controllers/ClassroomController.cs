using CWMasterTeacherDataModel;
using System;
using System.Web.Mvc;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMT.ReturnModels;

namespace CWMasterTeacher3
{
    [Authorize]
    public class ClassroomController : BaseController
    {
        private UserDomainObjBuilder _userObjBuilder;
        private ClassroomCUDRouter _classroomCUDRouter;
        private ClassroomViewObjBuilder _viewObjBuilder;
        private StudentManagerViewObjBuilder _studentManagerViewObjBuilder;

        public ClassroomController(UserDomainObjBuilder userObjBuilder, ClassroomViewObjBuilder classroomViewObjBuilder,
                            ClassroomCUDRouter classroomCUDRouter, StudentManagerViewObjBuilder studentManagerViewObjBuilder) :base(userObjBuilder)
        {
            _userObjBuilder = userObjBuilder;
            _viewObjBuilder = classroomViewObjBuilder;
            _classroomCUDRouter = classroomCUDRouter;
            _studentManagerViewObjBuilder = studentManagerViewObjBuilder;
        }

        public ActionResult Index(Guid? selectedTermId = null, Guid? selectedClassMeetingId = null, DateTime? selectedDate = null, 
                    bool showAllDates = false, bool showReferenceDocs = false, bool doShowStudentManager = false, 
                    Guid? classSectionId = null, bool showAllTerms = false)
        {
            try
            {
                ClassroomViewObj viewObj = _viewObjBuilder.RetrieveClassroomViewObj(currentUserId: CurrentUserId,
                                                                             selectedTermId: selectedTermId ?? Guid.Empty,
                                                                             selectedClassMeetingId: selectedClassMeetingId ?? Guid.Empty,
                                                                             showAllDates: showAllDates,
                                                                             selectedDate: selectedDate,
                                                                             showReferenceDocs: showReferenceDocs,
                                                                             doShowStudentManager: doShowStudentManager,
                                                                             classSectionId: classSectionId == null ? Guid.Empty : classSectionId.Value,
                                                                             showAllTerms: showAllTerms );


                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.ClassroomControllerIndexFailed(e);
                throw;
            }

        }

        public ActionResult ClassroomPlan(Guid? selectedClassMeetingId = null, bool isPreview = false)
        {
            try
            {
                ClassroomPlanViewObj viewObj = _viewObjBuilder.RetrieveClassroomPlanViewObj(selectedClassMeetingId ?? Guid.Empty, isPreview);
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.ClassroomPlanFailed(e);
                throw;
            }

        }

        public ActionResult ClassroomWrap(Guid? classSectionId = null, Guid? selectedClassMeetingId = null, int awaitingApprovalCount = -1, bool doAllowEditing = false)
        {
            ClassroomWrapViewObj viewObj = _viewObjBuilder.RetrieveClassroomWrapViewObj(selectedClassMeetingId: selectedClassMeetingId == null ? Guid.Empty : selectedClassMeetingId.Value,
                                                                                            classSectionId: classSectionId == null ? Guid.Empty : classSectionId.Value,
                                                                                            awaitingApprovalCount: awaitingApprovalCount < 0 ? 0 : awaitingApprovalCount,
                                                                                            doAllowEditing: doAllowEditing);
            return View(viewObj);
        }

        public ActionResult StudentManager(Guid? classSectionId = null, Guid? selectedClassMeetingId = null)
        {
            StudentManagerViewObj viewObj = _studentManagerViewObjBuilder.Retrieve(classSectionId: classSectionId == null ? Guid.Empty : classSectionId.Value,
                                                            selectedClassMeetingId: selectedClassMeetingId == null ? Guid.Empty : selectedClassMeetingId.Value );
            return View(viewObj);
        }

        public ActionResult UpdateClassSectionStudent(Guid classSectionStudentId, Guid selectedClassMeetingId, ClassSectionStudentReturnModel xStudentObj)
        {
            _classroomCUDRouter.UpdateClassSectionStudent(classSectionStudentId: classSectionStudentId, 
                                                            hasBeenApproved: xStudentObj.HasBeenApproved, hasBeenDenied: xStudentObj.HasBeenDenied);
            return RedirectToAction("Index", new {selectedClassMeetingId = selectedClassMeetingId, doShowStudentManager = true });
        }

        public ActionResult ReleaseDocument(Guid documentId, Guid lessonUseId, Guid selectedClassMeetingId)
        {
            _classroomCUDRouter.ReleaseDocument(documentId, lessonUseId);
            return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId });
        }

        public ActionResult ReleaseAllDocuments(Guid lessonUseId, Guid selectedClassMeetingId)
        {
            _classroomCUDRouter.ReleaseAllDocuments(lessonUseId);
            return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId });
        }

        public ActionResult UnReleaseDocument(Guid releasedDocumentId, Guid selectedClassMeetingId)
        {
            _classroomCUDRouter.UnReleaseDocument(releasedDocumentId);
            return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId });
        }

        public ActionResult UnReleaseAllDocuments(Guid lessonUseId, Guid selectedClassMeetingId)
        {
            _classroomCUDRouter.UnReleaseAllDocumentsForLessonUse(lessonUseId);
            return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId });
        }

        public ActionResult SaveNotesForStudents(string notesForStudents, Guid selectedClassMeetingId)
        {
            _classroomCUDRouter.SaveNotesForStudents(notesForStudents, selectedClassMeetingId);
            return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId });
        }

        public ActionResult PreviewClassroomPlan(Guid? selectedClassMeetingId, Guid selectedClassSectionId)
        {
            try
            {
                return RedirectToAction("Index", new
                {
                    selectedClassMeetingId = selectedClassMeetingId ?? Guid.Empty,
                    classSectionId = selectedClassSectionId,
                });
            }
            catch(Exception e)
            {
                WebLogger.Log.PreviewClassroomPlanFailed(e);
                throw;
            }
        }

        public ActionResult ClassroomDocuments(Guid? selectedClassMeetingId = null, bool showReferenceDocs = false)
        {
            try
            {
                ClassroomDocsMeetingViewObj viewObj = _viewObjBuilder.RetrieveClassroomDocsMeetingViewObj(selectedClassMeetingId ?? Guid.Empty, showReferenceDocs);
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.ClassroomDocumentsFailed(e);
                throw;
            }

        }


        public ActionResult SetClassMeetingIsReady(ClassMeetingDomainObjBasic xMeeting, ClassroomReturnModel model)
        {
            try
            {
                _classroomCUDRouter.SetClassMeetingIsReady(xMeeting.Id, xMeeting.IsReadyToTeach);
                return RedirectToAction("Index", new
                {
                    selectedTermId = model.SelectedTermId,
                    selectedClassMeetingId = model.SelectedClassMeetingId,
                    selectedDate = model.SelectedDate,
                    showAllDates = model.ShowAllDates,
                    showReferenceDocs = model.ShowReferenceDocs
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.SetClassMeetingIsReadyFailed(e);
                throw;
            }

        }

        public ActionResult MoveLessonForward(Guid selectedClassMeetingId, Guid selectedLessonUseId)
        {
            _classroomCUDRouter.MoveLessonForward(selectedLessonUseId);
            return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId });
        }

        public ActionResult CopyLessonForward(Guid selectedClassMeetingId, Guid selectedLessonUseId)
        {
            _classroomCUDRouter.CopyLessonForward(selectedLessonUseId);
            return RedirectToAction("Index", new { selectedClassMeetingId = selectedClassMeetingId });
        }

    }//End Class
}
