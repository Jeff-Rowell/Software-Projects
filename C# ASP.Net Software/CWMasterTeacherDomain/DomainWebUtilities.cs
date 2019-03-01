using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain
{
    public static class DomainWebUtilities
    {
        private static string _documentPath = "~/App_Data/Docs/";

        private static DateTime _bogusNow = new DateTime(2015, 10, 8, 13, 20, 0);

        private static string _narrativeTypeName = "Narrative";
        private static string _narrativeCommentTypeName = "Comment";
        private static string _lessonPlanTypeName = "Teaching Notes";

        //These are the CSS class constants
        private static string _activeMessageClass = "cw-message-active";
        private static string _newMessageClass = "cw-message-new";
        private static string _importantMessageClass = "cw-message-important";
        private static string _storedMessageClass = "cw-message-stored";
        private static string _activeStoredMessageClass = "cw-message-active-stored";
        private static string _archivedMessageClass = "cw-message-archived";
        private static string _activeMessage_OutForEdit_Class = "cw-message-active-outforedit";
        private static string _newMessage_OutForEdit_Class = "cw-message-new-outforedit";
        private static string _importantMessage_OutForEdit_Class = "cw-message-important-outforedit";
        private static string _storedMessage_OutForEdit_Class = "cw-message-stored-outforedit";
        private static string _activeStoredMessage_OutForEdit_Class = "cw-message-active-stored-outforedit";
        private static string _messageOutForEditClass = "cw-message-outforedit";
        private static string _documentOutForEditClass = "cw-document-outforedit";
        private static string _documentRegularClass = "cw-document";
        private static string _heading5Class = "cw-h5";
        private static string _listSelectedClass_1 = "cw-list-selected-1";
        private static string _listSelectedClass_2 = "cw-list-selected-2";
        private static string _listIsCurrentClass = "cw-bold";
        private static string _isIncludedInPlanClass = "cw-lesson-included";
        private static string _enabledClass = "cw-disabled";
        private static string _upArrowEnabledClass = "cw-arrow-up";
        private static string _rightArrowEnabledClass = "cw-arrow-right";
        private static string _downArrowEnabledClass = "cw-arrow-down";
        private static string _leftArrowEnabledClass = "cw-arrow-left";
        private static string _upSmallArrowEnabledClass = "cw-arrow-up-small";
        private static string _downSmallArrowEnabledClass = "cw-arrow-down-small";
        private static string _upArrowDisabledClass = "cw-arrow-up-disabled";
        private static string _rightArrowDisabledClass = "cw-arrow-right-disabled";
        private static string _downArrowDisabledClass = "cw-arrow-down-disabled";
        private static string _leftArrowDisabledClass = "cw-arrow-left-disabled";
        private static string _upSmallArrowDisabledClass = "cw-arrow-up-small-disabled";
        private static string _downSmallArrowDisabledClass = "cw-arrow-down-small-disabled";
        private static string _weekStartDisplayClass = "cw-h5";
        private static string _classNumberNotStartOfWeekDisplayClass = "cw-space-above-bigbigskip";
        private static string _classNumberStartOfWeekDisplayClass = "cw-space-above-bigskip";
        //private static string _dateIsSelectedClass = "cw-list-selected-1";
        private static string _courseLessonTitleClass = "cw-title";
        private static string _courseLessonTitleMasterClass = "cw-title-master";
        private static string _courseLessonTitleSmallClass = "cw-title-sm";
        private static string _courseLessonTitleSmallMasterClass = "cw-title-sm-master";
        private static string _isFolderClass = "cw-is-folder";
        private static string _classMeetingBorderClass = "cw-border";
        private static string _classMeetingNextBorderClass = "cw-border-ridge";
        private static string _tabButtonSelectedClass = "btn btn-sm cw-tabbutton-selected";
        private static string _tabButtonIdleClass = "btn btn-sm cw-tabbutton-idle";
        private static string _comparisonLessonLinkText = "Show Comparison Lessons";
        private static string _comparisonDocumentsLinkText = "Show Comparison Documents";
        private static string _titleMarginClass = "cw-title-margin";
        private static string _titleMarginNoneBelowClass = "cw-title-margin-nonebelow";
        private static string _minusButtonClass = "cw-minus";
        private static string _plusButtonClass = "cw-plus";



        public static DateTime BogusNow()//Used for testing.
        {
            return _bogusNow;
        }

        public static DateTime AdjustForwardForGMT(DateTime dateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
        }

        public static string UpArrowClass(bool isEnabled)
        {
            if (isEnabled)
            {
                return _upArrowEnabledClass;
            }
            else
            {
                return _upArrowDisabledClass;
            }

        }
        public static string RightArrowClass(bool isEnabled)
        {
            if (isEnabled)
            {
                return _rightArrowEnabledClass;
            }
            else
            {
                return _rightArrowDisabledClass;
            }

        }
        public static string DownArrowClass(bool isEnabled)
        {
            if (isEnabled)
            {
                return _downArrowEnabledClass;
            }
            else
            {
                return _downArrowDisabledClass;
            }

        }
        public static string LeftArrowClass(bool isEnabled)
        {
            if (isEnabled)
            {
                return _leftArrowEnabledClass;
            }
            else
            {
                return _leftArrowDisabledClass;
            }

        }
        public static string UpSmallArrowClass(bool isEnabled)
        {
            if (isEnabled)
            {
                return _upSmallArrowEnabledClass;
            }
            else
            {
                return _upSmallArrowDisabledClass;
            }

        }
        public static string DownSmallArrowClass(bool isEnabled)
        {
            if (isEnabled)
            {
                return _downSmallArrowEnabledClass;
            }
            else
            {
                return _downSmallArrowDisabledClass;
            }

        }

        public static string PlusMinusButtonClass(bool isPlusNotMinus)
        {
            if(!isPlusNotMinus)
            {
                return _minusButtonClass;
            }
            else
            {
                return _plusButtonClass;
            }
        }



        public static string GetMessageAndDocImageCSS(bool hasImportant, bool hasNew, bool hasActive, bool hasActiveStored, bool hasStored,
                                                        bool hasOutForEdit, bool isArchived)
        {
            if(isArchived)
            {
                return "";
            }
            if (hasImportant)
            {
                if (hasOutForEdit)
                {
                    return _importantMessage_OutForEdit_Class;
                }
                else
                {
                    return _importantMessageClass;
                }

            }
            else if (hasNew)
            {
                if (hasOutForEdit)
                {
                    return _newMessage_OutForEdit_Class;
                }
                else
                {
                    return _newMessageClass;
                }
            }
            else if (hasActive)
            {
                if (hasOutForEdit)
                {
                    return _activeMessage_OutForEdit_Class;
                }
                else
                {
                    return _activeMessageClass;
                }
            }
            else if (hasActiveStored)
            {
                if (hasOutForEdit)
                {
                    return _activeStoredMessage_OutForEdit_Class;
                }
                else
                {
                    return _activeStoredMessageClass;
                }
            }
            else if (hasStored)
            {
                if (hasOutForEdit)
                {
                    return _storedMessage_OutForEdit_Class;
                }
                else
                {
                    return _storedMessageClass;
                }
            }
            else
            {
                if (hasOutForEdit)
                {
                    return _messageOutForEditClass;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string GetIncludedInPlanCSS(bool isIncludedInPlan)
        {
            if (isIncludedInPlan)
            {
                return _isIncludedInPlanClass;
            }
            else
            {
                return "";
            }

        }

        public static string GetIsFolderCSS()
        {
            return _isFolderClass;
        }


        public static string GetDocOutForEditCSS(bool isOutForEdit)
        {
            if (isOutForEdit)
            {
                return _documentOutForEditClass;
            }
            else
            {
                return _documentRegularClass;
            }
        }

        public static string GetArchivedMessageCSS(bool isArchived)
        {
            if (isArchived)
            {
                return _archivedMessageClass;
            }
            else
            {
                return "";
            }
        }

        public static string GetHeading5Class(bool isHeading)
        {
            if (isHeading)
            {
                return _heading5Class;
            }
            else
            {
                return "";
            }
        }

        public static string ListSelectedClass(bool isSelected, bool isBlue)
        {
            if (isSelected)
            {
                if (isBlue)
                {
                    return _listSelectedClass_1;
                }
                else
                {
                    return _listSelectedClass_2;
                }
            }
            else
            {
                return "";
            }
        }

        public static string ListCurrentClass(bool isCurrent)
        {
            return isCurrent ? _listIsCurrentClass : "";
        }

        public static DateTime DateString_ToDateTime(string dateString)
        {
            return Convert.ToDateTime(dateString);
        }

        public static string DateTime_ToDateString(DateTime dateTime)
        {
            return String.Format("{0:MMM d, yyyy}", dateTime);
        }

        public static string DateTime_ToLongDateString(DateTime dateTime)
        {
            return String.Format("{0:ddd, MMM d}", dateTime);
        }

        public static DateTime TimeString_ToDateTime(string timeString)
        {
            return Convert.ToDateTime(timeString);
        }

        public static string DateTime_ToTimeString(DateTime dateTime)
        {
            return dateTime.ToString("t");
        }


        public static string EnabledClass(bool useIsSelected)
        {
            if (useIsSelected)
            {
                return "";
            }
            else
            {
                return _enabledClass;
            }
        }

        public static string MeetingDisplayNameClass(bool isStartOfWeek)
        {
            if (isStartOfWeek)
            {
                return _weekStartDisplayClass;
            }
            else
            {
                return "";
            }

        }

        public static string ClassNumberDisplayClass(bool isStartOfWeek)
        {
            if (isStartOfWeek)
            {
                return _classNumberStartOfWeekDisplayClass;
            }
            else
            {
                return _classNumberNotStartOfWeekDisplayClass;
            }
        }


        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public static string CourseLessonTitleClass(bool isMaster, bool isSmall, bool hasNoMargingBelow = false)
        {
            string returnString;
            if (isMaster)
            {
                if (isSmall)
                {
                    returnString = _courseLessonTitleSmallMasterClass;
                }
                else
                {
                    returnString = _courseLessonTitleMasterClass;
                }
            }
            else
            {
                if (isSmall)
                {
                    returnString = _courseLessonTitleSmallClass;
                }
                else
                {
                    returnString = _courseLessonTitleClass;
                }
            }

            if(!hasNoMargingBelow)
            {
                return returnString + " " + _titleMarginClass;
            }
            else
            {
                return returnString + " " + _titleMarginNoneBelowClass;
            }
            

        }

        public static string DocumentPath
        {
            get { return DomainWebUtilities._documentPath; }
            set { DomainWebUtilities._documentPath = value; }
        }

        public static string ClassMeetingBorderClass(bool isNextClass)
        {
            if (isNextClass)
            {
                return _classMeetingNextBorderClass;
            }
            else
            {
                return _classMeetingBorderClass;
            }
        }

        public static string NarrativeTypeName
        {
            get
            {
                return _narrativeTypeName;
            }
        }
        public static string LessonPlanTypeName
        {
            get
            {
                return _lessonPlanTypeName;
            }
        }
        public static string NarrativeCommentTypeName
        {
            get
            {
                return _narrativeCommentTypeName;
            }
        }

        public static string TabButtonCSS(bool isSelected)
        {
            if (isSelected)
            {
                return _tabButtonSelectedClass;
            }
            else
            {
                return _tabButtonIdleClass;
            }
        }

        public static class Roles
        {
            public static string ApprovedInstructor { get { return "ApprovedInstructor"; } }
            public static string Student { get { return "Student"; } }
            public static string Instructor { get { return "Instructor"; } }
            public static string Editor { get { return "Editor"; } }
            public static string WorkingGroupAdmin { get { return "WorkingGroupAdmin"; } }
            public static string ApplicationAdmin { get { return "ApplicationAdmin"; } }
            public static string[] AllRoles { get { return new string[] { ApprovedInstructor, Instructor, Student, Editor, WorkingGroupAdmin, ApplicationAdmin }; } }
        }

        public static class ComparerEditType
        {
            public static string LessonPlan { get { return "LessonPlan"; } }
            public static string Narrative { get { return "Narrative"; } }
            public static string Documents { get { return "Documents"; } }
        }

        public static class TextEditType
        {
            public static string Narrative { get { return "Narrative"; } }
            public static string LessonPlan { get { return "LessonPlan"; } }
            public static string Message { get { return "Message"; } }
            public static string MessageToSelf { get { return "MessageToSelf"; } }
            public static string MessageToSelfStorage { get { return "MessageToSelfStorage"; } }
            public static string CustomLessonPlan { get { return "CustomLessonPlan"; } }
        }

        public static string ComparisonLessonLinkText
        {
            get { return _comparisonLessonLinkText; }
        }

        public static string ComparisonDocumentsLinkText
        {
            get { return _comparisonDocumentsLinkText; }
        }
    }
}
