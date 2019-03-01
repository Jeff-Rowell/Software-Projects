namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Lesson")]
    public partial class Lesson : IEntity
    {
        public Lesson()
        {
            DocumentUses = new HashSet<DocumentUse>();
            ContainerLessonChildren = new HashSet<Lesson>();
            MasterLessonChildren = new HashSet<Lesson>();
            PredecessorLessonChildren = new HashSet<Lesson>();
            MirrorLessons  = new HashSet<Lesson>();
            LessonUses = new HashSet<LessonUse>();
            MessageUses = new HashSet<MessageUse>();
            LessonTagUses = new HashSet<LessonTagUse>();
            StashedLessonPlans = new HashSet<StashedLessonPlan>();
            StashedNarratives = new HashSet<StashedNarrative>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public Guid? ContainerLessonId { get; set; }

        public Guid? MasterLessonId { get; set; }

        public Guid? PredecessorLessonId { get; set; }

        public Guid? MirrorTargetLessonId { get; set; }

        public Guid MetaLessonId { get; set; }

        public Guid LessonPlanId { get; set; }

        public Guid NarrativeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int? EstimatedTimeMin { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public DateTime? DateTimeDocumentsChoiceConfirmed { get; set; }

        /// <summary>
        /// This is used when moving the comparison date back in time.  This stores the value that can be returned to.
        /// </summary>
        public DateTime? ReferenceDateTimeDocChoiceConfirmed { get; set; }

        //public DateTime? GroupMostRecentDocModDate { get; set; }

        public DateTime? DateDocumentsModified { get; set; }



        public DateTime? LessonPlanDateChoiceConfirmed { get; set; }

        /// <summary>
        /// This is used when moving the comparison date back in time.  This stores the value that can be returned to.
        /// </summary>
        public DateTime? LessonPlanReferenceDateChoiceConfirmed { get; set; }

        public DateTime? NarrativeDateChoiceConfirmed { get; set; }

        /// <summary>
        /// This is used when moving the comparison date back in time.  This stores the value that can be returned to.
        /// </summary>
        public DateTime? NarrativeReferenceDateChoiceConfirmed { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? SequenceNumber { get; set; }

        public bool IsFolder {get; set;}

        public bool IsArchived { get; set; }

        public bool IsHidden { get; set; }

        public bool IsCollapsed { get; set; }

        [StringLength(500)]
        public string ChangeNotes { get; set; }

        public bool HasActiveMessages { get; set; }

        public bool HasActiveStoredMessages { get; set; }

        public bool HasStoredMessages { get; set; }

        public bool HasImportantMessages { get; set; }

        public bool HasNewMessages { get; set; }

        public bool HasOutForEditDocuments { get; set; }

        public bool HasNarrativeEditRights { get; set; }

        public bool HasLessonPlanEditRights { get; set; }

        public bool HasDocumentEditRights { get; set; }

        public bool HasMasterEditRights { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<DocumentUse> DocumentUses { get; set; }

        public virtual ICollection<Lesson> ContainerLessonChildren { get; set; }

        public virtual Lesson ContainerLesson { get; set; }

        public virtual MetaLesson MetaLesson { get; set; }

        public virtual ICollection<Lesson> MasterLessonChildren { get; set; }

        public virtual Lesson MasterLesson { get; set; }

        public virtual ICollection<Lesson> PredecessorLessonChildren { get; set; }

        public virtual Lesson PredecessorLesson { get; set; }

        public virtual ICollection<Lesson> MirrorLessons { get; set; }

        public virtual Lesson MirrorTargetLesson { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }

        public virtual Narrative Narrative { get; set; }

        public virtual ICollection<LessonUse> LessonUses { get; set; }

        public virtual ICollection<MessageUse> MessageUses { get; set; }

        public virtual ICollection<LessonTagUse> LessonTagUses { get; set; }

        public virtual ICollection<StashedLessonPlan> StashedLessonPlans { get; set; }

        public virtual ICollection<StashedNarrative> StashedNarratives { get; set; }

    }//End class
}
