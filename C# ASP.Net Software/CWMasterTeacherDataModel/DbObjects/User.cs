namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User : IEntity
    {
        public User()
        {
            Courses = new HashSet<Course>();
            Documents = new HashSet<Document>();
            LessonPlans = new HashSet<LessonPlan>();
            Messages = new HashSet<Message>();
            Narratives = new HashSet<Narrative>();
            TagSets = new HashSet<TagSet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid WorkingGroupId { get; set; }

        public Guid UserPreferenceId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public bool IsApplicationAdmin { get; set; }

        public bool IsWorkingGroupAdmin { get; set; }

        public bool IsNarrativeEditor { get; set; }

        public bool IsActive { get; set; }

        public bool HasAdminApproval { get; set; }

        public DateTime? LastLogin { get; set; }

        public Guid? LastDisplayedCourseId { get; set; }

        public bool ShowMastersInCourseList { get; set; }

        public bool ShowAllInCourseList { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual WorkingGroup WorkingGroup { get; set; }

        public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Narrative> Narratives { get; set; }

        public virtual UserPreference UserPreference { get; set; }

        public virtual ICollection<TagSet> TagSets { get; set; }

    }// End class
}
