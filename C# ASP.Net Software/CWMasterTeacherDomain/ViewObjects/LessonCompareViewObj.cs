using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CWMasterTeacherDomain.DomainObjects;
using System.Text.RegularExpressions;
using System.Web;
using CWMasterTeacher3;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class LessonCompareViewObj
    {
        public LessonCompareViewObj()
        {

        }

        public LessonForComparisonDomainObj EditableLessonObj { get; set; }
        public LessonForComparisonDomainObj ComparisonLessonObj { get; set; }
        public LessonForComparisonDomainObj MasterLessonObj { get; set; }
        public UserDomainObj CurrentUserObj { get; set; }

        public bool UserIsApplicationAdmin { get { return CurrentUserObj.IsApplicationAdmin; } }
        public bool UserIsWorkingGroupAdmin { get { return CurrentUserObj.IsWorkingGroupAdmin; } }
        public bool UserIsNarrativeEditor { get { return CurrentUserObj.IsMasterEditor; } }

        public bool IsDisplayModeLessonPlan { get; set; }
        public bool IsDisplayModeNarrative { get; set; }
        public bool IsDisplayModeDocuments { get; set; }
        public int ComparisonModeInt { get; set; }
        

        [Display(Name = "Lock Lesson")]
        public bool DoLockEditableLesson { get; set; }

        /// <summary>
        /// This may be deprecated.  I think it is always true now.  I'm leaving it in until we find time to remove references to it.
        /// </summary>
        [Display(Name = "Lock Lesson")]
        public bool DoLockComparisonLesson { get; set; }

        //These are pass-throughs for the EmptyEditable partial
        public Guid LastEditableLessonId { get; set; }
        public Guid ContainerLessonId { get; set; }
        public Guid ContainerCourseId { get; set; }

        public bool DoShowSuggestions
        {
            get { return EditableLessonObj.IsMaster && !ComparisonLessonObj.IsMaster; }
        }

        public bool DoShowImportLessonPlan
        {
            get { return !ComparisonLessonObj.IsEmpty && ComparisonLessonPlanId != EditableLessonPlanId; }
        }

        public string EditableTitleCSS
        {
            get
            {
                return DomainWebUtilities.CourseLessonTitleClass(EditableLessonObj.IsMaster, false, true);
            }
        }

        public string ComparisonTitleCSS
        {
            get
            {
                return DomainWebUtilities.CourseLessonTitleClass(ComparisonLessonObj.IsMaster, false, true);
            }
        }

        public List<LessonForComparisonDomainObj> AllGroupLessonsList { get; set; }

        public string ComparisonDisplayName
        {
            get { return ComparisonLessonObj == null ? "_" : ComparisonLessonObj.ShortExtendedDisplayName; }
        }

        public string EditableDisplayName
        {
            get { return EditableLessonObj.ShortExtendedDisplayName; }
        }

        public Guid ComparisonLessonId
        {
            get { return ComparisonLessonObj == null ? Guid.Empty : ComparisonLessonObj.Id; }
        }
        public Guid EditableLessonId
        {
            get { return EditableLessonObj == null ? Guid.Empty : EditableLessonObj.Id; }
        }

        public Guid MasterLessonId
        {
            get { return MasterLessonObj == null ? Guid.Empty : EditableLessonObj.Id; }
        }

        public string NarrativeButtonCSS
        {
            get { return DomainWebUtilities.TabButtonCSS(IsDisplayModeNarrative); }
        }
        public string LessonPlanButtonCSS
        {
            get { return DomainWebUtilities.TabButtonCSS(IsDisplayModeLessonPlan); }
        }
        public string DocumentsButtonCSS
        {
            get { return DomainWebUtilities.TabButtonCSS(IsDisplayModeDocuments); }
        }

        [Display(Name = "Is Hidden")]
        public bool EditableLessonIsHidden
        {
            get { return EditableLessonObj == null ? false : EditableLessonObj.IsHidden; }
        }

        //These Properties relate to the LessonPlan mode ********************************************************************

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EditableLessonPlanText
        {
            get { return EditableLessonObj == null ? "_" : EditableLessonObj.LessonPlanText;  }
        }

        public string ComparisonLessonPlanText
        {
            get { return ComparisonLessonObj == null ? "_" : ComparisonLessonObj.LessonPlanText; }
        }
        public SelectList BackInTimeSelectList { get; set; }
        public int BackInTimeValue { get; set; }


        [Display(Name = "Suggest using master")]
        public bool DoSuggestUseMaster { get; set; }

        public List<StashedLessonPlanDomainObj> HistoryLessonPlans
        {
            get { return ComparisonLessonObj == null ? new List<StashedLessonPlanDomainObj>() : ComparisonLessonObj.HistoryLessonPlans; }
        }
        public List<StashedLessonPlanDomainObj> SavedLessonPlans
        {
            get { return ComparisonLessonObj == null ? new List<StashedLessonPlanDomainObj>() : ComparisonLessonObj.SavedLessonPlans; }
        }


        public bool ShowLessonPlanArchive
        {
            get
            {
                return (ComparisonLessonObj.IsMaster || EditableLessonObj.Id == ComparisonLessonObj.Id) 
                            && DomainUtilities.CompModeIsViewArchive(ComparisonModeInt) ;
            }
        }

        public Guid EditableLessonPlanId
        {
            get { return EditableLessonObj.LessonPlanObj.Id; }
        }

        public Guid ComparisonLessonPlanId
        {
            get { return ComparisonLessonObj == null ? Guid.Empty : ComparisonLessonObj.LessonPlanObj.Id; }
        }

        public string ComparisonLessonPlanDateDisplay
        {
            //get { return ComparisonLessonObj == null ? "_" : "Modified on " + ComparisonLessonObj.LessonPlanObj.DateModified.ToShortDateString(); }
            get { return ComparisonLessonObj == null ? "_" : "Modified on " + ComparisonLessonObj.LessonPlanObj.DateModified.ToString("M/d/yyyy h:mm tt"); }
        }

        public string EditableLessonPlanDateDisplay
        {
            get { return EditableLessonObj == null ? "_" : "Current through " + EditableLessonObj.LessonPlanDateChoiceConfirmed.ToString("M/d/yyyy h:mm tt"); }

        }
        public DateTime ComparisonLessonPlanModifiedDate
        {
            get { return ComparisonLessonObj == null ? new DateTime() : ComparisonLessonObj.LessonPlanModifiedDate; }
        }

        public bool DoShowLessonPlanAccept
        {
            get { return DomainUtilities.Date1_IsGreater(ComparisonLessonObj.LessonPlanModifiedDate, EditableLessonObj.LessonPlanDateChoiceConfirmed)
                            && (DomainUtilities.CompModeIsShowComparisonLessons(ComparisonModeInt) 
                            || DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt)
                            || DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt));
            }
        }

        public bool DoShowLessonPlanGoBackInTime
        {
            get { return EditableLessonObj.LessonPlanDateChoiceConfirmed >= EditableLessonObj.LessonPlanReferenceDateChoiceConfirmed; }
        }

        public string LessonPlanBackInTimeReturnLabel
        {
            get { return "Return to " + EditableLessonObj.LessonPlanReferenceDateChoiceConfirmed.ToShortDateString(); }
        }

        public List<LessonForComparisonDomainObj> LessonPlanComparisonLessonList { get; set; }

        public string LessonPlanTabLabel
        {
            get
            {
                string suffix = LessonPlanComparisonLessonList == null ? "" : " (" + LessonPlanComparisonLessonList.Count.ToString() + ")";
                return DomainWebUtilities.LessonPlanTypeName + suffix;
            }
        }

        //These Properties relate to the Narrative Mode ****************************************************************

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EditableNarrativeText
        {
            get { return EditableLessonObj == null ? "_" : EditableLessonObj.NarrativeText; }
        }

        public string ComparisonNarrativeText
        {
            get { return ComparisonLessonObj == null ? "_" : ComparisonLessonObj.NarrativeText; }
        }

        [Display(Name = "Suggest dropping comment")]
        public bool DoSuggestDropComment { get; set; }

        public int NarrativeBackInTimeValue { get; set; }

        public List<StashedNarrativeDomainObj> HistoryNarratives
        {
            get { return ComparisonLessonObj == null ? new List<StashedNarrativeDomainObj>() : ComparisonLessonObj.HistoryNarratives ; }
        }

        public List<StashedNarrativeDomainObj>SavedNarratives
        {
            get { return ComparisonLessonObj == null ? new List<StashedNarrativeDomainObj>() : ComparisonLessonObj.SavedNarratives; }
        }


        public bool ShowStashedNarratives
        {
            get
            {
                return ComparisonLessonObj.IsMaster || EditableLessonObj.Id == ComparisonLessonObj.Id;
            }
        }

        public Guid EditableNarrativeId
        {
            get { return EditableLessonObj.NarrativeObj.Id; }
        }

        public Guid ComparisonNarrativeId
        {
            get { return ComparisonLessonObj == null ? Guid.Empty : ComparisonLessonObj.NarrativeObj.Id; }
        }

        public string ComparisonNarrativeDateDisplay
        {
            get { return ComparisonLessonObj == null ? "_" : "Modified on " + ComparisonLessonObj.NarrativeObj.DateModified.ToString("M/d/yyyy h:mm tt"); }
        }

        public string EditableNarrativeDateDisplay
        {
            get { return ComparisonLessonObj == null ? "_" : "Current through " + EditableLessonObj.NarrativeDateChoiceConfirmed.ToString("M/d/yyyy h:mm tt"); }
        }
        public DateTime ComparisonNarrativeModifiedDate
        {
            get { return ComparisonLessonObj == null ? new DateTime() : ComparisonLessonObj.NarrativeModifiedDate; }
        }

        public bool ShowNarrativeAccept
        {
            get { return DomainUtilities.Date1_IsGreater(ComparisonLessonObj.NarrativeModifiedDate, EditableLessonObj.NarrativeDateChoiceConfirmed); }
        }

        public bool ShowNarrativeGoBackInTime
        {
            get { return EditableLessonObj.NarrativeDateChoiceConfirmed  >= EditableLessonObj.NarrativeReferenceDateChoiceConfirmed; }
        }

        public string NarrativeBackInTimeReturnLabel
        {
            get { return "Return to " + EditableLessonObj.NarrativeReferenceDateChoiceConfirmed.ToShortDateString(); }
        }

        public List<LessonForComparisonDomainObj> NarrativeComparisonLessonList { get; set; }

        public string NarrativeTabLabel
        {
            get
            {
                string suffix = NarrativeComparisonLessonList == null ? "" : " (" + NarrativeComparisonLessonList.Count.ToString() + ")";
                return DomainWebUtilities.NarrativeTypeName + suffix ;
            }
        }


        //These Properties relate to the Documents view  *****************************************************************************

        public string ComparisonDocumentsDateDisplay
        {
            get { return ComparisonLessonObj == null ? "_" : "Modified on " + ComparisonLessonObj.DateDocumentsModified.ToString("M/d/yyyy h:mm tt"); }
        }

        public string EditableDocumentsDateDisplay
        {
            get { return ComparisonLessonObj == null ? "_" : "Current through " + EditableLessonObj.DocumentsDateChoiceConfirmed.ToString("M/d/yyyy h:mm tt"); }
        }
        public DateTime ComparisonDocumentsModifiedDate
        {
            get { return ComparisonLessonObj == null ? new DateTime() : ComparisonLessonObj.DocumentsModifiedDate; }
        }

        public bool ShowDocumentsAccept
        {
            get { return DomainUtilities.Date1_IsGreater(ComparisonLessonObj.DocumentsModifiedDate, EditableLessonObj.DocumentsDateChoiceConfirmed); }
        }

        public bool ShowDocumentsGoBackInTime
        {
            get { return EditableLessonObj.DocumentsDateChoiceConfirmed >= EditableLessonObj.ReferenceDateDocChoiceConfirmed; }
        }

        public string DocumentsBackInTimeReturnLabel
        {
            get { return "Return to " + EditableLessonObj.ReferenceDateDocChoiceConfirmed.ToShortDateString(); }
        }

        public Guid ComparisonSelectedDocumentUseId { get; private set; }
        public Guid EditableSelectedDocumentUseId { get; private set; }

        public void SetAllSelectedDocumentUseIds(Guid comparisonSelectedDocUseId, Guid editableSelectedDocUseId)
        {
            ComparisonSelectedDocumentUseId = comparisonSelectedDocUseId;
            ComparisonLessonObj.SetAllSelectedDocumentUseIds(comparisonSelectedDocUseId);
            EditableSelectedDocumentUseId = editableSelectedDocUseId;
            EditableLessonObj.SetAllSelectedDocumentUseIds(editableSelectedDocUseId);
        }

        public List<LessonForComparisonDomainObj> DocumentsComparisonLessonList { get; set; }

        public string DocumentsTabLabel
        {
            get
            {
                string suffix = DocumentsComparisonLessonList == null ? "" : " (" + DocumentsComparisonLessonList.Count.ToString() + ")";
                return "Documents" + suffix;
            }
        }

        public string ComparisonModeHeading
        {
            get
            {
                if (DomainUtilities.CompModeIsImportContent(ComparisonModeInt))
                {
                    return "Import Content";
                }
                else if (DomainUtilities.CompModeIsImportLesson(ComparisonModeInt))
                {
                    return "Import Lesson";
                }
                else if (DomainUtilities.CompModeIsViewArchive(ComparisonModeInt))
                {
                    return "View Archive";
                }
                else if (DomainUtilities.CompModeIsShowComparisonLessons(ComparisonModeInt))
                {
                    return "View Comparison Lessons";
                }
                else if (DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt))
                {
                    return "View Changes to Master Lesson";
                }
                else if (DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt))
                {
                    return "View Changes to Individual Lessons";
                }
                else
                {
                    return "No Mode Selected";
                }
            }
        }

        public bool DoShowLessonLists
        {
            get
            {
                return (DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt)) || (DomainUtilities.CompModeIsShowComparisonLessons(ComparisonModeInt)) || (DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt));
            }
        }

        public bool DoShow_SelectLesson
        {
            get {
                return (DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt)
                  || DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt))
                  && EditableLessonId != Guid.Empty; }
        }

        public bool DoShowDocumentsTab
        {
            get { return !DomainUtilities.CompModeIsViewArchive(ComparisonModeInt); }
        }


        public bool DoShowRemoveNarrativeFromArchive
        {
            get
            {
                if (IsDisplayModeNarrative && ComparisonLessonObj.IsNarrativeSpecified && SavedNarratives != null)
                {
                    return SavedNarratives.Select(x => x.NarrativeId).Contains(ComparisonNarrativeId);
                }
                else
                {
                    return false;
                }
            }
        }
        
        public string NarrativeDiff
        {
            get
            {
                DMPCWMTHelper dMPCWMTHelper = new DMPCWMTHelper();
                return dMPCWMTHelper.PrettyDiffFormatter(ComparisonLessonObj.NarrativeText, EditableLessonObj.NarrativeText);
            }
        }

        public string LessonPlanDiff
        {
            get
            {
                DMPCWMTHelper dMPCWMTHelper = new DMPCWMTHelper();
                return dMPCWMTHelper.PrettyDiffFormatter(ComparisonLessonObj.LessonPlanText, EditableLessonObj.LessonPlanText);
            }
        }
    }
}
