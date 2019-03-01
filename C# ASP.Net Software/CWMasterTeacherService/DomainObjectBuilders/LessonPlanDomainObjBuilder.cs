using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    public class LessonPlanDomainObjBuilder : ILessonPlanDomainObjBuilder
    {

        private LessonPlanRepo _lessonPlanRepo;
        private LessonRepo _lessonRepo;

        public LessonPlanDomainObjBuilder(LessonPlanRepo lessonPlanRepo, LessonRepo lessonRepo)
        {
            _lessonPlanRepo = lessonPlanRepo;
            _lessonRepo = lessonRepo;

        }

        public LessonPlanDomainObjBuilder()
        { }

        public static LessonPlanDomainObj Build(LessonPlan dbObject)
        {
            LessonPlanDomainObjBasic basicObj = BuildBasic(dbObject);
            LessonPlanDomainObj lessonPlanObj = new LessonPlanDomainObj();

            lessonPlanObj.BasicLessonPlan = basicObj;
            lessonPlanObj.Text = dbObject.Text == null ? null : dbObject.Text;
            lessonPlanObj.DateModified = dbObject.DateModified == null ? new DateTime(1900, 01, 01) : dbObject.DateModified;
            lessonPlanObj.ModifiedByUserId = dbObject.ModifiedByUserId;
            lessonPlanObj.ModificationNotes = dbObject.ModificationNotes == null ? null : dbObject.ModificationNotes;
            lessonPlanObj.IsIndvNotMaster = !dbObject.IsMaster;
            lessonPlanObj.DateCreated = dbObject.DateCreated;


            return lessonPlanObj;
        }

        public static LessonPlanDomainObj BuildEmpty()
        {
            LessonPlanDomainObjBasic basicObj = BuildBasicEmpty();
            return new LessonPlanDomainObj(basicLessonPlan: basicObj,
                                            text: "_",
                                            dateModified: new DateTime(),
                                            modifiedByUserId: Guid.Empty,
                                            modificationNotes: "_",
                                            isIndivNotMaster: false);
        }

        public LessonPlanDomainObj BuildFromId(Guid lessonPlanId)
        {
            LessonPlan lessonPlan = _lessonPlanRepo.GetById(lessonPlanId);
            return Build(lessonPlan);
        }
        public LessonPlanDomainObj BuildFromLessonId(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            return Build(lesson.LessonPlan);
        }


        public static LessonPlanDomainObjBasic BuildBasic(LessonPlan lessonPlan)
        {
            LessonPlanDomainObjBasic _lessonPlanBasic = new LessonPlanDomainObjBasic(lessonPlan.Id, lessonPlan.Name);
            return _lessonPlanBasic;
        }

        public static LessonPlanDomainObjBasic BuildBasicEmpty()
        {
            LessonPlanDomainObjBasic _lessonPlanBasic = new LessonPlanDomainObjBasic(Guid.Empty, "_");
            return _lessonPlanBasic;
        }

        public LessonPlanDomainObjBasic BuildBasicFromId(Guid id)
        {
            LessonPlan lessonPlan = _lessonPlanRepo.GetById(id);
            return BuildBasic(lessonPlan);
        }

        public LessonPlanDomainObj GetLessonPlanObjForLesson(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lesson != null)
            {
                LessonPlan lessonPlan = _lessonRepo.GetLessonPlanForLessonById(lessonId);
                if (lessonPlan != null)
                {
                    return Build(lessonPlan);
                }
            }

            return new LessonPlanDomainObj();
        }

        public string GetLessonPlanTextForLesson(Guid lessonId)
        {
            return _lessonRepo.GetLessonPlanTextForLessonById(lessonId);
        }


    }//End Class
}