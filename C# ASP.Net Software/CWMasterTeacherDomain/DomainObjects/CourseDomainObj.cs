using System;
using System.Collections.Generic;
using System.Linq;

namespace CWMasterTeacherDomain.DomainObjects
{
    /// <summary>
    /// CourseDomainObj abstracts the dependencies of the db from the rest of the application
    /// by taking relevant information from the Course DbObject and storing it in the Domain object
    /// </summary>
    public class CourseDomainObj
    {
        //This gets passed down to Lessons to set CSS
        private Guid _selectedLessonId;
        private bool _isForLessonManager;

        public CourseDomainObjBasic CourseDomainObjBasic { get; set; }

        public bool ShowFolders { get; set; }
        public bool ShowOptionalLessons { get; set; }
        public Guid LastDisplayedLessonId { get; set; }
        public Guid WorkingGroupId { get; set; }
        public Guid? MasterCourseId { get; set; }
        public Guid? PredecessorCourseId { get; set; }
        public Guid? LastDisplayedClassSectionId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
        public string UploadPath { get; set; }
        public string DownloadPath { get; set; }
        public List<LessonDomainObj> ContainerChildLessons{ get; set; }
        public bool HasCustomMasterEditRights { get; set; }
        public CoursePreferenceDomainObjBasic PreferencesObj { get; set; }
        public string MirrorTargetUserDisplayName { get; set; }
        public Guid MetaCourseId { get; set; }
        public bool DoShowHiddenLessons { get; set; }
        
        public Guid Id
        {
            get { return CourseDomainObjBasic.Id; }
        } 
        public Guid CourseId
        {
            get { return CourseDomainObjBasic.Id; }
        }

        public string DisplayName
        {
            get
            {
                return CourseDomainObjBasic.DisplayName;
            }
        }

        public string Name
        {
            get
            {
                return CourseDomainObjBasic.Name;
            }
        }

        public bool IsMaster
        {
            get
            {
                return CourseDomainObjBasic.IsMaster;
            }
        }

        public Guid TermId
        {
            get
            {
                return CourseDomainObjBasic.TermId;
            }
        }

        public string NameExtension
        {
            get { return CourseDomainObjBasic.NameExtension; }
        }

        public Guid SelectedLessonId
        {
            get
            {
                return _selectedLessonId;
            }
            set
            {
                _selectedLessonId = value;
                foreach(var xLessonObj in ContainerChildLessons)
                {
                    xLessonObj.SelectedLessonId = value;
                }
            }
        }

        public bool IsForLessonManager
        {
            get
            {
                return _isForLessonManager ;
            }
            set
            {
                _isForLessonManager  = value;
                foreach (var xLessonObj in ContainerChildLessons)
                {
                    xLessonObj.IsForLessonManager  = value;
                }
            }
        }

        public void SetIsUsedInPlanBooleansForAllLessons(HashSet<Guid> usedLessonIdSet)
        {
            foreach (var xLesson in ContainerChildLessons)
            {
                xLesson.SetIsUsedInPlanBoolean(usedLessonIdSet);
            }
        }

        public Guid UserId
        {
            get { return CourseDomainObjBasic.UserId; }
        }

        public bool DoShowNarrativeNotifications
        {
            get { return PreferencesObj == null ? false : PreferencesObj.DoShowNarrativeNotifications; }
        }
        public bool DoShowLessonPlanNotifications
        {
            get { return PreferencesObj == null ? false : PreferencesObj.DoShowLessonPlanNotifications; }
        }
        public bool DoShowDocumentNotifications
        {
            get { return PreferencesObj == null ? false : PreferencesObj.DoShowDocumentNotifications; }
        }



    }
}
