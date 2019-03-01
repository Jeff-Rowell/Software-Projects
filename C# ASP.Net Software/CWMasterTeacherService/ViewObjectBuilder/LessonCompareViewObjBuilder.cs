using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.DomainObjectBuilders;
using static CWMasterTeacherDomain.DomainUtilities;
using System.Web.Mvc;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class LessonCompareViewObjBuilder
    {
        private LessonForComparisonDomainObjBuilder _lessonForComparisonObjBuilder;
        private StashedLessonPlanDomainObjBuilder _stashedLessonPlanDomainObjBuilder;
        private ILessonPlanDomainObjBuilder _lessonPlanDomainObjBuilder;
        private NarrativeDomainObjBuilder _narrativeDomainObjBuilder;
        private LessonCUDService _lessonCUDService;

        public LessonCompareViewObjBuilder(LessonForComparisonDomainObjBuilder lessonForComparisonBuilder,
                                            StashedLessonPlanDomainObjBuilder stashedLessonPlanDomainObjBuilder,
                                            ILessonPlanDomainObjBuilder lessonPlanDomainObjBuilder,
                                            LessonCUDService lessonCUDService,
                                            NarrativeDomainObjBuilder narrativeDomainObjBuilder)
        {
            _lessonForComparisonObjBuilder = lessonForComparisonBuilder;
            _stashedLessonPlanDomainObjBuilder = stashedLessonPlanDomainObjBuilder;
            _lessonPlanDomainObjBuilder = lessonPlanDomainObjBuilder;
            _lessonCUDService = lessonCUDService;
            _narrativeDomainObjBuilder = narrativeDomainObjBuilder;
        }


        public LessonCompareViewObj Retrieve(Guid editableLessonId, Guid comparisonLessonId, UserDomainObj currentUserObj,
                                            Guid specifiedLessonPlanId, Guid specifiedNarrativeId,
                                            DateTime? lessonPlanReferenceTime, DateTime? narrativeReferenceTime, DateTime? documentsReferenceTime,
                                            bool doSuggestUseMaster, bool isDisplayModeNarrative, bool isDisplayModeLessonPlan, bool isDisplayModeDocuments,
                                            Guid comparisonSelectedDocumentUseId, Guid editableSelectedDocumentUseId, bool doLockEditableLesson,
                                            bool doLockComparisonLesson, Guid lastEditableLessonId, Guid containerLessonId, Guid containerCourseId,
                                            int comparisonModeInt)
        {


            LessonCompareViewObj viewObj = new LessonCompareViewObj();

            List<LessonForComparisonDomainObj> allGroupLessonsList = new List<LessonForComparisonDomainObj>();
            List<LessonForComparisonDomainObj> narrativeComparisonLessonList = new List<LessonForComparisonDomainObj>();
            List<LessonForComparisonDomainObj> lessonPlanComparisonLessonList = new List<LessonForComparisonDomainObj>();
            List<LessonForComparisonDomainObj> documentsComparisonLessonList = new List<LessonForComparisonDomainObj>();

            LessonForComparisonDomainObj comparisonLessonObj = null;

            if (comparisonLessonId != Guid.Empty)
            {
                comparisonLessonObj = _lessonForComparisonObjBuilder.BuildFromId(comparisonLessonId);
                comparisonLessonObj = SetSpecifiedLessonPlanAndNarrative(comparisonLessonObj, specifiedLessonPlanId, specifiedNarrativeId);
            }


            LessonForComparisonDomainObj editableLessonObj = GetEditableLessonObj(comparisonLessonObj: comparisonLessonObj,
                                                                    editableLessonId: editableLessonId, comparisonLessonId: comparisonLessonId,
                                                                    comparisonModeInt: comparisonModeInt,
                                                                    currentUserId: currentUserObj.Id);
            viewObj.EditableLessonObj = editableLessonObj;
            viewObj.CurrentUserObj = currentUserObj;

            viewObj = SetDisplayMode(viewObj, isDisplayModeNarrative, isDisplayModeLessonPlan, isDisplayModeDocuments);
            if (editableLessonObj != null && DoGetLessonLists(comparisonModeInt))
            {
                allGroupLessonsList = _lessonForComparisonObjBuilder.AllGroupLessons(editableLessonObj).ToList();
                narrativeComparisonLessonList = allGroupLessonsList.Where(x => x.NarrativeHasNewChangesRelToGivenLesson).ToList();
                lessonPlanComparisonLessonList = GetLessonPlanComparisonLessonList(allGroupLessonsList);
                documentsComparisonLessonList = allGroupLessonsList.Where(x => x.DocumentsHaveNewChangesRelToGivenLesson).ToList();

                viewObj.NarrativeComparisonLessonList = narrativeComparisonLessonList;
                viewObj.LessonPlanComparisonLessonList = lessonPlanComparisonLessonList;
                viewObj.DocumentsComparisonLessonList = documentsComparisonLessonList;
                viewObj.AllGroupLessonsList = allGroupLessonsList;
            }

            if (DomainUtilities.CompModeIsViewArchive(comparisonModeInt))
            {
                comparisonLessonId = editableLessonId;
            }

            if (comparisonLessonObj == null)
            {
                comparisonLessonObj = GetComparisonLessonObjForEmptyId(comparisonLessonId: comparisonLessonId,
                                                            specifiedLessonPlanId: specifiedLessonPlanId,
                                                            isDisplayModeNarrative: viewObj.IsDisplayModeNarrative,
                                                            isDisplayModeLessonPlan: viewObj.IsDisplayModeLessonPlan,
                                                            isDisplayModeDocuments: viewObj.IsDisplayModeDocuments,
                                                            narrativeComparisonLessonList: narrativeComparisonLessonList,
                                                            lessonPlanComparisonLessonList: lessonPlanComparisonLessonList,
                                                            documentsComparisonLessonList: documentsComparisonLessonList,
                                                            specifiedNarrativeId: specifiedNarrativeId);
            }

            if (editableLessonObj != null)
            {
                comparisonLessonObj.SetDocumentComparisonBooleans(editableLessonObj.AllDocumentUses, editableLessonObj.DocumentsDateChoiceConfirmed);
            }

            viewObj.ComparisonLessonObj = comparisonLessonObj;

            viewObj.MasterLessonObj = _lessonForComparisonObjBuilder.GetMasterObjForLessonId(editableLessonId);

            //diff_match_patch diff = new diff_match_patch();
            //var d = diff.diff_main(editableLessonObj.LessonPlanText, comparisonLessonObj.LessonPlanText);
            //diff.diff_cleanupSemantic(d);
            //viewObj.NarrativeDiff = d;

            viewObj.BackInTimeSelectList = GetBackInTimeSelectList();
            viewObj.SetAllSelectedDocumentUseIds(comparisonSelectedDocumentUseId, editableSelectedDocumentUseId);
            viewObj.DoLockEditableLesson = doLockEditableLesson;
            viewObj.DoLockComparisonLesson = true;
            viewObj.LastEditableLessonId = lastEditableLessonId;
            viewObj.ContainerLessonId = containerLessonId;
            viewObj.ContainerCourseId = containerCourseId;
            viewObj.ComparisonModeInt = comparisonModeInt;

            return viewObj;
        }

        private LessonForComparisonDomainObj GetComparisonLessonObjForEmptyId(Guid comparisonLessonId, Guid specifiedLessonPlanId, bool isDisplayModeNarrative,
                                                                bool isDisplayModeLessonPlan, bool isDisplayModeDocuments, Guid specifiedNarrativeId,
                                                                List<LessonForComparisonDomainObj> narrativeComparisonLessonList,
                                                                List<LessonForComparisonDomainObj> lessonPlanComparisonLessonList,
                                                                List<LessonForComparisonDomainObj> documentsComparisonLessonList)
        {
            LessonForComparisonDomainObj comparisonLessonObj = null;

            if (isDisplayModeNarrative)
            {
                if (narrativeComparisonLessonList.Count > 0)
                {
                    comparisonLessonObj = narrativeComparisonLessonList.FirstOrDefault();
                }
                else
                {
                    comparisonLessonObj = _lessonForComparisonObjBuilder.BuildEmpty();
                    comparisonLessonObj.CustomNarrativeText = "No Narratives to compare.";
                }
            }
            else if (isDisplayModeLessonPlan)
            {
                if (lessonPlanComparisonLessonList.Count > 0)
                {
                    comparisonLessonObj = lessonPlanComparisonLessonList.FirstOrDefault();
                }
                else
                {
                    comparisonLessonObj = _lessonForComparisonObjBuilder.BuildEmpty();
                    comparisonLessonObj.LessonPlanText = "No LessonPlans to compare.";
                }
            }
            else if (isDisplayModeDocuments)
            {
                if (documentsComparisonLessonList.Count > 0)
                {
                    comparisonLessonObj = documentsComparisonLessonList.FirstOrDefault();
                }
                else
                {
                    comparisonLessonObj = _lessonForComparisonObjBuilder.BuildEmpty();
                }
            }
            else
            {
                comparisonLessonObj = _lessonForComparisonObjBuilder.BuildEmpty();
            }


            comparisonLessonObj = SetSpecifiedLessonPlanAndNarrative(comparisonLessonObj, specifiedLessonPlanId, specifiedNarrativeId);

            return comparisonLessonObj;
        }

        /// <summary>
        /// THis is used when calling up a specific Lesson Plan or Narrative from Archive
        /// </summary>
        private LessonForComparisonDomainObj SetSpecifiedLessonPlanAndNarrative(LessonForComparisonDomainObj comparisonLessonObj, Guid specifiedLessonPlanId, Guid specifiedNarrativeId)
        {
            if (specifiedLessonPlanId != Guid.Empty)
            {
                comparisonLessonObj.LessonPlanObj = _lessonPlanDomainObjBuilder.BuildFromId(specifiedLessonPlanId);
            }
            if (specifiedNarrativeId != Guid.Empty)
            {
                comparisonLessonObj.NarrativeObj = _narrativeDomainObjBuilder.BuildFromId(specifiedNarrativeId);
                comparisonLessonObj.IsNarrativeSpecified = true;
            }
            return comparisonLessonObj;
        }







        private LessonCompareViewObj SetDisplayMode(LessonCompareViewObj viewObj, bool isDisplayModeNarrative,
                                                            bool isDisplayModeLessonPlan, bool isDisplayModeDocuments)
        {
            if (isDisplayModeNarrative)
            {
                viewObj.IsDisplayModeNarrative = isDisplayModeNarrative;
            }
            else if (isDisplayModeDocuments)
            {
                viewObj.IsDisplayModeDocuments = isDisplayModeDocuments;
            }
            else
            {
                viewObj.IsDisplayModeLessonPlan = true;
            }
            return viewObj;
        }

        private SelectList GetBackInTimeSelectList()
        {
            var dictionary = new Dictionary<int, string>
            {
                {-1, "Set to Current Date" },
                {1, "one day" },
                {7, "one week" },
                {31, "one month" },
                {93, "three months" },
                {366, "one year" },
                {3660, "ten years" }
            };

            return new SelectList(dictionary, "Key", "Value");
        }

        private bool DoGetLessonLists(int comparisonModeInt)
        {
            return (DomainUtilities.CompModeIsShowComparisonLessons(comparisonModeInt)) || (DomainUtilities.CompModeIsUpdateFromMaster(comparisonModeInt)) || (DomainUtilities.CompModeIsUpdateFromGroup(comparisonModeInt));
        }

        private LessonForComparisonDomainObj GetEditableLessonObj(LessonForComparisonDomainObj comparisonLessonObj,
                                                                    Guid editableLessonId, Guid comparisonLessonId,
                                                                int comparisonModeInt, Guid currentUserId)
        {
            if (DomainUtilities.CompModeIsUpdateFromMaster(comparisonModeInt)
                || DomainUtilities.CompModeIsUpdateFromGroup(comparisonModeInt)
                || DomainUtilities.CompModeIsImportLesson(comparisonModeInt))
            {
                if (comparisonLessonObj != null)
                {
                    Guid masterLessonId = comparisonLessonObj.IsMaster ? comparisonLessonObj.Id : comparisonLessonObj.MasterLessonId;
                    LessonForComparisonDomainObj returnLesson = _lessonForComparisonObjBuilder.GetObjForMasterLessonAndUser(masterLessonId, currentUserId);
                    if (returnLesson != null)
                    {
                        return returnLesson;
                    }
                }
                else if (editableLessonId != Guid.Empty)
                {
                    return _lessonForComparisonObjBuilder.BuildFromId(editableLessonId);
                }

            }
            else if (DomainUtilities.CompModeIsShowComparisonLessons(comparisonModeInt)
                        || DomainUtilities.CompModeIsViewArchive(comparisonModeInt)
                        || DomainUtilities.CompModeIsImportContent(comparisonModeInt))
            {
                if (editableLessonId != Guid.Empty)
                {
                    return _lessonForComparisonObjBuilder.BuildFromId(editableLessonId);
                }
            }
            return _lessonForComparisonObjBuilder.BuildEmpty();
        }

        List<LessonForComparisonDomainObj> GetLessonPlanComparisonLessonList(List<LessonForComparisonDomainObj> allGroupLessonsList)
        {
            List<LessonForComparisonDomainObj> lessonPlanLessonObjList = allGroupLessonsList
                                                                .Where(x => x.LessonPlanHasNewChangesRelToGivenLesson)
                                                                .OrderBy(x => x.LessonPlanCreatedDate)
                                                                .ToList();
            //We are doing this this way to essentially create a Hash Set but to have the oldest created be the one in the list.
            List<LessonForComparisonDomainObj> returnList = new List<LessonForComparisonDomainObj>();
            foreach( var xLessonObj in lessonPlanLessonObjList)
            {
                if(!returnList.Select(x => x.LessonPlanId).Contains(xLessonObj.LessonPlanId))
                {
                    returnList.Add(xLessonObj);
                }
            }
            return returnList;
        }
    }
}
