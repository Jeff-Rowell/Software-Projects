namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Course")]
    public partial class Course: IEntity
    {
        public Course()
        {
            ClassSections = new HashSet<ClassSection>();
            Lessons = new HashSet<Lesson>();
            MasterCourseChildren = new HashSet<Course>();
            PredecessorCourseChildren = new HashSet<Course>();
            MirrorCourses = new HashSet<Course>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid WorkingGroupId { get; set; }

        public Guid CoursePreferenceId { get; set; }

        public Guid TermId { get; set; }

        public Guid UserId { get; set; }

        public Guid MetaCourseId { get; set; }

        public Guid? MasterCourseId { get; set; }

        public Guid? OriginalCourseId { get; set; }

        /// <summary>
        /// Deprecated.  Do not use.  Can't delete now because of foreign key.
        /// </summary>
        public Guid? MirrorTargetCourseId { get; set; }

        public Guid? PredecessorCourseId { get; set; }

        public Guid? LastDisplayedClassSectionId { get; set; }

        public Guid? LastDisplayedLessonId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateModified { get; set; }

        public bool IsActive { get; set; }

        public bool ShowFolders { get; set; }

        public bool ShowOptionalLessons { get; set; }

        public bool ShowHiddenLessons { get; set; }

        public bool HasActiveMessages { get; set; }

        public bool HasActiveStoredMessages { get; set; }

        public bool HasStoredMessages { get; set; }

        public bool HasImportantMessages { get; set; }

        public bool HasNewMessages { get; set; }

        public bool HasOutForEditDocuments { get; set; }

        public bool IsArchived { get; set; }

        public bool IsMaster { get; set; }

        public bool HasNarrativeEditRights { get; set; }

        public bool HasLessonPlanEditRights { get; set; }

        public bool HasDocumentEditRights { get; set; }

        public bool HasMasterEditRights { get; set; }

        public virtual ICollection<ClassSection> ClassSections { get; set; }

        public virtual WorkingGroup WorkingGroup { get; set; }

        public virtual CoursePreference CoursePreference { get; set; }

        public virtual Term Term { get; set; }

        public virtual User User { get; set; }

        public virtual MetaCourse MetaCourse { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Course> MasterCourseChildren { get; set; }

        public virtual Course MasterCourse { get; set; }

        public virtual ICollection<Course> PredecessorCourseChildren { get; set; }

        public virtual Course PredecessorCourse { get; set; }

        public virtual ICollection<Course> MirrorCourses { get; set; }

        public virtual Course MirrorTargetCourse { get; set; }


    }//End Class
}
