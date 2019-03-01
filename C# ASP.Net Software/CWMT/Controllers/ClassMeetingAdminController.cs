using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Web.Mvc;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3
{
    /// <summary>
    /// Makes requests to the service for view objects and then passes the view objects to 
    /// the factory which returns a view model.
    /// </summary>
    public class ClassMeetingAdminController : BaseController
    {
        private ClassMeetingAdminViewObjBuilder _classMeetingViewObjBuilder;
        private ClassMeetingAdminCUDRouter _classMeetingAdminCUDRouter;


        public ClassMeetingAdminController(UserDomainObjBuilder userObjBuilder, ClassMeetingAdminViewObjBuilder classMeetingViewObjBuilder,
                                ClassMeetingAdminCUDRouter classMeetingAdminCUDRouter
                                )
            : base(userObjBuilder)
        {
            _classMeetingViewObjBuilder = classMeetingViewObjBuilder;
            _classMeetingAdminCUDRouter = classMeetingAdminCUDRouter;
        }

        /// <summary>
        /// Makes a request to the ClassMeetingService for a view object, then gives the view object
        /// to the factory which returns a ClassMeetingViewModel.
        /// </summary>
        /// <param name="selectedClassSectionId"></param>
        /// <param name="selectedUserId"></param>
        /// <param name="selectedTermId"></param>
        /// <param name="selectedCourseId"></param>
        /// <param name="selectedClassSectionId"></param>
        /// <param name="addSingleClassMeeting"></param>
        /// <param name="selectedClassMeetingId"></param>
        /// <param name="classMeetingIsHoliday"></param>
        /// <param name="showEditNoClass"></param>
        /// <returns></returns>
        public ActionResult Index(Guid? selectedClassSectionId = null, Guid? selectedUserId = null, Guid? selectedTermId = null, Guid? selectedCourseId = null,
                bool addSingleClassMeeting = false, Guid? selectedClassMeetingId = null, bool classMeetingIsHoliday = false,
                bool showEditNoClass = false, bool showRenameClassSection = false, bool showCreateMirrorClassSection = false)
        {
            try
            {
                ClassMeetingAdminViewObj viewObj = _classMeetingViewObjBuilder.buildClassMeetingViewObj(currentUserId: CurrentUserId,
                                                                                                    selectedUserId: selectedUserId == null ? Guid.Empty : selectedUserId.Value,
                                                                                                    selectedTermId: selectedTermId == null ? Guid.Empty : selectedTermId.Value,
                                                                                                    selectedClassSectionId: selectedClassSectionId == null ? Guid.Empty : selectedClassSectionId.Value,
                                                                                                    selectedCourseId: selectedCourseId == null ? Guid.Empty : selectedCourseId.Value,
                                                                                                    selectedClassMeetingId: selectedClassMeetingId == null ? Guid.Empty : selectedClassMeetingId.Value,
                                                                                                    addSingleClassMeeting: addSingleClassMeeting,
                                                                                                    showEditNoClass: showEditNoClass,
                                                                                                    showRenameClassSection: showRenameClassSection,
                                                                                                    showCreateMirrorClassSection: showCreateMirrorClassSection );

                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.ClassMeetingIndexFailed(e);
                throw;
            }

        }

        public ActionResult AddSingleClassMeeting(ClassMeetingAdminReturnModel model)
        {
            try
            {
                _classMeetingAdminCUDRouter.CreateClassMeeting(model.SelectedClassSectionId, model.SingleMeetingDate, model.StartTimeLocal, model.EndTimeLocal);
                return RedirectToAction("Index", new { selectedClassSectionId = model.SelectedClassSectionId });
            }
            catch (Exception e)
            {
                WebLogger.Log.AddSingleClassMeetingFailed(e);
                throw;
            }

        }

        public ActionResult DeleteClassMeeting(Guid selectedClassSectionId, Guid selectedClassMeetingId, Guid selectedTermId, Guid selectedCourseId, bool addSingleClassMeeting)
        {
            try
            {
                _classMeetingAdminCUDRouter.DeleteClassMeeting(selectedClassMeetingId);
                return RedirectToAction("Index", new
                {
                    selectedClassSectionId = selectedClassSectionId,
                    selectedTermId = selectedTermId,
                    selectedCourseId = selectedCourseId,
                    addSingleClassMeeting = addSingleClassMeeting
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DeleteClassMeetingFailed(e);
                throw;
            }
        }

        public ActionResult DeleteClassSection(Guid selectedClassSectionId, Guid selectedTermId, Guid selectedCourseId, bool addSingleClassMeeting)
        {
            try
            {
                _classMeetingAdminCUDRouter.DeleteClassSection(selectedClassSectionId);
                return RedirectToAction("Index", new
                {
                    selectedTermId = selectedTermId,
                    selectedCourseId = selectedCourseId,
                    addSingleClassMeeting = addSingleClassMeeting
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.DeleteClassMeetingFailed(e);
                throw;
            }
        }

        public ActionResult MarkClassMeetingAsNoClass(Guid selectedClassSectionId, Guid selectedClassMeetingId, bool selectedClassMeetingHasNoClass, string noClassComment,
                                        Guid selectedTermId, Guid selectedCourseId, bool addSingleClassMeeting)
        {
            try
            {
                _classMeetingAdminCUDRouter.MarkClassMeetingAsNoClass(selectedClassMeetingId, selectedClassMeetingHasNoClass, noClassComment);
                bool showEditNoClass = selectedClassMeetingHasNoClass && noClassComment.Length <= 3;

                return RedirectToAction("Index", new
                {
                    selectedClassSectionId = selectedClassSectionId,
                    selectedTermId = selectedTermId,
                    selectedCourseId = selectedCourseId,
                    selectedClassMeetingId = selectedClassMeetingId,
                    addSingleClassMeeting = addSingleClassMeeting,
                    showEditNoClass = showEditNoClass 
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.MarkClassMeetingAsNoClassFailed(e);
                throw;
            }
        }

        public ActionResult RenameClassSection(Guid selectedClassSectionId, string selectedClassSectionName)
        {
            _classMeetingAdminCUDRouter.RenameClassSection(classSectionId: selectedClassSectionId, classSectionName: selectedClassSectionName);
            return RedirectToAction("Index", new {selectedClassSectionId = selectedClassSectionId });
        }

        public ActionResult CreateMirrorClassSection(Guid mirrorCourseId,  Guid selectedClassSectionId)
        {
            if(mirrorCourseId != null && mirrorCourseId != Guid.Empty)
            {
                _classMeetingAdminCUDRouter.CreateMirrorClassSection(mirrorCourseId: mirrorCourseId, targetClassSectionId: selectedClassSectionId);
            }
            return RedirectToAction("Index", new { selectedClassSectionId = selectedClassSectionId });
        }

    }// End Class
}
