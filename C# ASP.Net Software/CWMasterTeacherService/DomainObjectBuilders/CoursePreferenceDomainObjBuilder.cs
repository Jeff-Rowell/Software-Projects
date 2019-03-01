using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.DomainObjectBuilders
{
    public class CoursePreferenceDomainObjBuilder
    {
        private CoursePreferenceRepo _coursePreferenceRepo;
        private CourseRepo _courseRepo;

        public CoursePreferenceDomainObjBuilder(CoursePreferenceRepo coursePreferenceRepo, CourseRepo courseRepo)
        {
            _coursePreferenceRepo = coursePreferenceRepo;
            _courseRepo = courseRepo;
        }

        public static CoursePreferenceDomainObjBasic BuildBasic(CoursePreference domainObj)
        {
            CoursePreferenceDomainObjBasic basicObj = new CoursePreferenceDomainObjBasic();

            basicObj.CoursePreferenceId = domainObj.Id;
            basicObj.Name = domainObj.Name;
            basicObj.DoShowNarrativeNotifications = domainObj.DoShowNarrativeNotifications;
            basicObj.DoShowLessonPlanNotifications = domainObj.DoShowLessonPlanNotifications;
            basicObj.DoShowDocumentNotifications = domainObj.DoShowDocumentNotifications;

            return basicObj;
        }

        public CoursePreferenceDomainObjBasic BuildBasicFromId(Guid coursePreferenceId)
        {
            CoursePreference coursePreference = _coursePreferenceRepo.GetById(coursePreferenceId);

            return coursePreference == null ? null : BuildBasic(coursePreference);
        }

        public CoursePreferenceDomainObjBasic BuildBasicFromCourseId(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);

            return course == null ? null : BuildBasic(course.CoursePreference);
        }
    }
}
