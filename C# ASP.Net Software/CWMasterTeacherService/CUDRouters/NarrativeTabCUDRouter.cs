using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class NarrativeTabCUDRouter
    {
        private NarrativeCUDService _narrativeCUDService;
        private LessonCUDService _lessonCUDService;
        private StashedNarrativeCUDService _stashedNarrativeCUDService;

        public NarrativeTabCUDRouter (NarrativeCUDService narrativeCUDService, LessonCUDService lessonCUDService,
                                    StashedNarrativeCUDService stashedNarrativeCUDService)
        {
            _narrativeCUDService = narrativeCUDService;
            _lessonCUDService = lessonCUDService;
            _stashedNarrativeCUDService = stashedNarrativeCUDService;
        }

        public void UpdateNarrative(Guid narrativeId, string text, Guid lessonId)
        {
            _narrativeCUDService.UpdateNarrative(narrativeId: narrativeId, lessonId: lessonId, text: text);
        }

        public void CreateSavedStashedNarrative(Guid narrativeId, Guid lessonId)
        {
            _stashedNarrativeCUDService.CreateStashedNarrativeFromLessonId(lessonId: lessonId, narrativeId: narrativeId, isSaved: true);
        }

        public void UpdateNarrativeDateChoiceConfirmedToGroupDate(Guid lessonId)
        {
            _lessonCUDService.UpdateNarrativeDateChoiceConfirmedToGroupDate(lessonId);
        }


    }
}
