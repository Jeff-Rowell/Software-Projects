using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class LessonNarrativeCUDService
    {
        private LessonNarrativeRepo _lessonNarrativeRepo;

        public LessonNarrativeCUDService(LessonNarrativeRepo lessonNarrativeRepo)
        {
            _lessonNarrativeRepo = lessonNarrativeRepo;
        }

        public LessonNarrative CreateLessonNarrative(Guid lessonId, Guid narrativeId)
        {
            LessonNarrative lessonNarrative = new LessonNarrative();
            lessonNarrative.LessonId = lessonId;
            lessonNarrative.NarrativeId = narrativeId;
            _lessonNarrativeRepo.Insert(lessonNarrative);

            return lessonNarrative;
        }


    }//End Class
}
