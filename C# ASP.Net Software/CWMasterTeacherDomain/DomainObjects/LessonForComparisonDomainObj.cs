using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain
{
    public class LessonForComparisonDomainObj : IDomainObj 
    {
        public LessonForComparisonDomainObj(LessonDomainObjBasic basicObj)
        {
            this.LessonObjBasic = basicObj;
        }

        public LessonDomainObjBasic LessonObjBasic { get; private set; }
        public LessonDomainObjBasic EditableLessonObj { get; set; }
        public bool IsActive { get; set; }
        public string ChangeNotes { get; set; }
        public LessonPlanDomainObj LessonPlanObj { get; set; }
        public NarrativeDomainObj NarrativeObj { get; set; }

        public string CustomNarrativeText { get; set; }
        public bool IsNarrativeSpecified { get; set; }

        public DateTime DocumentsModifiedDate { get; set; }
        public string UserName { get; set; }

        public List<StashedLessonPlanDomainObj> StashedLessonPlans { private get; set; }
        public List<StashedNarrativeDomainObj> StashedNarratives { private get; set; }

        public DateTime? DateTimeLessonPlanChoiceConfirmed { get; set; }
        /// <summary>
        /// This is used when moving the comparison date back in time.  This stores the value that can be returned to.
        /// </summary>
        public DateTime? ReferenceDateTimeLessonPlanChoiceConfirmed { get; set; }
        /// <summary>
        /// Used to determine whether new comparison Lessons are available.
        /// </summary>
        public DateTime? GroupMostRecentLessonPlanModDate { get; set; }

        public Guid MetaLessonId { get; set; }

        public string NarrativeText
        {
            get { return NarrativeObj == null ? CustomNarrativeText : NarrativeObj.Text; }
        }

        public List<StashedLessonPlanDomainObj> HistoryLessonPlans
        {
            get { return StashedLessonPlans == null ? new List<StashedLessonPlanDomainObj> () : StashedLessonPlans.Where(x => !x.IsSaved).OrderBy(x => x.DateModified).ToList(); }
        }
        public List<StashedLessonPlanDomainObj> SavedLessonPlans
        {
            get { return StashedLessonPlans == null ? new List<StashedLessonPlanDomainObj> () : StashedLessonPlans.Where(x => x.IsSaved).OrderBy(x => x.DateModified).ToList(); }
        }

        public List<StashedNarrativeDomainObj> HistoryNarratives
        {
            get { return StashedNarratives == null ? new List<StashedNarrativeDomainObj>() : StashedNarratives.Where(x => !x.IsSaved).OrderBy(x => x.DateModified).ToList(); }
        }
        public List<StashedNarrativeDomainObj> SavedNarratives
        {
            get { return StashedNarratives == null ? new List<StashedNarrativeDomainObj>() : StashedNarratives.Where(x => x.IsSaved).OrderBy(x => x.DateModified).ToList(); }
        }

        public Guid Id
        {
            get { return this.LessonObjBasic.Id; }
        }

        public Guid MasterLessonId { get; set; }


        public string Name
        {
            get
            {
                return this.LessonObjBasic.Name;
            }
        }

        public bool IsCollapsed
        {
            get
            {
                return this.LessonObjBasic.IsCollapsed;

            }
        }

        public bool IsHidden
        {
            get { return this.LessonObjBasic.IsHidden; }
        }

        public bool IsMaster
        {
            get
            {
                return this.LessonObjBasic.IsMaster;
            }
        }
        public string DisplayName
        {
            get
            {
                return this.LessonObjBasic.DisplayName;
            }
        }
   

        public string LessonPlanText
        {
            get { return LessonPlanObj == null ? "_" : LessonPlanObj.Text; }
            set { LessonPlanObj.Text = value; }
        }

        public DateTime OldestDateModified
        {
            get
            {
                List<DateTime?> dateList = new List<DateTime?>();
                dateList.Add(LessonPlanModifiedDate);
                dateList.Add(NarrativeModifiedDate);
                dateList.Add(DocumentsModifiedDate);
                return dateList.Min(x => x.Value);
            }
        }

        public string ShortExtendedDisplayName
        {
            get
            {
                if(Id != Guid.Empty)
                {
                    string prefix = IsMaster ? "Master " : UserName + "'s ";
                    string name = prefix + Name;
                    name = IsCollapsed ? name + " >>Collapsed<<" : name;
                    name = IsHidden ? name + "_(Hidden)" : name;
                    return name;
                }
                else
                {
                    return "_";
                }

            }

        }


        public bool IsEmpty
        {
            get { return Id == Guid.Empty; }
        }

        
        public DateTime ReferenceDateDocChoiceConfirmed { get; set; }

        public List<DocumentUseDomainObj> TeachingDocumentUses { get; set; }
        public List<DocumentUseDomainObj> InstructorOnlyDocumentUses { get; set; }
        public List<DocumentUseDomainObj> ReferenceDocumentUses { get; set; }
        public List<DocumentUseDomainObj> AllDocumentUses
        {
            get
            { 
                if(TeachingDocumentUses != null && InstructorOnlyDocumentUses != null && ReferenceDocumentUses != null)
                {
                    return TeachingDocumentUses.Concat(InstructorOnlyDocumentUses).Concat(ReferenceDocumentUses).ToList();
                }
                else
                {
                    return new List<DocumentUseDomainObj>();
                }
            }

        }

        public void SetAllSelectedDocumentUseIds(Guid selectedDocumentUseId)
        {
            if(TeachingDocumentUses != null)
            {
                SetSelectedDocUseIds(TeachingDocumentUses, selectedDocumentUseId);
            }
            if (InstructorOnlyDocumentUses != null)
            {
                SetSelectedDocUseIds(InstructorOnlyDocumentUses, selectedDocumentUseId);
            }
            if (ReferenceDocumentUses != null)
            {
                SetSelectedDocUseIds(ReferenceDocumentUses, selectedDocumentUseId);
            }
        }

        private void SetSelectedDocUseIds(List<DocumentUseDomainObj> docUseList, Guid selectedDocUseId)
        {
            foreach(var xDocUse in docUseList)
            {
                xDocUse.SelectedDocumentUseId = selectedDocUseId;
            }
        }


        /// <summary>
        /// I'm doing it this way, with the double "foreach" loop, because I might want to add a DateChangesAccepted to the DocumentUseObject later on and this will support comparing that.
        /// </summary>
        /// <param name="docUseList"></param>
        /// <param name="editableDocUseList"></param>
        public void SetDocumentComparisonBooleans(List<DocumentUseDomainObj> editableDocUseList, DateTime dateChangesAccepted)
        {
            List<DocumentUseDomainObj> docUseList = AllDocumentUses;
            foreach (var xDocUse in docUseList)
            {
                bool isUsed = false;
                bool hasBeenUpdated = false;
                foreach(var xEditableDocUse in editableDocUseList)
                {
                    if(xEditableDocUse.DocumentId == xDocUse.DocumentId ||
                        (xEditableDocUse.DocumentId != Guid.Empty && xEditableDocUse.DocumentId == xDocUse.Document.OriginalDocumentId )||
                        (xEditableDocUse.Document.OriginalDocumentId != Guid.Empty && xEditableDocUse.Document.OriginalDocumentId == xDocUse.Document.OriginalDocumentId) )
                    {
                        isUsed = true;
                        if(xDocUse.Document.DateModified > dateChangesAccepted)
                        {
                            hasBeenUpdated = true;
                        }
                    }
                }
                xDocUse.IsUsedInEditableLesson = isUsed;
                xDocUse.HasBeenUpdated = hasBeenUpdated;
            }
        }


        //********************
        public Guid LessonPlanId
        {
            get { return LessonPlanObj == null ? Guid.Empty : LessonPlanObj.Id; }
        }

        public DateTime LessonPlanModifiedDate
        {
            get { return LessonPlanObj == null ? new DateTime() : LessonPlanObj.DateModified; }
        }

        public DateTime LessonPlanCreatedDate
        {
            get { return LessonPlanObj == null ? new DateTime() : LessonPlanObj.DateCreated; }
        }

        public DateTime LessonPlanReferenceDateChoiceConfirmed
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.LessonPlanReferenceDateChoiceConfirmed; }
        }

        public DateTime LessonPlanDateChoiceConfirmed
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.LessonPlanDateChoiceConfirmed; }
        }

        public bool LessonPlanHasNewChanges
        {
            get { return LessonObjBasic == null ? false : LessonObjBasic.LessonPlanHasNewChangesInGroup; }
        }

        public DateTime NarrativeModifiedDate
        {
            get { return NarrativeObj == null ? new DateTime() : NarrativeObj.DateModified; }
        }

        public int NarrativeTextLength
        {
            get { return NarrativeObj == null ? 0 : NarrativeObj.Text.Length; }
        }

        public DateTime NarrativeReferenceDateChoiceConfirmed
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.NarrativeReferenceDateChoiceConfirmed; }
        }

        public DateTime NarrativeDateChoiceConfirmed
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.NarrativeDateChoiceConfirmed; }
        }

        public bool NarrativeHasNewChanges
        {
            get { return LessonObjBasic == null ? false : LessonObjBasic.NarrativeHasNewChangesInGroup; }
        }

        public DateTime DateDocumentsModified
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.DocumentsDateModified; }
        }
        public DateTime DocumentsDateChoiceConfirmed
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.DocumentsDateChoiceConfirmed; }
        }

        public bool HasComparisonDocumentsAvailable
        {
            get { return LessonObjBasic == null ? false : LessonObjBasic.DocumentsHaveNewChangesinGroup; }
        }
        //*****************************

        /// <summary>
        /// This is used in identifying lessons that have more recent modifications than a given lesson.This is the date the editable lesson was modified.
        /// </summary>
        public DateTime NarrativeEditableDateConfirmed
        {
            get { return EditableLessonObj == null ? new DateTime() : EditableLessonObj.NarrativeDateChoiceConfirmed; }
        }

        public bool NarrativeHasNewChangesRelToGivenLesson
        {
            get { return DomainUtilities.Date1_IsGreater(NarrativeModifiedDate, NarrativeEditableDateConfirmed)
                                        && Id != EditableLessonId && NarrativeTextLength > 3; }
        }


        /// <summary>
        /// This is used in identifying lessons that have more recent modifications than a given lesson.  This is the date the editable lesson was modified.
        /// </summary>
        public DateTime LessonPlanEditableDateConfirmed
        {
            get { return EditableLessonObj == null ? new DateTime() : EditableLessonObj.LessonPlanDateChoiceConfirmed; }
        }

        public bool LessonPlanHasNewChangesRelToGivenLesson
        {
            get { return DomainUtilities.Date1_IsGreater(LessonPlanModifiedDate, LessonPlanEditableDateConfirmed)
                                            && Id != EditableLessonId;
            }
        }


        /// <summary>
        /// This is used in identifying lessons that have more recent modifications than a given lesson.  This is the date the editable lesson was modified.
        /// </summary>
        public DateTime DocumentsEditableDateConfirmed
        {
            get { return EditableLessonObj == null ? new DateTime() : EditableLessonObj.DocumentsDateChoiceConfirmed; }
        }
        /// <summary>
        /// The modification date for the Documents for this particular lesson.
        /// </summary>
        public bool DocumentsHaveNewChangesRelToGivenLesson
        {
            get { bool returnVal = DomainUtilities.Date1_IsGreater(DocumentsModifiedDate, DocumentsEditableDateConfirmed)
                    && Id != EditableLessonId;
                return returnVal;
            }
        }
        public bool LessonHasNewChangesRelToGivenLesson
        {
            get { return DocumentsHaveNewChangesRelToGivenLesson || LessonHasNewChangesRelToGivenLesson || NarrativeHasNewChangesRelToGivenLesson; }
        }

        /// <summary>
        /// This used in whether this lesson has new changes relative to editable lesson.
        /// </summary>
        public Guid EditableLessonId
        {
            get { return EditableLessonObj == null ? Guid.Empty : EditableLessonObj.Id; }
        }

        /// <summary>
        /// This used in whether this lesson has new changes relative to editable lesson.
        /// </summary>
        public Guid EditableNarrativeId
        {
            get { return EditableLessonObj == null ? Guid.Empty : EditableLessonObj.NarrativeId; }
        }

        /// <summary>
        /// This used in whether this lesson has new changes relative to editable lesson.
        /// </summary>
        public Guid EditableLessonPlanId
        {
            get { return EditableLessonObj == null ? Guid.Empty : EditableLessonObj.LessonPlanId; }
        }


        //*****************************

        public string LessonIdString
        {
            get { return Id.ToString(); }
        }
    }
}
