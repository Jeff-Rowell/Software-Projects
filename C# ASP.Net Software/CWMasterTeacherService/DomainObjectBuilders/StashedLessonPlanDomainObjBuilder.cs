using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherService.DomainObjectBuilders
{
    public class StashedLessonPlanDomainObjBuilder 
    {
        private StashedLessonPlanRepo _stashedLessonPlanRepo;
        private LessonRepo _lessonRepo;


        public StashedLessonPlanDomainObjBuilder(StashedLessonPlanRepo stashedLessonPlanRepo, LessonRepo lessonRepo)
        {
            _stashedLessonPlanRepo = stashedLessonPlanRepo;
            _lessonRepo = lessonRepo;
        }

        public static StashedLessonPlanDomainObj Build (StashedLessonPlan dbObject)
        {
            StashedLessonPlanDomainObj domainObj = new StashedLessonPlanDomainObj();
            domainObj.Id = dbObject.Id;
            domainObj.LessonId = dbObject.LessonId;
            domainObj.LessonPlanId = dbObject.LessonPlanId;
            domainObj.IsSaved = dbObject.IsSaved;
            domainObj.DateModified = dbObject.LessonPlan.DateModified;
            domainObj.UserDisplayName = dbObject.Lesson.Course.User.DisplayName;
            domainObj.LessonPlanName = dbObject.LessonPlan.Name;
            domainObj.IsLessonMaster = dbObject.Lesson.Course.IsMaster;
            return domainObj;
        }


        public StashedLessonPlanDomainObj BuildFromId(Guid stashedLessonPlanId)
        {
            StashedLessonPlan stashedPlan = _stashedLessonPlanRepo.GetById(stashedLessonPlanId);
            return Build(stashedPlan);
        }

        public static List <StashedLessonPlanDomainObj> StashedLessonPlanObjsForLesson(Lesson lesson)
        {
            return lesson.StashedLessonPlans.Select(x => Build(x)).ToList();
        }
    }
}
