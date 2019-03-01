namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassSection")]
    public partial class ClassSection : IEntity
    {
        public ClassSection()
        {
            ClassMeetings = new HashSet<ClassMeeting>();
            ReferenceCalendars = new HashSet<ReferenceCalendar>();
            ReferToMeCalendars = new HashSet<ReferenceCalendar>();
            ClassSectionStudents = new HashSet<ClassSectionStudent>();
            MirrorClassSections = new HashSet<ClassSection>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Guid? MirrorTargetClassSectionId { get; set; }

        public DateTime StudentAccessExpirationDate { get; set; }

        public Guid? LastDisplayedClassMeetingId { get; set; }

        public virtual ICollection<ClassMeeting> ClassMeetings { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<ReferenceCalendar> ReferenceCalendars { get; set; }

        public virtual ICollection<ReferenceCalendar> ReferToMeCalendars { get; set; }

        public virtual ICollection<ClassSectionStudent> ClassSectionStudents { get; set; }

        public virtual ClassSection MirrorTargetClassSection { get; set; }

        public virtual ICollection<ClassSection>MirrorClassSections { get; set; }





    }
}
