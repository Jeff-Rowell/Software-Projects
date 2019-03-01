namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LessonPlanLink")]
    public partial class LessonPlanLink : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LessonPlanId { get; set; }

        public Guid MessageId { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }

        public virtual Message Message { get; set; }
    }
}
