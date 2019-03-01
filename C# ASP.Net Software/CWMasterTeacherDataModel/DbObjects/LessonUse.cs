namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LessonUse")]
    public partial class LessonUse : IEntity
    {
        public LessonUse()
        {
            ReleasedDocuments = new HashSet<ReleasedDocument>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ClassMeetingId { get; set; }

        public Guid? LessonId { get; set; }

        public int? SequenceNumber { get; set; }

        [StringLength(255)]
        public string CustomName { get; set; }

        [Column(TypeName = "text")]
        public string CustomText { get; set; }

        public int? CustomTime { get; set; }

        public bool HasCustomTime { get; set; }

        public virtual ClassMeeting ClassMeeting { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual ICollection<ReleasedDocument> ReleasedDocuments { get; set; }


    }// End class
}
