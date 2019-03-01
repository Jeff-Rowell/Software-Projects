using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class ClassSectionCUDService
    {
        private CourseRepo _courseRepo;
        private TermRepo _termRepo;
        private CourseCUDService _courseCUDService;
        ClassSectionRepo _classSectionRepo;


        public ClassSectionCUDService(ClassSectionRepo classSectionRepo, CourseRepo courseRepo, TermRepo termRepo,
                                        CourseCUDService courseCUDService)

        {
            _courseRepo = courseRepo;
            _termRepo = termRepo;
            _classSectionRepo = classSectionRepo;
            _courseCUDService = courseCUDService;
        }

        /// <summary>
        /// This is used for navigating between tabs. Probaly dead after Jan 2017 redesign
        /// </summary>
        public void SetLastDisplayedClassMeetingId(ClassMeeting lastDisplayedClassMeeting)
        {
            ClassSection classSection = lastDisplayedClassMeeting.ClassSection;
            classSection.LastDisplayedClassMeetingId = lastDisplayedClassMeeting.Id;
            _classSectionRepo.Update(classSection);

            _courseCUDService.SetLastDisplayedClassSectionId(classSection);
        }

        /// <summary>
        /// This is used for navigating between tabs.  This is probably the version to keep after the Jan 2017 redesign
        /// </summary>
        public void SetLastDisplayedClassMeetingId(Guid classMeetingId, Guid classSectionId)
        {
            ClassSection classSection = _classSectionRepo.GetById(classSectionId);

            classSection.LastDisplayedClassMeetingId = classMeetingId;
            _classSectionRepo.Update(classSection);

            _courseCUDService.SetLastDisplayedClassSectionId(classSection);
        }

        public bool DeleteClassSection(Guid classSectionId)
        {
            ClassSection classSection = _classSectionRepo.GetById(classSectionId);
            if (classSection.ClassMeetings.Count == 0)
            {
                _classSectionRepo.Delete(classSectionId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RenameClassSection(Guid classSectionId, string newName)
        {
            ClassSection classSection = _classSectionRepo.GetById(classSectionId);
            if (classSection != null)
            {
                classSection.Name = newName;
                _classSectionRepo.Update(classSection);
            }
        }

        public ClassSection CreateClassSection(Guid courseId, string name)
        {
            ClassSection classSection = new ClassSection();
            classSection = SetClassSectionValues(classSection, courseId, name);
            _classSectionRepo.Insert(classSection);

            return classSection;
        }

        private ClassSection SetClassSectionValues(ClassSection classSection, Guid courseId, string name)
        {
            classSection.CourseId = courseId;
            classSection.Name = name;
            classSection.StudentAccessExpirationDate = DateTime.Now.AddDays(30);
            return classSection;
        }

        public void CreateMirrorClassSection(Guid mirrorCourseId, Guid targetClassSectionId)
        {
            Course mirrorCourse = _courseRepo.GetById(mirrorCourseId);
            ClassSection targetClassSection = _classSectionRepo.GetById(targetClassSectionId);
            string name = targetClassSection.Name + "_" + mirrorCourse.User.DisplayName + " (mirror)";
            ClassSection mirrorClassSection = new ClassSection();
            mirrorClassSection = SetClassSectionValues(mirrorClassSection, mirrorCourseId, name);
            mirrorClassSection.MirrorTargetClassSectionId = targetClassSectionId;
            _classSectionRepo.Insert(mirrorClassSection);
        }





    }//End Class
}
