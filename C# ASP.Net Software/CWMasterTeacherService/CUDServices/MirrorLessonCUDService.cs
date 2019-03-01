using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class MirrorLessonCUDService
    {
        private LessonRepo _lessonRepo;
        private CourseRepo _courseRepo;

        public MirrorLessonCUDService(LessonRepo lessonRepo, CourseRepo courseRepo)
        {
            _lessonRepo = lessonRepo;
            _courseRepo = courseRepo;
        }



    }
}
