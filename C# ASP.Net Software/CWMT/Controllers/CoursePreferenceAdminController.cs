using CWMasterTeacher3;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherService.DomainObjectBuilders;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT
{
    public class CoursePreferenceAdminController : BaseController
    {
        private CoursePreferenceAdminViewObjBuilder _coursePreferenceViewObjBuilder;
        private CoursePreferenceCUDService _coursePreferenceCUDRouter;

        public CoursePreferenceAdminController(UserDomainObjBuilder userObjBuilder, CoursePreferenceAdminViewObjBuilder coursePreferenceViewObjBuilder, 
                                                    CoursePreferenceCUDService coursePreferenceCUDService) : base(userObjBuilder )
        {
            _coursePreferenceViewObjBuilder = coursePreferenceViewObjBuilder;
            _coursePreferenceCUDRouter = coursePreferenceCUDService;
        }

        public ActionResult Index(Guid courseId, string message = "")
        {
            CoursePreferenceAdminViewObj viewObj = _coursePreferenceViewObjBuilder.Retrieve(currentUserObj: CurrentUserObj, 
                                                                                            courseId: courseId,
                                                                                            message: message);
            return View(viewObj);
        }

        public ActionResult UpdateCoursePreferences(CoursePreferenceAdminViewObj model)
        {
            try
            {
                _coursePreferenceCUDRouter.UpdateCoursePreference(coursePreferenceId: model.CoursePreferenceId,
                                                                        doShowNarrativeNotifications: model.DoShowNarrativeNotifications,
                                                                        doShowLessonPlanNotifications: model.DoShowLessonPlanNotifications,
                                                                        doShowDocumentNotifications: model.DoShowDocumentNotifications);
                string message = "Course preferences have been updated.";
                return RedirectToAction("Index", "CoursePreferenceAdmin", new { courseId = model.CourseId, message = message });
            }
            catch (Exception e)
            {
                WebLogger.Log.AddDocumentToLessonFailed(e);
                throw;
            }
        }
    }
}