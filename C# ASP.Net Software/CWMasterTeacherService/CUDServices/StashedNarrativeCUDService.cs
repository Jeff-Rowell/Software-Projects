using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class StashedNarrativeCUDService
    {

        private IStashedNarrativeRepo _stashedNarrativeRepo;
        private ILessonRepo _lessonRepo;

        public StashedNarrativeCUDService(IStashedNarrativeRepo narrativeLessonRepo, ILessonRepo lessonRepo)
        {
            _stashedNarrativeRepo = narrativeLessonRepo;
            _lessonRepo = lessonRepo;
        }

        //Trims the number of unsaved stashed lesson plans to 2, then adds the new one
        public StashedNarrative CreateStashedNarrative(Lesson lesson, Guid narrativeId, bool isSaved)
        {
            List<StashedNarrative> stashList = lesson.StashedNarratives.OrderByDescending(x => x.CreationDate).ToList();
            var matchQuery = from x in stashList
                             where x.LessonId == lesson.Id && x.NarrativeId == narrativeId
                             select x;

            if (matchQuery.Count() == 0)//We haven't already saved it
            {
                List<Guid> idList = stashList.Where(stash => !stash.IsSaved).Select(stash => stash.Id).ToList();

                if (!isSaved && idList.Count > 2)//Then trim size of list so we only end up with 3
                {
                    for (int index = 2; index <= stashList.Count - 1; index++)
                    {
                        _stashedNarrativeRepo.Delete(idList.ElementAt(index));
                    }
                }

                StashedNarrative stashedNarrative = new StashedNarrative();
                stashedNarrative.LessonId = lesson.Id;
                stashedNarrative.NarrativeId = narrativeId;
                stashedNarrative.IsSaved = isSaved; ;
                stashedNarrative.CreationDate = DateTime.Now;
                stashedNarrative.InactivationDate = null;

                _stashedNarrativeRepo.Insert(stashedNarrative);

                return stashedNarrative;
            }
            else
            {
                return _stashedNarrativeRepo.GetById(matchQuery.FirstOrDefault().Id);
            }

        }

        public void CreateStashedNarrativeFromLessonId(Guid lessonId, Guid narrativeId, bool isSaved)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if(lesson != null)
            {
                CreateStashedNarrative(lesson, narrativeId, isSaved);
            }
        }


        /// <summary>
        /// Creates a new StashedNarrative with the same Narrative as input, but a different Lesson
        /// </summary>
        /// <param name="stashedNarrative">Narrative will be read from this</param>
        /// <param name="lessonId">Id of target for new StashedNarrative</param>
        public StashedNarrative CopyToNewLesson(StashedNarrative stashedNarrative, Lesson lesson, bool isSaved)
        {
            return CreateStashedNarrative(lesson: lesson,
                                           narrativeId: stashedNarrative.NarrativeId,
                                           isSaved: isSaved);
        }
    }
}
