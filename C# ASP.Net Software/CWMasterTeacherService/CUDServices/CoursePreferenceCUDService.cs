using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class CoursePreferenceCUDService
    {
        private ICoursePreferenceRepo _coursePreferenceRepo;

        public CoursePreferenceCUDService(ICoursePreferenceRepo coursePreferenceRepo)
        {
            _coursePreferenceRepo = coursePreferenceRepo;
        }


        public CoursePreference CopyCoursePreference(CoursePreference fromCoursePreference)
        {
            CoursePreference newPreference  = new CoursePreference();
            newPreference.Name = fromCoursePreference.Name;
            newPreference = SetCoursePreferenceValues(coursePreference: newPreference,
                                                        doShowNarrativeNotifications: fromCoursePreference.DoShowNarrativeNotifications,
                                                        doShowLessonPlanNotifications: fromCoursePreference.DoShowLessonPlanNotifications,
                                                        doShowDocumentNotifications: fromCoursePreference.DoShowDocumentNotifications);
            _coursePreferenceRepo.Insert(newPreference);
            return newPreference;
        }

        public CoursePreference CreateCoursePreference(string name)
        {
            CoursePreference coursePreference = new CoursePreference();
            coursePreference.Name = name;
            _coursePreferenceRepo.Insert(coursePreference);

            return coursePreference;
        }

        private CoursePreference SetCoursePreferenceValues(CoursePreference coursePreference, 
                                                    bool doShowNarrativeNotifications, 
                                                    bool doShowLessonPlanNotifications,
                                                    bool doShowDocumentNotifications)
        {
            coursePreference.DoShowNarrativeNotifications = doShowNarrativeNotifications;
            coursePreference.DoShowLessonPlanNotifications = doShowLessonPlanNotifications;
            coursePreference.DoShowDocumentNotifications = doShowDocumentNotifications;

            return coursePreference;
        }

        public void UpdateCoursePreference(Guid coursePreferenceId, 
                                            bool doShowNarrativeNotifications, 
                                            bool doShowLessonPlanNotifications, 
                                            bool doShowDocumentNotifications)
        {
            CoursePreference coursePreference = _coursePreferenceRepo.GetById(coursePreferenceId);
            coursePreference = SetCoursePreferenceValues(coursePreference: coursePreference,
                                                            doShowNarrativeNotifications: doShowNarrativeNotifications,
                                                            doShowLessonPlanNotifications: doShowLessonPlanNotifications,
                                                            doShowDocumentNotifications: doShowDocumentNotifications);

            _coursePreferenceRepo.Update(coursePreference);
        }
    }
}
