using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class LessonPlanCUDService
    {
        private StashedLessonPlanCUDService _stashedLessonPlanCUDService;
        ILessonPlanRepo _lessonPlanRepo;
        ILessonRepo _lessonRepo;




        public LessonPlanCUDService(ILessonPlanRepo lessonPlanRepo, StashedLessonPlanCUDService lessonPlanLessonCUDService,
                                    ILessonRepo lessonRepo)
        {
            _stashedLessonPlanCUDService = lessonPlanLessonCUDService;
            _lessonPlanRepo = lessonPlanRepo;
            _lessonRepo = lessonRepo;
        }

        public LessonPlan CreateNewLessonPlan(string name, Guid userId, bool isMaster)
        {
            LessonPlan newPlan = new LessonPlan();
            newPlan.DateModified = DateTime.Now;
            newPlan.Name = name;
            newPlan.Text = "_";
            newPlan.ModifiedByUserId = userId;
            newPlan.ModificationNotes = "_";
            newPlan.IsPreNote = false;
            newPlan.IsPostNote = false;
            newPlan.IsMaster = isMaster;
            newPlan.DateCreated = DateTime.Now;

            _lessonPlanRepo.Insert(newPlan);

            return (newPlan);
        }

        /// <summary>
        /// Could be add or update. For Add, lessonPlanId  will be Guid.Empty.
        /// For update, first we look to see if other Lessons are looking at this lesson plan.  If yes, it creates a new LessonPlan
        /// If no, we use the old one and update the values.
        /// ***Anything Calling this should call LessonRepo UpdateDateModified
        /// </summary>

        public LessonPlan AddOrUpdateLessonPlan(Guid lessonPlanId, Guid lessonId, Guid userId, string text, string name, 
                                                bool isPreNote, bool isPostNote)
        {

            LessonPlan lessonPlan = new LessonPlan();
            Lesson lesson = _lessonRepo.GetById(lessonId);

            bool createNew = true;
            if (lessonPlanId != Guid.Empty)//Which means this is an update
            {
                LessonPlan oldLessonPlan = _lessonPlanRepo.GetById(lessonPlanId);
                name = name.Length < 2 ? oldLessonPlan.Name : name;
                bool isMasterEdit = lesson.Course.IsMaster;

                if (isMasterEdit)//So we're going to edit the oldLessonPlan but stash a copy
                {
                    createNew = false;
                    LessonPlan copyOfLessonPlan = CopyLessonPlan(oldLessonPlan);
                    _stashedLessonPlanCUDService.CreateStashedLessonPlan(lesson, copyOfLessonPlan.Id, false);
                    lessonPlan = oldLessonPlan;
                }
                else //So we're going to stash the oldLessonPlan and create a new one for the edits.
                {
                    createNew = true;
                    _stashedLessonPlanCUDService.CreateStashedLessonPlan(lesson, lessonPlanId, false);
                }

            }

            if(createNew)
            {
                _lessonRepo.SetLessonPlanDateChoiceConfirmed(lesson, DateTime.Now);
            }

            DateTime time = DateTime.Now;
            lessonPlan.DateModified = time;
            lessonPlan.DateCreated = time;
            lessonPlan.Text = text;
            lessonPlan.Name = name;
            lessonPlan.ModifiedByUserId = userId;
            lessonPlan.IsPreNote = isPreNote;
            lessonPlan.IsPostNote = isPostNote;

            if (createNew)
            {
                _lessonPlanRepo.Insert(lessonPlan);
                _lessonRepo.SetLessonPlanForLesson(lessonId, lessonPlan.Id);
            }
            else
            {
                _lessonPlanRepo.Update(lessonPlan);
            }

            
            _lessonPlanRepo.CleanUpOrphanLessonPlans();

            return lessonPlan;
        }



        public LessonPlan CopyLessonPlan(LessonPlan fromLessonPlan)
        {
            LessonPlan newLessonPlan = new LessonPlan();

            newLessonPlan.Name = fromLessonPlan.Name;
            newLessonPlan.Text = fromLessonPlan.Text;
            newLessonPlan.DateModified = fromLessonPlan.DateModified;
            newLessonPlan.DateCreated = DateTime.Now;
            newLessonPlan.ModificationNotes = fromLessonPlan.ModificationNotes;
            newLessonPlan.IsMaster = fromLessonPlan.IsMaster;
            newLessonPlan.IsPreNote = fromLessonPlan.IsPreNote;
            newLessonPlan.IsPostNote = fromLessonPlan.IsPostNote;
            newLessonPlan.DoSuggestUsingMaster = false;

            _lessonPlanRepo.Insert(newLessonPlan);

            LessonPlan junkTestLessonPlan = _lessonPlanRepo.GetById(newLessonPlan.Id);

            return newLessonPlan;
        }

        public void ChangeLessonPlanForLesson(Guid lessonId, Guid newLessonPlanId, bool doCheckStashForDuplicate)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            //And remove saved copy of new lesson if it exists.  (This happens when we import from a saved LP)
            if (doCheckStashForDuplicate)
            {
                _stashedLessonPlanCUDService.RemoveSavedStashedLessonPlan(newLessonPlanId, lesson);
            }

            //Then stash a copy of the old one as history 
            _stashedLessonPlanCUDService.CreateStashedLessonPlan(lesson, lesson.LessonPlanId, false);
            lesson.LessonPlanId = newLessonPlanId;
            _lessonRepo.Update(lesson);

        }


    }
}
