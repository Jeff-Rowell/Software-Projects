using System;
using System.Collections.Generic;
using System.Linq;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class LessonDomainObj
    {
        private bool _isForLessonManager;
        private List<LessonDomainObj> _containerChildren;
        private Guid _selectedLessonId;

        public LessonDomainObj(LessonDomainObjBasic basic)
        {
            this.LessonObjBasic = basic;
        }

        public LessonDomainObjBasic LessonObjBasic { get; private set; }
        public LessonDomainObjBasic EditableLessonObj { get; set; }
        public Guid MasterLessonId { get; set; }
        public bool IsActive { get; set; }
        public Guid? ContainerLessonId { get; set; }
        public Guid? PredecessorLessonId { get; set; }
        public int? EstimatedTimeMin { get; set; }
        public int? SequenceNumber { get; set; }
        public bool IsFolder { get; set; }
        public string ChangeNotes { get; set; }
        public bool HasActiveMessages { get; set; }
        public bool HasActiveStoredMessages { get; set; }
        public bool HasStoredMessages { get; set; }
        public bool HasImportantMessages { get; set; }
        public bool HasNewMessages { get; set; }
        public bool HasOutForEditDocuments { get; set; }
        public string NarrativeForEditorText { get; set; }
        public bool IsForCompareSelect { get; set; }
        public Guid MirrorTargetCourseId { get; set; }
        public bool HasCustomMasterEditRights { get; set; }
        public Guid MasterCourseId { get; set; }
        public Guid WorkingGroupId { get; set; }
        public int ComparisonModeInt { get; set; }
        public Guid MetaLessonId { get; set; }
        public bool IsHidden { get; set; }

        /// <summary>
        /// This is used to determine whether this Lesson is selected so we can set the CSS.
        /// </summary>
        public Guid SelectedLessonId
        {
            get
            {
                return _selectedLessonId;
            }
            set
            {
                _selectedLessonId = value;
                foreach(var xLessonObj in ContainerChildren)
                {
                    xLessonObj.SelectedLessonId = value;
                }
            }
        }

        public List<LessonDomainObj> ContainerChildren
        {
            get
            {
                return _containerChildren.OrderBy(x => x.SequenceNumber).ToList();
            }
            set
            {
                _containerChildren = value;
            }
        }


        public Guid Id
        {
            get { return this.LessonObjBasic.Id; }
        }

        public string Name
        {
            get
            {
                return this.LessonObjBasic.Name;
            }
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
                string displayName = LessonObjBasic.DisplayName;
                if (IsHidden)
                {
                    displayName = displayName + "__(Hidden)";
                }
                return displayName;
            }
        }

        public bool IsSelected
        {
            get { return Id == SelectedLessonId; }
        }

        public string SelectedClass
        {
            get
            {
                return DomainWebUtilities.ListSelectedClass(IsSelected, true); 
            }
        }



        public string IconClass
        {
            get
            {
                if (IsForPlanning) //Which means this is for planning
                {
                    return DomainWebUtilities.GetIncludedInPlanCSS(IsUsedInPlan);
                }
                else if (IsFolder)
                {
                    return DomainWebUtilities.GetIsFolderCSS();
                }
                else if (!IsForLessonManager && !IsForCompareSelect ) //Which means this is for the curriculum view and not for the LessonManager and not for CompareSelect
                {
                    return DomainWebUtilities.GetMessageAndDocImageCSS(HasImportantMessages, HasNewMessages,
                                                        HasActiveMessages, HasActiveStoredMessages, HasStoredMessages, 
                                                        HasOutForEditDocuments, false);
                }
                else
                {
                    return "";
                }
            }
        }

        public string DisplayClass
        {
            get
            {
                return IconClass + " " + SelectedClass;
            }
        }

        public bool IsForPlanning { get; set; }


        public bool IsForLessonManager
        {
            get
            {
                return _isForLessonManager;
            }

            set
            {
                _isForLessonManager = value;
                foreach (var xLessonObj in this.ContainerChildren)
                {
                    xLessonObj.IsForLessonManager = value;
                }
            }
        }

        public int ActiveDocumentCount
        {
            get
            {
                return LessonObjBasic.ActiveDocumentCount;
            }
            set
            {
                LessonObjBasic.ActiveDocumentCount = value;
            }
        }

        public Guid CourseId
        {
            get { return LessonObjBasic.CourseId; }
            set { LessonObjBasic.CourseId = value; }
        }

        public string FullNarrative
        {
            get { return LessonObjBasic == null ? "" : LessonObjBasic.FullNarrative; }
            set { LessonObjBasic.FullNarrative = value; }
        }

        public string DisplayNameWithNumber
        {
            get { return "(" + SequenceNumber.ToString() + ") " + DisplayName; }
        }

        /// <summary>
        /// Set in DailyPlanningService
        /// </summary>
        public bool IsUsedInPlan { get; set; }

        public void SetIsUsedInPlanBoolean(HashSet<Guid> usedLessonIdSet)
        {
            IsForPlanning = true;
            IsUsedInPlan = usedLessonIdSet.Contains(Id);
            foreach(var xLesson in ContainerChildren)
            {
                xLesson.SetIsUsedInPlanBoolean(usedLessonIdSet);
            }
        }

        public string NameForCompareSelect
        {
            get
            {

                string hiddenSuffix = "";
                string refCourseSuffix = "";
                string changeSuffix = "";
                if (DoShowIsHiddenInName)
                {
                    hiddenSuffix = this.LessonObjBasic.IsHidden ? " (Hidden) " : "";

                }
                if (DoShow_IsNotInReferenceCourse_InName)
                {
                    refCourseSuffix = this.IsUsedInReferenceCourse ? "" : " ***Not In Current Course*** ";
                }
                if (DoShowComparisonsInName)
                {
                    if (NarrativeHasChangesForLessonCompare)
                    {
                        changeSuffix = changeSuffix + "Narrative,";
                    }

                    if (LessonPlanHasChangesForLessonCompare)
                    {
                        changeSuffix = changeSuffix + " " + DomainWebUtilities.LessonPlanTypeName + ",";
                    }

                    if (DocumentsHaveChangesForLessonCompare)
                    {
                        changeSuffix = changeSuffix + " Documents";
                    }
                }

                if(changeSuffix.Length > 3)
                {
                    changeSuffix = "___(" + changeSuffix + ")";
                }
                return Name + hiddenSuffix + refCourseSuffix + changeSuffix ;

            }
        }

        private bool NarrativeHasChangesForLessonCompare
        {
            get
            {
                if (DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt))
                {
                    return EditableLessonObj == null ? false : EditableLessonObj.NarrativeHasNewChangesInMaster;
                }
                if (DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt))
                {
                    return EditableLessonObj == null ? false : EditableLessonObj.NarrativeHasNewChangesInGroup;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool LessonPlanHasChangesForLessonCompare
        {
            get
            {
                if (DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt))
                {
                    return EditableLessonObj == null ? false : EditableLessonObj.LessonPlanHasNewChangesInMaster;
                }
                if (DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt))
                {
                    return EditableLessonObj == null ? false : EditableLessonObj.LessonPlanHasNewChangesInGroup;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool DocumentsHaveChangesForLessonCompare
        {
            get
            {
                if (DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt))
                {
                    return EditableLessonObj == null ? false : EditableLessonObj.DocumentsHaveNewChangesInMaster;
                }
                if (DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt))
                {
                    return EditableLessonObj == null ? false : EditableLessonObj.DocumentsHaveNewChangesinGroup;
                }
                else
                {
                    return false;
                }
            }
        }



        public bool IsCollapsed
        {
            get { return LessonObjBasic == null ? false : LessonObjBasic.IsCollapsed; }
        }

        //********************

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

        public DateTime LessonPlanGroupMostRecentDocModDate
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.LessonPlanGroupMostRecentModDate; }
        }

        public DateTime NarrativeGroupMostRecentModDate
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.NarrativeGroupMostRecentModDate; }
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
        public DateTime DateDocumentsChoiceConfirmed
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.DocumentsDateChoiceConfirmed; }
        }

        public DateTime DocumentsGroupMostRecentModDate
        {
            get { return LessonObjBasic == null ? new DateTime() : LessonObjBasic.DocumentsGroupMostRecentModDate; }
        }

        public bool HasComparisonDocumentsAvailable
        {
            get { return LessonObjBasic == null ? false : LessonObjBasic.DocumentsHaveNewChangesinGroup; }
        }

        public Guid NarrativeId
        {
            get { return LessonObjBasic == null ? Guid.Empty : LessonObjBasic.NarrativeId; }
        }

        public Guid LessonPlanId
        {
            get { return LessonObjBasic == null ? Guid.Empty : LessonObjBasic.LessonPlanId; }
        }

        //*******************************************


        private bool IsUsedInReferenceCourse
        {
            get { return EditableLessonObj != null; }
        }

        public bool DoShowComparisonsInName
        {
            get { return DomainUtilities.CompModeIsUpdateFromGroup(ComparisonModeInt) 
                                || DomainUtilities.CompModeIsUpdateFromMaster(ComparisonModeInt); }
        }


        /// <summary>
        /// I know this looks silly but I might want to change this to be conditional later.
        /// </summary>
        public bool DoShowIsHiddenInName
        {
            get { return true; }
        }

        /// <summary>
        /// I know this looks silly but I might want to change this to be conditional later.
        /// </summary>
        public bool DoShow_IsNotInReferenceCourse_InName
        {
            get { return true; }
        }

        public bool IsForTesting
        {
            get { return true; }
        }

        public string SuffixForTesting
        {
            get
            {
                string testSuffix = "";
                if (NarrativeHasChangesForLessonCompare)
                {
                    testSuffix = testSuffix + "Comments,";
                }

                if (LessonPlanHasChangesForLessonCompare)
                {
                    testSuffix = testSuffix + " " + DomainWebUtilities.LessonPlanTypeName + ",";
                }

                if (DocumentsHaveChangesForLessonCompare)
                {
                    testSuffix = testSuffix + " Documents";
                }

                return testSuffix;
            }
        }





    }//End Class
}

