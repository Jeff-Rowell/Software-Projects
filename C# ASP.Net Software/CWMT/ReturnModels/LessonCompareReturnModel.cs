using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT
{
    public class LessonCompareReturnModel
    {
        public LessonCompareReturnModel()
        {
            DoSuggestRemoveComment = false;
            DoSuggestUseMaster = false;
        }
        public bool IsDisplayModeLessonPlan { get; set; }
        public bool IsDisplayModeNarrative { get; set; }
        public bool IsDisplayModeDocuments { get; set; }
        public bool DoSuggestUseMaster { get; set; }
        public bool DoSuggestRemoveComment { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EditableLessonPlanText { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EditableNarrativeText { get; set; }

        public Guid ComparisonLessonId { get; set; }
        public Guid EditableLessonId { get; set; }
        public Guid EditableLessonPlanId { get; set; }
        public Guid ComparisonLessonPlanId { get; set; }
        public Guid SpecifiedLessonPlanId { get; set; }

        public DateTime ComparisonLessonPlanModifiedDate { get; set; }
        public int BackInTimeValue { get; set; }

        public Guid EditableNarrativeId { get; set; }
        public Guid ComparisonNarrativeId { get; set; }
        public Guid SpecifiedNarrativeId { get; set; }

        public DateTime ComparisonNarrativeModifiedDate { get; set; }
        public int NarrativeBackInTimeValue { get; set; }

        public Guid ComparisonSelectedDocumentUseId { get; set; }
        public Guid EditableSelectedDocumentUseId { get; set; }

        public bool DoLockEditableLesson { get; set; }
        public bool DoLockComparisonLesson { get; set; }

        public bool EditableLessonIsHidden { get; set; }
        public bool LessonPlanIsFromArchive { get; set; }

        public int ComparisonModeInt { get; set; }
        public Guid StashedNarrativeId { get; set; }
        public Guid StashedLessonPlanId { get; set; }


    }
}