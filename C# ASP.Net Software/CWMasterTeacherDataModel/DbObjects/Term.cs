namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Term")]
    public partial class Term : IEntity
    {
        public Term()
        {
            Courses = new HashSet<Course>();
            Holidays = new HashSet<Holiday>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid WorkingGroupId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Holiday> Holidays { get; set; }

        public virtual WorkingGroup WorkingGroup { get; set; }
    }
}
