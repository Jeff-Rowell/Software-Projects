using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;
using static CWMasterTeacherDomain.DomainUtilities;

namespace CWMasterTeacherService.DomainObjectBuilders
{
    public class LessonForComparisonDomainObjBuilder : CWMasterTeacherDataModel.Interfaces.ILessonForComparisonDomainObjBuilder
    {

        private readonly LessonRepo _lessonRepo;
        private readonly DocumentUseRepo _documentUseRepo;
        private readonly StashedLessonPlanRepo _stashedLessonPlanRepo;
        private readonly LessonPlanRepo _lessonPlanRepo;
        private readonly DocumentUseDomainObjBuilder _documentUseObjBuilder;

        public LessonForComparisonDomainObjBuilder(LessonRepo lessonRepo, DocumentUseRepo documentUseRepo, 
                                                        StashedLessonPlanRepo stashedLessonPlanRepo,
                                                        LessonPlanRepo lessonPlanRepo,
                                                        DocumentUseDomainObjBuilder documentUseObjBuilder)
        {
            _lessonRepo = lessonRepo;
            _documentUseRepo = documentUseRepo;
            _stashedLessonPlanRepo = stashedLessonPlanRepo;
            _lessonPlanRepo = lessonPlanRepo;
            _documentUseObjBuilder = documentUseObjBuilder;
        }


        public static LessonForComparisonDomainObj Build(Lesson lesson)
        {
            var basic = LessonDomainObjBuilder.BuildBasic(lesson);
            List<DocumentUseDomainObj> allDocUseObjs = lesson.DocumentUses.Where(x => x.IsActive).Select(x => DocumentUseDomainObjBuilder.Build(x)).ToList();

            return new LessonForComparisonDomainObj(basic)
            {
                ChangeNotes = lesson.ChangeNotes,

                IsActive = lesson.IsActive,
                LessonPlanObj = LessonPlanDomainObjBuilder.Build(lesson.LessonPlan),
                NarrativeObj = NarrativeDomainObjBuilder.Build(lesson.Narrative),
                MasterLessonId = lesson.MasterLessonId == null ? Guid.Empty : lesson.MasterLessonId.Value,
                DocumentsModifiedDate = lesson.DateDocumentsModified.GetValueOrDefault(),
                UserName = lesson.Course.User.DisplayName,
                StashedLessonPlans = StashedLessonPlanDomainObjBuilder.StashedLessonPlanObjsForLesson(lesson),
                StashedNarratives = StashedNarrativeDomainObjBuilder.StashedNarrativeObjsForLesson(lesson),
                ReferenceDateDocChoiceConfirmed = lesson.ReferenceDateTimeDocChoiceConfirmed == null ? new DateTime() : lesson.ReferenceDateTimeDocChoiceConfirmed.Value,
                TeachingDocumentUses = allDocUseObjs.Where(x => !x.IsReference && !x.IsInstructorOnly).OrderByDescending(x => x.DateModified).ToList(),
                InstructorOnlyDocumentUses = allDocUseObjs.Where(x => x.IsReference).OrderByDescending(x => x.DateModified).ToList(),
                ReferenceDocumentUses = allDocUseObjs.Where(x => x.IsInstructorOnly).OrderByDescending(x => x.DateModified).ToList(),
                DateTimeLessonPlanChoiceConfirmed = lesson.LessonPlanDateChoiceConfirmed,
                ReferenceDateTimeLessonPlanChoiceConfirmed = lesson.LessonPlanReferenceDateChoiceConfirmed,
                MetaLessonId = lesson.MetaLessonId,
            };
        }


        public LessonForComparisonDomainObj BuildEmpty()
        {
            var basic = LessonDomainObjBuilder.BuildBasicEmpty();

            return new LessonForComparisonDomainObj(basic)
            {
                ChangeNotes = "",
                IsActive = false,
                LessonPlanObj = LessonPlanDomainObjBuilder.BuildEmpty(),
                NarrativeObj = NarrativeDomainObjBuilder.BuildEmpty(),
                MasterLessonId = Guid.Empty,
                DocumentsModifiedDate = new DateTime (),
                UserName ="_" 

            };
        }



        /// <summary>
        /// A method to build a basic lesson domain object.
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public static LessonDomainObjBasic BuildBasic(Lesson lesson)
        {
            return LessonDomainObjBuilder.BuildBasic(lesson);
        }



        /// <summary>
        /// A method to build a lesson domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonForComparisonDomainObj BuildFromId(Guid id)
        {

            Lesson lesson = _lessonRepo.GetById(id);
            if (lesson == null)
            {
                return BuildEmpty();
            }
            else
            {
                LessonForComparisonDomainObj lessonForComparisonObj = Build(lesson);
                return lessonForComparisonObj;
            }
        }


        /// <summary>
        /// A method to build a basic lesson domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonDomainObjBasic BuildBasicFromId(Guid id)
        {
            Lesson lesson = _lessonRepo.GetById(id);
            if (lesson == null)
            {
                return null;
            }
            else
            {
                return BuildBasic(lesson);
            }
        }

        public List<LessonForComparisonDomainObj> AllGroupLessons(LessonForComparisonDomainObj editableLessonObj)
        {
            Guid masterLessonId = editableLessonObj.MasterLessonId == Guid.Empty ? editableLessonObj.Id : editableLessonObj.MasterLessonId; ;
            List<LessonForComparisonDomainObj> lessonObjList = _lessonRepo.LessonsThatShareMaster(masterLessonId).Select(x => Build(x)).ToList();
            foreach(var xLessonObj in lessonObjList)
            {
                xLessonObj.EditableLessonObj = editableLessonObj.LessonObjBasic;
            }
            return lessonObjList.ToList();
        }

        public LessonForComparisonDomainObj GetMasterObjForLessonId(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if(lesson != null)
            {
                if (lesson.Course.IsMaster)
                {
                    return Build(lesson);
                }
                else if (lesson.MasterLesson != null)
                {
                    return Build(lesson.MasterLesson);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public LessonForComparisonDomainObj GetObjForMasterLessonAndUser(Guid masterLessonId, Guid userId)
        {
            Lesson lesson = _lessonRepo.LessonForMasterLessonAndUser(masterLessonId, userId);
            return lesson == null ? null : Build(lesson);
        }




    }
}
