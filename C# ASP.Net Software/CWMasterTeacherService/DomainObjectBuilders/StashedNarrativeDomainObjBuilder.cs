using CWMasterTeacherDataModel;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.DomainObjectBuilders
{
    public class StashedNarrativeDomainObjBuilder
    {

        private StashedNarrativeRepo _stashedNarrativeRepo;
        private LessonRepo _lessonRepo;


        public StashedNarrativeDomainObjBuilder(StashedNarrativeRepo stashedNarrativeRepo, LessonRepo lessonRepo)
        {
            _stashedNarrativeRepo = stashedNarrativeRepo;
            _lessonRepo = lessonRepo;
        }

        public static StashedNarrativeDomainObj Build(StashedNarrative dbObject)
        {
            StashedNarrativeDomainObj domainObj = new StashedNarrativeDomainObj();
            domainObj.Id = dbObject.Id;
            domainObj.LessonId = dbObject.LessonId;
            domainObj.NarrativeId = dbObject.NarrativeId;
            domainObj.IsSaved = dbObject.IsSaved;
            domainObj.DateModified = dbObject.Narrative.DateModified;
            domainObj.UserDisplayName = dbObject.Lesson.Course.User.DisplayName;
            domainObj.NarrativeName = dbObject.Lesson.Name;
            domainObj.IsLessonMaster = dbObject.Lesson.Course.IsMaster;
            return domainObj;
        }


        public StashedNarrativeDomainObj BuildFromId(Guid stashedNarrativeId)
        {
            StashedNarrative stashedNarrative = _stashedNarrativeRepo.GetById(stashedNarrativeId);
            return Build(stashedNarrative);
        }

        public static List<StashedNarrativeDomainObj> StashedNarrativeObjsForLesson(Lesson lesson)
        {
            return lesson.StashedNarratives.Select(x => Build(x)).ToList();
        }
    }
}
