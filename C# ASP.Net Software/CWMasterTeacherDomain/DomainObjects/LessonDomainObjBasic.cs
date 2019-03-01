using System;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class LessonDomainObjBasic
    {

        public Guid Id { get; set; }

        public string Name { get; set; }
        public bool IsMaster { get; set; }
        public bool IsHidden { get; set; }
        public bool IsCollapsed { get; set; }
        public int ActiveDocumentCount { get; set; }
        public Guid CourseId { get; set; }
        public string UserDisplayName { get; set; }
        public bool DoShowLessonPlanNotifications { get; set; }
        public bool DoShowNarrativeNotifications { get; set; }
        public bool DoShowDocumentsNotifications { get; set; }

        public Guid NarrativeId { get; set; }

        public int NarrativeTextLength { get; set; }

        public Guid LessonPlanId { get; set; }

        public string DisplayName
        {
            get
            {
                string displayName = "";
                if (IsCollapsed)
                {
                    displayName = Name + "  >>[]<<";
                }
                else
                {
                    displayName = Name;
                }
                if (HaveChangesUserWantsToKnowAbout)
                {
                    displayName = displayName + "*";
                }
                return displayName;
            }
        }

        private bool HaveChangesUserWantsToKnowAbout  
        {
            get
            {
                return (LessonHasGroupChanges && DoShowLessonPlanNotifications)
                        || (NarrativeHasNewChangesInGroup && DoShowNarrativeNotifications)
                        || (DocumentsHaveNewChangesinGroup && DoShowDocumentsNotifications);
            }
        }


        public string DisplayNameForHeading
        {
            get
            {
                if(IsMaster)
                {
                    return Name + " (Master)";
                }
                else
                {
                    return Name;
                }
            }
        }

        public string ShortExtendedDisplayName
        {
            get
            {
                if (Id != Guid.Empty)
                {
                    string prefix = IsMaster ? "Master " : UserDisplayName + "'s ";
                    return prefix + Name;
                }
                else
                {
                    return "_";
                }

            }

        }

        //These are needed to set tab text in Curriculum
        public DateTime NarrativeReferenceDateChoiceConfirmed { get; set; }
        public DateTime NarrativeDateChoiceConfirmed { get; set; }
        public DateTime NarrativeDateModified { get; set; }
        public DateTime MasterNarrativeDateModified { get; set; }
        //public Guid MasterNarrativeId { get; set; }
        public DateTime NarrativeGroupMostRecentModDate { get; set; }
        public bool NarrativeHasNewChangesInGroup
        {
            get { return DomainUtilities.Date1_IsGreater(NarrativeGroupMostRecentModDate, NarrativeDateChoiceConfirmed); }
        }
        public bool NarrativeHasNewChangesInMaster
        {
            get { return DomainUtilities.Date1_IsGreater(MasterNarrativeDateModified, NarrativeDateChoiceConfirmed); }
        }

        /// <summary>
        /// This is used when moving the comparison date back in time.  This stores the value that can be returned to.
        /// </summary>
        public DateTime LessonPlanReferenceDateChoiceConfirmed { get; set; }
        public DateTime LessonPlanDateModified { get; set; }
        public DateTime MasterLessonPlanDateModified { get; set; }
        public DateTime LessonPlanGroupMostRecentModDate { get; set; }
        //public Guid MasterLessonPlanId { get; set; }
        public DateTime LessonPlanDateChoiceConfirmed { get; set; }
        public bool LessonPlanHasNewChangesInGroup
        {
            get { return DomainUtilities.Date1_IsGreater(LessonPlanGroupMostRecentModDate, LessonPlanDateChoiceConfirmed); }
        }
        public bool LessonPlanHasNewChangesInMaster
        {
            get { return DomainUtilities.Date1_IsGreater(MasterLessonPlanDateModified , LessonPlanDateChoiceConfirmed); }
        }

        /// <summary>
        /// This is used when moving the comparison date back in time.  This stores the value that can be returned to.
        /// </summary>
        public DateTime DocumentReferenceDateChoiceConfirmed { get; set; }
        public DateTime DocumentsDateModified { get; set; }
        public DateTime MasterDocumentsDateModified { get; set; }
        public DateTime DocumentsDateChoiceConfirmed {get; set; }
        public DateTime DocumentsGroupMostRecentModDate {get; set; }
        public bool DocumentsHaveNewChangesinGroup
        {
            get { return DomainUtilities.Date1_IsGreater(DocumentsGroupMostRecentModDate, DocumentsDateChoiceConfirmed);}
        }
        public bool DocumentsHaveNewChangesInMaster
        {
            get { return DomainUtilities.Date1_IsGreater(MasterDocumentsDateModified, DocumentsDateChoiceConfirmed); }
        }

        public bool LessonHasGroupChanges
        {
            get { return DocumentsHaveNewChangesinGroup || LessonPlanHasNewChangesInGroup || NarrativeHasNewChangesInGroup; }
        }

        public string FullNarrative { get; set; }
    }
}
