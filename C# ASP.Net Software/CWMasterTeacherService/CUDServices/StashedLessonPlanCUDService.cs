using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class StashedLessonPlanCUDService
    {

        private IStashedLessonPlanRepo _stashedLessonPlanRepo;
        private ILessonRepo _lessonRepo;
        private ILessonPlanRepo _lessonPlanRepo;

        public StashedLessonPlanCUDService(IStashedLessonPlanRepo lessonPlanLessonRepo, ILessonRepo lessonRepo, ILessonPlanRepo lessonPlanRepo)
        {
            _stashedLessonPlanRepo = lessonPlanLessonRepo;
            _lessonRepo = lessonRepo;
            _lessonPlanRepo = lessonPlanRepo;
        }

        public void RemoveSavedStashedLessonPlan(Guid lessonPlanId, Lesson lesson)
        {
            List<Guid> stashIdList = lesson.StashedLessonPlans.Where(x => x.LessonPlanId == lessonPlanId && x.IsSaved).Select(x =>x.Id).ToList();
            if(stashIdList != null && stashIdList.Count > 0)
            {
                foreach (var Id in stashIdList)
                {
                    _stashedLessonPlanRepo.Delete(Id);
                }
            }
        }

            //Trims the number of unsaved stashed lesson plans to 2, then adds the new one
            public StashedLessonPlan CreateStashedLessonPlan(Lesson lesson, Guid lessonPlanId, bool isSaved)
        {
            List<StashedLessonPlan> stashList = lesson.StashedLessonPlans.OrderByDescending(x => x.CreationDate).ToList();
            var matchQuery = from x in stashList
                             where x.LessonId == lesson.Id && x.LessonPlanId == lessonPlanId
                             select x;
            LessonPlan junkLessonPlan = _lessonPlanRepo.GetById(lessonPlanId);
            if (matchQuery.Count() == 0)//We haven't already saved it
            {
                StashedLessonPlan stashedLessonPlan = new StashedLessonPlan();
                stashedLessonPlan.LessonId = lesson.Id;

                LessonPlan junkLessonPlan2 = _lessonPlanRepo.GetById(lessonPlanId);
                stashedLessonPlan.LessonPlanId = lessonPlanId;
                stashedLessonPlan.IsSaved = isSaved; ;
                stashedLessonPlan.CreationDate = DateTime.Now;
                stashedLessonPlan.InactivationDate = null;

                _stashedLessonPlanRepo.Insert(stashedLessonPlan);

                TrimNumberOfStashedLessonPlans(lessonPlanId, lesson.Id);

                return stashedLessonPlan;


            }
            else
            {
                return _stashedLessonPlanRepo.GetById(matchQuery.FirstOrDefault().Id);
            }

        }

        public void CreateStashedLessonPlanFromLessonId(Guid lessonId, Guid lessonPlanId, bool isSaved)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            CreateStashedLessonPlan(lesson, lessonPlanId, isSaved);
        }


        /// <summary>
        /// Creates a new StashedLessonPlan with the same LessonPlan as input, but a different Lesson
        /// </summary>
        /// <param name="stashedLessonPlan">LessonPlan will be read from this</param>
        /// <param name="lessonId">Id of target for new StashedLessonPlan</param>
        public StashedLessonPlan CopyToNewLesson(StashedLessonPlan stashedLessonPlan, Lesson lesson, bool isSaved)
        {
            return CreateStashedLessonPlan(lesson: lesson, 
                                           lessonPlanId: stashedLessonPlan.LessonPlanId, 
                                           isSaved: isSaved);
        }

        public void RemoveStashedLessonPlanFromArchive(Guid stashedLessonPlanId)
        {
            StashedLessonPlan stashedLessonPlan = _stashedLessonPlanRepo.GetById(stashedLessonPlanId);
            if (stashedLessonPlan != null)
            {
                stashedLessonPlan.IsSaved = false; ;
                _stashedLessonPlanRepo.Update(stashedLessonPlan);
                TrimNumberOfStashedLessonPlans(stashedLessonPlan.LessonPlanId, stashedLessonPlan.LessonId);
            }
        }

        public void TrimNumberOfStashedLessonPlans(Guid lessonPlanId, Guid lessonId)
        {
            List<StashedLessonPlan> stashList = _stashedLessonPlanRepo.StashedLessonPlansForLesson(lessonId)
                                       .OrderByDescending(x => x.CreationDate).ToList();
            stashList = stashList.Where(x => !x.IsSaved).ToList();
            int max = DomainUtilities.LessonPlanStashMaxSize;
            List<Guid> idList = stashList.Where(stash => !stash.IsSaved).Select(stash => stash.Id).ToList();

            if (stashList.Count > max)
            {
                for (int index = max; index <= stashList.Count - 1; index++)
                {
                    _stashedLessonPlanRepo.Delete(idList.ElementAt(index));
                }
            }
            _lessonPlanRepo.CleanUpOrphanLessonPlans();
        }








    }
}
