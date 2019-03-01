namespace CWMasterTeacherDataModel
{
    using CWMasterTeacherDomain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Globalization;

    [Table("ClassMeeting")]
    public partial class ClassMeeting : IEntity
    {
        public ClassMeeting()
        {
            LessonUses = new HashSet<LessonUse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid ClassSectionId { get; set; }

        public DateTime MeetingDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int? MeetingNumber { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public bool NoClass { get; set; }

        public bool IsExamDay { get; set; }

        public bool IsBeginningOfWeek { get; set; }

        public bool IsReadyToTeach { get; set; }

        public string NotesForStudents { get; set; }

        public virtual ClassSection ClassSection { get; set; }

        public virtual ICollection<LessonUse> LessonUses { get; set; }


    }//End Class
}
