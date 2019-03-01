using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherDataModel.ObjectBuilders
{

    public class LessonUseDomainObjBuilder : ILessonUseDomainObjBuilder
    {

        private LessonUseRepo _lessonUseRepo;
        private LessonRepo _lessonRepo;


        /// <summary>
        /// Construct a LessonUseDomainObjBuilder with the given LessonUseRepo.
        /// </summary>
        /// <param name="repo">
        ///     LessonUseRepo used by the builder to retrieve LessonUse database objects.
        /// </param>
        public LessonUseDomainObjBuilder(LessonUseRepo repo, LessonRepo lessonRepo)
        {
            _lessonUseRepo = repo;
            _lessonRepo = lessonRepo;
        }


        /// <summary>
        /// Build a LessonUseDomainObj from the given LessonUse.
        /// </summary>
        /// <param name="dbObject">
        ///     LessonUse database object.
        /// </param>
        /// <returns>
        ///     LessonUseDomainObj
        /// </returns>
        public static LessonUseDomainObj Build(LessonUse dbObject)
        {
            if (dbObject == null)
            {
                throw new NullReferenceException("In LessonUseDomainObjBuilder.Build: Null reference");
            }

            LessonUseDomainObjBasic basicObj = BuildBasic(dbObject);
            LessonUseDomainObj domainObj = new LessonUseDomainObj(basicObj);

            domainObj.LessonUseId = dbObject.Id;
            domainObj.ClassMeetingId = dbObject.ClassMeetingId;
            domainObj.LessonId = dbObject.LessonId.HasValue ? dbObject.LessonId.Value : Guid.Empty;
            domainObj.SequenceNumber = dbObject.SequenceNumber;
            domainObj.CustomName = dbObject.CustomName;
            domainObj.CustomText = dbObject.CustomText;
            domainObj.CustomTime = dbObject.CustomTime;
            domainObj.HasCustomTime = dbObject.HasCustomTime;
            domainObj.LessonPlanText = dbObject.Lesson == null ? "" : dbObject.Lesson.LessonPlan.Text;

            return domainObj;
        }


        /// <summary>
        /// Build a LessonUseDomainObj from the LessonUse DbObject with the given Id.
        /// </summary>
        /// <param name="id">
        ///     LessonUse identifier.
        /// </param>
        /// <returns>
        ///     LessonUseDomainObj
        /// </returns>
        public LessonUseDomainObj BuildFromId(Guid id)
        {
            LessonUseDomainObj domainObj = Build(_lessonUseRepo.GetById(id));
            return domainObj;
        }


        /// <summary>
        /// Build a LessonUseDomainObjBasic from the given LessonUse database object.
        /// </summary>
        /// <param name="dbObject">
        ///     LessonUse database object.
        /// </param>
        /// <returns>
        ///     LessonUseDomainObjBasic
        /// </returns>
        public static LessonUseDomainObjBasic BuildBasic(LessonUse dbObject)
        {
            LessonUseDomainObjBasic basicObj = new LessonUseDomainObjBasic();
            basicObj.Id = dbObject.Id;
            basicObj.Name = (dbObject.Lesson == null) ? "" : dbObject.Lesson.Name;
            return basicObj;
        }


        /// <summary>
        /// Build a LessonUseDomainObjBasic from the LessonUse DbObject with the given Id.
        /// </summary>
        /// <param name="id">
        ///     LessonUse database object.
        /// </param>
        /// <returns>
        ///     LessonUseDomainObjBasic
        /// </returns>
        public LessonUseDomainObjBasic BuildBasicFromId(Guid id)
        {
            return BuildBasic(_lessonUseRepo.GetById(id));
        }

        public List<LessonUseDomainObj> LessonUseObjsForClassMeeting(Guid classMeetingId)
        {
            List<LessonUse> lessonUseList = _lessonUseRepo.LessonUsesForClassMeeting(classMeetingId).ToList();
            return lessonUseList.Select(lessonUse => Build(lessonUse)).ToList();
        }

        public string GetTextForLessonUse(Guid lessonUseId)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            if(lessonUse != null)
            {
                if(lessonUse.CustomText != null && lessonUse.CustomText.Length > 2)
                {
                    return lessonUse.CustomText;
                }
                else if (lessonUse.LessonId != null)
                {
                    return _lessonRepo.GetLessonPlanTextForLessonById(lessonUse.LessonId.Value);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
