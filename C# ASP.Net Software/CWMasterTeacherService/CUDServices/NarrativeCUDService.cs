using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherService.CUDServices
{
    public class NarrativeCUDService
    {
        private INarrativeRepo _narrativeRepo;
        private ILessonRepo _lessonRepo;
        private IStashedNarrativeRepo _stashedNarrativeRepo;
        private ICourseRepo _courseRepo;
        private StashedNarrativeCUDService _stashedNarrativeCUDService;
        private SharedCUDService _sharedCUDService;

        public NarrativeCUDService(INarrativeRepo narrativeRepo, ILessonRepo lessonRepo, IStashedNarrativeRepo stashedNarrativeRepo,
                                    ICourseRepo courseRepo, StashedNarrativeCUDService stashedNarrativeCUDService, SharedCUDService sharedCUDService)
        {
            _narrativeRepo = narrativeRepo;
            _lessonRepo = lessonRepo;
            _stashedNarrativeRepo = stashedNarrativeRepo;
            _courseRepo = courseRepo;
            _stashedNarrativeCUDService = stashedNarrativeCUDService;
            _sharedCUDService = sharedCUDService;
        }

        private void Update(Narrative narrative)
        {
            
            if (narrative.Text != null)
            {
                if (narrative.Text.Length < 1)
                {
                    narrative.Text = "_";
                }
            }
            else
            {
                narrative.Text = "_";
            }

            _narrativeRepo.Update(narrative);
        }


        public Narrative CreateNarrative(Guid userId)
        {
            Narrative narrative = new Narrative();
            narrative.ModifiedByUserId = userId;
            narrative.Text = "_";
            narrative.DateModified = new DateTime();

            _narrativeRepo.Insert(narrative);

            return narrative;
        }

        public Narrative CreateNarrativeFromLessonId(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            return lesson == null ? null : CreateNarrative(lesson.Course.UserId);
        }

        public Narrative CreateNarrativeFromCourseId(Guid courseId)
        {
            Course course = _courseRepo.GetById(courseId);
            return course == null ? null : CreateNarrative(course.UserId);
        }

        public Narrative CreateCommentNarrative(Guid courseId, Guid masterNarrativeId)
        {
            Narrative narrative = CreateNarrativeFromCourseId(courseId);
            if(narrative != null)
            {
                narrative.MasterNarrativeId = masterNarrativeId;
                return narrative;
            }
            else
            {
                return null;
            }
        }

        public Narrative CopyNarrative(Narrative fromNarrative)
        {
            Narrative newNarrative = new Narrative();
            newNarrative.ModifiedByUserId = fromNarrative.ModifiedByUserId;
            newNarrative.Text = fromNarrative.Text;
            newNarrative.DateModified = fromNarrative.DateModified;

            _narrativeRepo.Insert(newNarrative);

            return newNarrative;
        }

        public void UpdateNarrative(Guid narrativeId, Guid lessonId, string text)
        {
            Narrative narrative = _narrativeRepo.GetById(narrativeId);

            _stashedNarrativeCUDService.CreateStashedNarrativeFromLessonId(lessonId, narrativeId, false);

            DateTime time = DateTime.Now;
            narrative.DateModified = time;

            narrative.Text = text;
            Update(narrative);

            SetNarrativeDateChoiceConfirmedInLesson(lessonId);

            TrimNumberOfStashedNarratives(narrativeId, lessonId);
        }

        public void TrimNumberOfStashedNarratives(Guid narrativeId, Guid lessonId)
        {
            List<StashedNarrative> stashList = _stashedNarrativeRepo.StashedNarrativesForLesson(lessonId)
                                                .OrderByDescending(x => x.CreationDate).ToList();
            stashList = stashList.Where(x => !x.IsSaved).ToList();
            int max = DomainUtilities.NarrativeStashMaxSize;
            if (stashList.Count > max)
            {
                List<Guid> idList = stashList.Select(stash => stash.Id).ToList();
                for(int index = max; index <= stashList.Count -1; index++)
                {
                    _stashedNarrativeRepo.Delete(idList.ElementAt(index));
                }
            }
            _narrativeRepo.CleanUpOrphanNarratives();
        }

        public void RemoveStashedNarrativeFromArchive(Guid lessonId, Guid narrativeId)
        {
            StashedNarrative stashedNarrative = _stashedNarrativeRepo.StashedNarrativeForLessonAndNarrative(lessonId, narrativeId);
            if (stashedNarrative != null)
            {
                stashedNarrative.IsSaved = false; ;
                _stashedNarrativeRepo.Update(stashedNarrative);
                TrimNumberOfStashedNarratives(stashedNarrative.NarrativeId, stashedNarrative.LessonId);
            }
        }

        public void ChangeNarrativeForLesson(Guid lessonId, Guid newNarrativeId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            //First stash a copy of the old one 
            _stashedNarrativeCUDService.CreateStashedNarrative(lesson, lesson.NarrativeId, false);
            lesson.NarrativeId = newNarrativeId;
            _lessonRepo.Update(lesson);
        }

        public void SetNarrativeDateChoiceConfirmedInLesson(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            lesson.NarrativeDateChoiceConfirmed = DateTime.Now;
            _lessonRepo.Update(lesson);
        }

    }
}
