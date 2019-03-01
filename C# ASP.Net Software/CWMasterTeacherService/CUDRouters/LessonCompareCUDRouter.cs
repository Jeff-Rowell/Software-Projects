using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherService.CUDServices;

namespace CWMasterTeacherService.CUDRouters
{
    public class LessonCompareCUDRouter
    {

        private LessonPlanCUDService _lessonPlanCUDService;
        private StashedLessonPlanCUDService _stashedLessonPlanCUDService;
        private NarrativeCUDService _narrativeCUDService;
        private StashedNarrativeCUDService _stashedNarrativeCUDService;
        private LessonCUDService _lessonCUDService;
        private DocumentCUDService _documentCUDService;

        public LessonCompareCUDRouter(LessonPlanCUDService lessonPlanCUDService, StashedLessonPlanCUDService stashedLessonPlanCUDService,
                                    NarrativeCUDService narrativeCUDService, StashedNarrativeCUDService stashedNarrativeCUDService,
                                    LessonCUDService lessonCUDService, DocumentCUDService documentCUDService)
        {
            _lessonPlanCUDService = lessonPlanCUDService;
            _stashedLessonPlanCUDService = stashedLessonPlanCUDService;
            _narrativeCUDService = narrativeCUDService;
            _stashedNarrativeCUDService = stashedNarrativeCUDService;
            _lessonCUDService = lessonCUDService;
            _documentCUDService = documentCUDService;
        }

        public void AddOrUpdateLessonPlan(Guid lessonPlanId, Guid lessonId, Guid userId, string text, string name,
                                            bool isPreNote, bool isPostNote)
        {
            _lessonPlanCUDService.AddOrUpdateLessonPlan(lessonPlanId: lessonPlanId,
                                                        lessonId: lessonId,
                                                        userId: userId,
                                                        text: text,
                                                        name: name,
                                                        isPreNote: isPreNote,
                                                        isPostNote: isPostNote);

        }


        public void CreateStashedLessonPlan(Guid lessonId, Guid lessonPlanId, bool isSaved)
        {
            _stashedLessonPlanCUDService.CreateStashedLessonPlanFromLessonId(lessonId, lessonPlanId, isSaved);
        }

        public void ImportLessonPlanAsCurrent(Guid lessonId, Guid lessonPlanId, bool doCheckStashForDuplicate)
        {
            _lessonPlanCUDService.ChangeLessonPlanForLesson(lessonId, lessonPlanId, doCheckStashForDuplicate);
        }

        public void UpdateLessonPlanChangesAcceptedDate(Guid editableLessonId, Guid editableLessonPlanId, Guid comparisonLessonPlanId, bool doSuggestUseMaster)
        {
            _lessonCUDService.UpdateLessonPlanDateChoiceConfirmed(editableLessonId: editableLessonId,
                                                            editableLessonPlanId: editableLessonPlanId,
                                                            comparisonLessonPlanId: comparisonLessonPlanId, 
                                                            doSuggestUseMaster: doSuggestUseMaster);
        }

        public void SetLessonPlanDateChangesAcceptedBackInTime(Guid editableLessonId, Guid editableLessonPlanId, int numberOfDaysBack)
        {
            if (numberOfDaysBack > 0)
            {
                _lessonCUDService.SetLessonPlanDateChoiceConfirmedBackInTime(editableLessonId, numberOfDaysBack);
            }
            else
            {
                _lessonCUDService.SetLessonPlanDateChoiceConfirmed(editableLessonId: editableLessonId, 
                                                                editableLessonPlanId: editableLessonPlanId,
                                                                date: DateTime.Now,
                                                                doSuggestUseMaster: false);
            }
        }

        public void ReturnToLessonPlanReferenceChangesAcceptedDate(Guid editableLessonId)
        {
            _lessonCUDService.ReturnToReferenceChoiceConfirmedDate(editableLessonId);
        }

        //**************************************************************
        public void AddOrUpdateNarrative(Guid narrativeId, Guid lessonId, Guid userId, string text, string name,
                                            bool isPreNote, bool isPostNote)
        {
            _narrativeCUDService.UpdateNarrative(narrativeId: narrativeId,
                                                        lessonId: lessonId,
                                                        text: text);
        }

        public void RemoveStashedNarrativeFromArchive(Guid lessonId, Guid narrativeId)
        {
            _narrativeCUDService.RemoveStashedNarrativeFromArchive(lessonId, narrativeId);
        }

        public void CreateStashedNarrative(Guid lessonId, Guid narrativeId, bool isSaved)
        {
            _stashedNarrativeCUDService.CreateStashedNarrativeFromLessonId(lessonId, narrativeId, isSaved);
        }

        public void ImportNarrativeAsCurrent(Guid lessonId, Guid narrativeId)
        {
            _narrativeCUDService.ChangeNarrativeForLesson(lessonId, narrativeId);
        }

        public void UpdateNarrativeChangesAcceptedDate(Guid editableLessonId, Guid comparisonLessonId, bool doSuggestRemoveComment)
        {
            _lessonCUDService.UpdateNarrativeDateChoiceConfirmed(editableLessonId: editableLessonId, 
                                                                    comparisonLessonId: comparisonLessonId, 
                                                                    doSuggestRemoveComment: doSuggestRemoveComment);
        }

        public void SetNarrativeDateChangesAcceptedBackInTime(Guid editableLessonId, int numberOfDaysBack)
        {
            if (numberOfDaysBack > 0)
            {
                _lessonCUDService.SetNarrativeDateChoiceConfirmedBackInTime(editableLessonId, numberOfDaysBack);
            }
            else
            {
                _lessonCUDService.SetNarrativeDateChoiceConfirmed(editableLessonId, DateTime.Now, false);
            }
            
        }

        public void ReturnToNarrativeReferenceChangesAcceptedDate(Guid editableLessonId)
        {
            _lessonCUDService.ReturnToReferenceNarrativeChoiceConfirmedDate(editableLessonId: editableLessonId);
        }

        // Documents stuff ****************************************************************************************

        public void ImportDocumentIntoLesson(Guid lessonId, Guid documentUseId)
        {
            _documentCUDService.AddDocFromDocUseIdToLesson(lessonId, documentUseId);
        }
        public void UpdateDocumentsChoiceConfirmedDate(Guid editableLessonId, Guid comparisonLessonId)
        {
            _lessonCUDService.UpdateDocumentsChoiceConfirmedDateToComparisonLesson(editableLessonId, comparisonLessonId);
        }

        public void SetDocumentsDateChoiceConfirmeddBackInTime(Guid editableLessonId, int numberOfDaysBack)
        {
            if(numberOfDaysBack > 0)
            {
                _lessonCUDService.SetDateDocChoiceConfirmedBackInTime(editableLessonId, numberOfDaysBack);
            }
            else
            {
                _lessonCUDService.UpdateDocumentsChoiceConfirmedDate(editableLessonId, DateTime.Now);
            }
        }

        public void ReturnToReferenceDateDocumentChoiceConfirmed(Guid editableLessonId)
        {
            _lessonCUDService.ReturnToReferenceDateDocumentChoiceConfirmed(editableLessonId);
        }

        //**********************************

        public Guid ImportLesson_ReturnId(Guid fromLessonId, Guid containerLessonId, Guid toCourseLessonId)
        {
            return _lessonCUDService.ImportLesson_ReturnId(fromLessonId, containerLessonId, toCourseLessonId);
        }

        public void SetIsHidden(Guid lessonId, bool isHidden)
        {
            _lessonCUDService.SetIsHidden(lessonId, isHidden);
        }

        public void RemoveDocumentFromLesson(Guid documentUseId)
        {
            _documentCUDService.RemoveDocumentFromLesson(documentUseId);
        }

    }
}
