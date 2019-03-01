namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkingGroup")]
    public partial class WorkingGroup : IEntity
    {
        public WorkingGroup()
        {
            MetaCourses = new HashSet<MetaCourse>();
            Courses = new HashSet<Course>();
            Terms = new HashSet<Term>();
            Users = new HashSet<User>();
            TemplateSets = new HashSet<TemplateSet>();
            WorkingGroupStudents = new HashSet<WorkingGroupStudent>();
            TagSets = new HashSet<TagSet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string StudentAccessCode { get; set; }

        public string InstructorAccessCode { get; set; }

        public Guid WorkingGroupPreferenceId { get; set; }

        public virtual ICollection<MetaCourse> MetaCourses { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Term> Terms { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection <TemplateSet> TemplateSets { get; set; }

        public virtual ICollection<TagSet> TagSets { get; set; }

        public virtual WorkingGroupPreference WorkingGroupPreference { get; set; }

        public virtual ICollection<WorkingGroupStudent> WorkingGroupStudents { get; set; }

    }
}
