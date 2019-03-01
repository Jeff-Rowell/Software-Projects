using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3
{
    /// <summary>
    /// Populates Course Create View Model from Course Create View Object, which is populated from Course Create Domain Obj
    /// </summary>
    public class CourseCreateAdminController : BaseController
    {
        private CourseRepo _courseRepo;
        private WorkingGroupRepo _workingGroupRepo;
        private LessonRepo _lessonRepo;
        private CourseCreateAdminViewObjBuilder _courseCreateViewObjBuilder;
        private CourseCUDService _courseCUDService;
        private CourseCreateAdminCUDRouter _courseCreateAdminCUDRouter;

        public CourseCreateAdminController(UserDomainObjBuilder userObjBuilder, CourseRepo courseRepo,
                                WorkingGroupRepo workingGroupRepo, LessonRepo lessonRepo, 
                                CourseCreateAdminViewObjBuilder courseCreateViewObjBuilder, CourseCUDService courseCUDService,
                                CourseCreateAdminCUDRouter courseCreateAdminCUDRouter) : base(userObjBuilder)
        {
            _courseRepo = courseRepo;
            _workingGroupRepo = workingGroupRepo;
            _lessonRepo = lessonRepo;
            _courseCreateViewObjBuilder = courseCreateViewObjBuilder;
            _courseCUDService = courseCUDService;
            _courseCreateAdminCUDRouter = courseCreateAdminCUDRouter;
        }


        public ActionResult Index(Guid? courseId = null, Guid? workingGroupId = null, Guid? userId = null, Guid? termId = null, Guid? levelSetId = null,
                                string message = "", bool showDeleteCourse = false, bool showNewWorkingGroup = false, bool showDeleteWorkingGroup = false,
                                bool showAllCourses = false, bool isCourseDeletable = false, bool showRenameWorkingGroup = false, string narrativeMessage = "")
        {
            try
            {
                /*
                 * LevelSet functionality is depreicated and due to be removed. Current LevelSetSelectList must remain until dependencies are removed
                 */
                //List<LevelSet> levelSetList = _levelSetRepo.LevelSetsForWorkingGroup(workingGroupId).ToList();
                //var levelSetSelectList = new SelectList(levelSetList, "Id", "Name");

                if (workingGroupId == Guid.Empty)
                {
                    workingGroupId = CurrentWorkingGroupId;
                }

                var viewObj = _courseCreateViewObjBuilder.Retrieve(courseId: courseId ?? Guid.Empty,
                                                                        workingGroupId: workingGroupId ?? Guid.Empty, 
                                                                        userId: userId ?? Guid.Empty, 
                                                                        termId: termId ?? Guid.Empty, 
                                                                        message: message,
                                                                        showDeleteCourse: showDeleteCourse, 
                                                                        showNewWorkingGroup: showNewWorkingGroup, 
                                                                        showDeleteWorkingGroup: showDeleteWorkingGroup, 
                                                                        showAllCourses: showAllCourses, 
                                                                        isCourseDeletable: isCourseDeletable,
                                                                        showRenameWorkingGroup: showRenameWorkingGroup, 
                                                                        narrativeMessage: narrativeMessage,
                                                                        currentUserName: CurrentUserName);

                //CourseCreateAdminViewModel model = _factory.BuildCourseCreateViewModel(courseCreateViewObj, CurrentUser.IsAdmin, CurrentUser.IsNarrativeEditor, levelSetSelectList);

                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.CourseCreateIndexFailed(e);
                throw;
            }
        }

        public ActionResult CreateCourse(CourseCreateAdminReturnModel model)
        {
            try
            {
                var courseIdAndMessage = _courseCreateAdminCUDRouter.CreateCourseReturnIdAndMessage(model.CourseName, model.WorkingGroupId, model.UserId, model.TermId);

                Guid courseId = courseIdAndMessage.Item1;
                string message = courseIdAndMessage.Item2;

                return RedirectToAction("Index", routeValues: new { courseId = courseId, termId = model.TermId, message = message });
            }
            catch (Exception e)
            {
                WebLogger.Log.CreateCourseFailed(e);
                throw;
            }
        }

        public ActionResult RenameCourse(CourseCreateAdminReturnModel model)
        {
            try
            {
                if (model.CourseId != Guid.Empty)
                {
                    _courseCUDService.RenameCourse(model.CourseId, model.CourseName);
                }
                return RedirectToAction("Index", routeValues: new { courseId = model.CourseId, termId = model.TermId, workingGroupId = model.WorkingGroupId });
            }
            catch (Exception e)
            {
                WebLogger.Log.RenameCourseFailed(e);
                throw;
            }
        }

        public ActionResult CheckIfCourseDeletable(CourseCreateAdminReturnModel model)
        {
            try
            {
                string message = _courseCUDService.CheckIfCourseDeletable(model.CourseId);
                if (message.Length < 4)// Which means course is deletable.
                {
                    message = "Course is deletable.  Are you really sure you want to do this?  This cannot be undone.";
                    return RedirectToAction("Index", routeValues: new
                    {
                        termId = model.TermId,
                        workingGroupId = model.WorkingGroupId,
                        message = message,
                        isCourseDeletable = true,
                        showAllCourses = model.ShowAllCourses,
                        showDeleteCourse = model.ShowDeleteCourse,
                        courseId = model.CourseId
                    });
                }
                else
                {
                    message = "Course cannot be deleted: " + message;
                    return RedirectToAction("Index", routeValues: new
                    {
                        termId = model.TermId,
                        workingGroupId = model.WorkingGroupId,
                        message = message,
                        showAllCourses = model.ShowAllCourses,
                        showDeleteCourse = model.ShowDeleteCourse,
                        courseId = model.CourseId
                    });
                }
            }
            catch (Exception e)
            {
                WebLogger.Log.CheckIfCourseDeletableFailed(e);
                throw;
            }
        }

        public ActionResult DeleteCourse(CourseCreateAdminReturnModel model)
        {
            try
            {
                string message = "";
                if (model.CourseId != Guid.Empty)
                {
                    message = _courseCUDService.DeleteCourse(model.CourseId);
                }
                return RedirectToAction("Index", routeValues: new { termId = model.TermId, workingGroupId = model.WorkingGroupId, message = message });
            }
            catch (Exception e)
            {
                WebLogger.Log.DeleteCourseFailed(e);
                throw;
            }
        }

        //public ActionResult CreateWorkingGroup(CourseCreateAdminReturnModel model)
        //{
        //    try
        //    {
        //        _courseCreateAdminCUDRouter.CreateWorkingGroup(model.WorkingGroupName);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        WebLogger.Log.CreateWorkingGroupFailed(e);
        //        throw;
        //    }

        //}

        //public ActionResult RenameWorkingGroup(CourseCreateAdminReturnModel model)
        //{
        //    try
        //    {
        //        _courseCreateAdminCUDRouter.RenameWorkingGroup(model.WorkingGroupId, model.WorkingGroupName);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        WebLogger.Log.RenameWorkingGroupFailed(e);
        //        throw;
        //    }
        //}




    }// End Class
}