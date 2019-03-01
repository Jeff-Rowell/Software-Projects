using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class CurriculumViewObj
    {

        private List<CourseDomainObjBasic> _courseObjList;

        //These are just pass-throughs
        public bool ShowGroupDocStorage { get; set; }
        public string FileValidationMessage { get; set; }
        public bool IsPdfCopyUpload { get; set; }
        public Guid DisplayMessageUseId { get; set; }
        public bool ShowArchivedMessages { get; set; }
        public Guid SelectedDocumentUseId { get; set; }
        public Guid SelectedDocumentId { get; set; }
        public bool ShowUploadDialog { get; set; }
        public bool ShowArchivedDocs { get; set; }
        public int ShowThisManyDocs { get; set; }



        //These are more than just pass-throughs.
        public CourseDomainObj CourseObj { get; set; }
        public LessonDomainObjBasic LessonObj { get; set; }
        public UserDomainObj UserObj { get; set; }
        public TermDomainObj CurrentTermObj { get; set; }

        public Guid ReturnLessonId { get; set; }

        public Guid SelectedCourseId
        {
            get
            {
                return CourseObj == null ? Guid.Empty : CourseObj.Id;
            }
        }

        [Display(Name = "Show Hidden Lessons")]
        public bool DoShowHiddenLessons
        {
           get { return CourseObj == null ? false : CourseObj.DoShowHiddenLessons; }
        }

        public string DoShowHiddenLessonsLinkText
        {
            get { return DoShowHiddenLessons ? "Hide Hidden Lessons" : "Show Hidden Lessons"; }
        }

        public string HideLessonLinkText
        {
            get
            {
                if (LessonObj != null)
                {
                    return LessonObj.IsHidden ? "Unhide Selected Lesson" : "Hide Selected Lesson";
                }
                else
                {
                    return "";
                }

            }
        }

        public Guid SelectedLessonId
        {
            get
            {
                return LessonObj == null ? Guid.Empty : LessonObj.Id;
            }
        }

        public string SelectedLessonName
        {
            get { return LessonObj == null ? "" : LessonObj.DisplayNameForHeading; }
        }

        public string CollapseLinkText
        {
            get
            {
                if (LessonObj != null && LessonObj.IsCollapsed)
                {
                    return "Expand Lesson";
                }
                else
                {
                    return "Collapse Lesson";
                }
            }
        }
        public bool ShowLessonCollapseLink
        {
            get { return LessonObj != null; }
        }
        public string LessonCollapseButtonCSS
        {
            get { return DomainWebUtilities.PlusMinusButtonClass(LessonObj == null || LessonObj.IsCollapsed); }
        }


        public bool ShowMasters
        {
            get
            {
                return UserObj == null ? false : UserObj.ShowMastersInCourseList;
            }
        }
        public bool UserIsEditor
        {
            get
            {
                return UserObj == null ? false : UserObj.IsMasterEditor;
            }
        }
        public bool UserIsApplicationAdmin
        {
            get
            {
                return UserObj == null ? false : UserObj.IsApplicationAdmin;
            }
        }
        public bool UserIsWorkingGroupAdmin
        {
            get
            {
                return UserObj == null ? false : UserObj.IsWorkingGroupAdmin;
            }
        }
        public bool ShowAllCourses
        {
            get
            {
                return UserObj == null ? false : UserObj.ShowAllInCourseList;
            }
        }

        public bool IsMasterEdit
        {
            get
            {
                if (LessonObj != null)
                {
                    return LessonObj.IsMaster;
                }
                else
                {
                    return CourseObj == null ? false : CourseObj.IsMaster;
                }

            }
        }
        public string MasterButtonText
        {
            get
            {
                if (IsMasterEdit && ReturnLessonId != Guid.Empty)
                {
                    
                    return "Close Master";
                }
                else if (!IsMasterEdit)
                {
                    return "Open Master";
                }
                else
                {
                    return "_";
                }
            }
        }


        public string TitleCSS
        {
            get
            {
                return DomainWebUtilities.CourseLessonTitleClass(IsMasterEdit, false);
            }
        }
        public string SubtitleCSS
        {
            get
            {
                return DomainWebUtilities.CourseLessonTitleClass(IsMasterEdit, true);
            }
        }

        public List<CourseDomainObjBasic> CourseObjList
        {
            get
            {
                bool showMasters = UserObj == null ? false : UserObj.ShowMastersInCourseList;

                var query = from x in _courseObjList
                            where x.IsMaster == showMasters
                            select x;
                List<CourseDomainObjBasic> filteredList = query.ToList();

                if (UserObj == null || UserObj.ShowAllInCourseList || CurrentTermObj == null || CurrentTermObj.Id == Guid.Empty)
                {
                    return filteredList;
                }
                else
                {
                    var query2 = from x in filteredList
                                 where x.TermId == CurrentTermObj.Id
                                 select x;
                    return query2.ToList();
                }
            }
            set
            {
                _courseObjList = value;
            }
        }

        public string CourseListHeading
        {
            get
            {
                if (UserObj.ShowMastersInCourseList)
                {
                    if (UserObj.ShowAllInCourseList)
                    {
                        return "All Masters";
                    }
                    else
                    {
                        return CurrentTermObj.Name + " Masters";
                    }
                }
                else
                {
                    if (UserObj.ShowAllInCourseList || CurrentTermObj == null)
                    {
                        return "All Courses_" + UserObj.DisplayName;
                    }
                    else
                    {
                        return CurrentTermObj.Name + "_" + UserObj.DisplayName;
                    }
                }
            }
        }

        public string CoursesButtonText
        {
            get
            {
                if (UserObj.ShowMastersInCourseList)
                {
                    if (UserObj.ShowAllInCourseList)
                    {
                        return "All Masters";
                    }
                    else
                    {
                        return "Current Masters";
                    }
                }
                else
                {
                    if (UserObj.ShowAllInCourseList)
                    {
                        return "All My Courses";
                    }
                    else
                    {
                        return "My Current Courses)";
                    }
                }
            }
        }


        public string ShowFoldersDisplayText
        {
            get
            {
                if (CourseObj != null && CourseObj.ShowFolders)
                {
                    return "Hide Folder Contents";
                }
                else
                {
                    return "Show Folder Contents";
                }
            }
        }
        public string ShowOptionalLessonsDisplayText
        {
            get
            {
                if (CourseObj != null && CourseObj.ShowOptionalLessons)
                {
                    return "Hide Optional Lessons";
                }
                else
                {
                    return "Show Optional Lessons";
                }
            }
        }

        public bool ShowToggleMasterListItem
        {
            get
            {
                return (ReturnLessonId != Guid.Empty || !IsMasterEdit) && HasMasterEditRights;
            }
        }

        public string NarrativeTabText
        {
            get
            {
                string tabText = DomainWebUtilities.NarrativeTypeName;
                if (LessonObj != null && LessonObj.NarrativeHasNewChangesInGroup)
                {
                    tabText = tabText + "*";
                }
                return tabText;
            }
        }

        public string LessonPlanTabText
        {
            get
            {
                string tabText = DomainWebUtilities.LessonPlanTypeName;
                if (LessonObj != null && LessonObj.LessonPlanHasNewChangesInGroup)
                {
                    tabText = tabText + "*";
                }
                return tabText;
            }
        }

        public string DocumentsTabText
        {
            get
            {
                string tabText = "";
                if (LessonObj == null || LessonObj.ActiveDocumentCount < 1)
                {
                    tabText = "Documents";
                }
                else
                {
                    tabText = "Documents (" + LessonObj.ActiveDocumentCount.ToString() + ")";
                }

                if (LessonObj != null && LessonObj.DocumentsHaveNewChangesinGroup)
                {
                    tabText = tabText + "*";
                }

                return tabText;
            }
        }

        public bool SelectedLessonHasComparisonDocs
        {
            get { return LessonObj == null ? false : LessonObj.DocumentsHaveNewChangesinGroup & DoShowDocumentNotifications; }
        }


        /// <summary>
        /// Deprecated. Originally related to mirrors.  We may remove this later, but for now it's just "true".
        /// </summary>
        public bool HasBasicEditRights
        {
            get { return true; }
        }

        public bool HasMasterEditRights
        {
            get
            {
                if (UserObj != null && UserObj.IsMasterEditor)
                {
                    return true;
                }
                else if (CourseObj != null)
                {
                    return CourseObj.HasCustomMasterEditRights;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DoShowNarrativeNotifications
        {
            get { return CourseObj == null ? false : CourseObj.DoShowNarrativeNotifications; }
        }
        public bool DoShowLessonPlanNotifications
        {
            get { return CourseObj == null ? false : CourseObj.DoShowLessonPlanNotifications; }
        }
        public bool DoShowDocumentNotifications
        {
            get { return CourseObj == null ? false : CourseObj.DoShowDocumentNotifications; }
        }


    }//End class
}
