using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.DomainObjectBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class CoursePreferenceAdminViewObjBuilder
    {
        private CoursePreferenceDomainObjBuilder _coursePreferenceObjBuilder;

        public CoursePreferenceAdminViewObjBuilder(CoursePreferenceDomainObjBuilder coursePreferenceObjBuilder)
        {
            _coursePreferenceObjBuilder = coursePreferenceObjBuilder;
        }

        public CoursePreferenceAdminViewObj Retrieve (UserDomainObj currentUserObj, Guid courseId, string message)
        {
            CoursePreferenceDomainObjBasic basicObj = _coursePreferenceObjBuilder.BuildBasicFromCourseId(courseId);
            CoursePreferenceAdminViewObj viewObj = new CoursePreferenceAdminViewObj();

            viewObj.CurrentUserObj = currentUserObj;

            viewObj.CourseId = courseId;
            viewObj.CoursePreferenceId = basicObj.CoursePreferenceId;
            viewObj.Name = basicObj.Name;
            viewObj.DoShowNarrativeNotifications = basicObj.DoShowNarrativeNotifications;
            viewObj.DoShowLessonPlanNotifications = basicObj.DoShowLessonPlanNotifications;
            viewObj.DoShowDocumentNotifications = basicObj.DoShowDocumentNotifications;
            viewObj.Message = message;

            return viewObj;

        }
    }
}
