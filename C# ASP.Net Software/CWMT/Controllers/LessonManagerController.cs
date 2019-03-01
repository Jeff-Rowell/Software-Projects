using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3.Controllers
{
    public class LessonManagerController : BaseController
    {
        private LessonRepo _lessonRepo;
        private LessonManagerViewObjBuilder _viewObjBuilder;
        private LessonManagerCUDRouter _lessonManagerCUDRouter;

        public LessonManagerController(UserDomainObjBuilder userObjBuilder, LessonRepo lessonRepo,
                                        LessonManagerViewObjBuilder viewObjBuilder, LessonManagerCUDRouter lessonManagerCUDRouter)
            : base(userObjBuilder)
        {
            _lessonRepo = lessonRepo;
            _viewObjBuilder = viewObjBuilder;
            _lessonManagerCUDRouter = lessonManagerCUDRouter;
        }

        public ActionResult Index(Guid? courseId = null, Guid? selectedLessonId = null, Guid? parentId = null,
                                    string lessonName = "", bool isCreate = false, bool isEdit = false,
                                    bool isArchive = false, bool isDelete = false, Guid? lessonLevelId = null, bool showSelectLocation = false,
                                    bool parentIsCourse = false, bool isMove = false, bool showConfirmDelete = false, string deletionMessage = ""
                                    )
        {
            try
            {
                LessonManagerViewObj viewObj = _viewObjBuilder.RetrieveLessonManagerViewObject(courseId: courseId?? Guid.Empty,
                                                                                        selectedLessonId: selectedLessonId ?? Guid.Empty,
                                                                                        parentId: parentId ?? Guid.Empty,
                                                                                        lessonName: lessonName,
                                                                                        isCreate: isCreate,
                                                                                        isEdit: isEdit,
                                                                                        isArchive: isArchive,
                                                                                        isDelete: isDelete,
                                                                                        showSelectLocation: showSelectLocation,
                                                                                        parentIsCourse: parentIsCourse,
                                                                                        isMove: isMove,
                                                                                        currentUserId: CurrentUserId,
                                                                                        showConfirmDelete: showConfirmDelete,
                                                                                        deletionMessage: deletionMessage
                                                                                        );


                ModelState.Clear();
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManagerIndexFailed(e);
                throw;
            }
        }

        public ActionResult CreateOrEditLesson(LessonManagerReturnModel model)
        {
            try
            {
                Guid currentUserId = CurrentUserId;
                _lessonManagerCUDRouter.CreateOrEditLesson(selectedLessonId: model.SelectedLessonId,
                                                            isCreate: model.IsCreate,
                                                            parentId: model.ParentId,
                                                            lessonName: model.LessonName,
                                                            courseId: model.CourseId,
                                                            sequenceNumber: model.SequenceNumber,
                                                            isFolder: model.SelectedLessonIsFolder,
                                                            isHidden: false,
                                                            currentUserId: currentUserId);

                return RedirectToAction("Index", new { selectedLessonId = model.SelectedLessonId, courseId = model.CourseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_CreateOrEditLessonFailed(e);
                throw;
            }
        }

        //TODO: This is deprecated and should be removed once I'm sure it's never being called.
        public ActionResult ToggleLessonOptional(Guid selectedLessonId, Guid courseId)
        {
            try
            {
                return RedirectToAction("Index", new { selectedLessonId = selectedLessonId, courseId = courseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.ToggleLessonOptionalFailed(e);
                throw;
            }
        }

        public ActionResult ToggleLessonIsCollapsed(Guid lessonId)
        {
            _lessonManagerCUDRouter.ToggleLessonIsCollapsed(lessonId);
            return RedirectToAction("Index", new { selectedLessonId = lessonId });
        }

        public ActionResult DeleteLesson(LessonManagerReturnModel model)
        {
            try
            {
                var messageAndId = _lessonManagerCUDRouter.DeleteLessonReturnMessageAndId(model.SelectedLessonId);
                string deletionMessage = messageAndId.Item1;
                Guid lessonId = messageAndId.Item2;

                return RedirectToAction("Index", new { courseId = model.CourseId, deletionMessage = deletionMessage, isDelete = model.IsDelete,
                                            showConfirmDelete = model.ShowConfirmDelete, selectedLessonId = lessonId});
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_DeleteLessonFailed(e);
                throw;
            }
        }

        public ActionResult RaiseSequenceNumber(Guid selectedLessonId, Guid courseId, bool isEdit, bool isMove)
        {
            try
            {
                _lessonManagerCUDRouter.RaiseSequenceNumber(selectedLessonId);

                return RedirectToAction("Index", new { selectedLessonId = selectedLessonId, courseId = courseId, isEdit = isEdit, isMove = isMove });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_RaiseSequenceNumberFailed(e);
                throw;
            }
        }

        public ActionResult LowerSequenceNumber(Guid selectedLessonId, Guid courseId, bool isEdit, bool isMove)
        {
            try
            {
                _lessonManagerCUDRouter.LowerSequenceNumber(selectedLessonId);

                return RedirectToAction("Index", new { selectedLessonId = selectedLessonId, courseId = courseId, isEdit = isEdit, isMove = isMove });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_LowerSequenceNumberFailed(e);
                throw;
            }
        }

        public ActionResult RenumberAllLessonsInCourse(Guid courseId)
        {
            try
            {
                _lessonManagerCUDRouter.RenumberAllLessonsInCourse(courseId);

                return RedirectToAction("Index", new { courseId = courseId});
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_LowerSequenceNumberFailed(e);
                throw;
            }
        }

        public ActionResult SetLessonIsHidden(Guid lessonId, bool isHidden)
        {
            try
            {
                _lessonManagerCUDRouter.SetLessonIsHidden(lessonId: lessonId, isHidden: isHidden);

                return RedirectToAction("Index", new { selectedLessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_LowerSequenceNumberFailed(e);
                throw;
            }
        }

        public ActionResult SetCourseShowHiddenLessons(Guid lessonId, Guid courseId, bool doShowHiddenLessons)
        {
            try
            {
                _lessonManagerCUDRouter.SetShowHiddenLessons(courseId: courseId, doShowHidden: doShowHiddenLessons);

                return RedirectToAction("Index", new { selectedLessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_LowerSequenceNumberFailed(e);
                throw;
            }
        }

    }//End Class
}
