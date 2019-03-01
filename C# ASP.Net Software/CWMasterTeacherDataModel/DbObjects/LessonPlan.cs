namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LessonPlan")]
    public partial class LessonPlan : IEntity
    {
        private string _indvOrMstr;

        public LessonPlan()
        {
            LessonPlanLinks = new HashSet<LessonPlanLink>();
            StashedLessonPlans = new HashSet<StashedLessonPlan>();
            Lessons = new HashSet<Lesson>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid? ModifiedByUserId { get; set; }

        public string ModificationNotes { get; set; }

        public bool IsMaster { get; set; }

        public bool IsPreNote { get; set; }

        public bool IsPostNote { get; set; }

        public bool DoSuggestUsingMaster { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<LessonPlanLink> LessonPlanLinks { get; set; }

        public virtual ICollection<StashedLessonPlan> StashedLessonPlans { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

    }
}
