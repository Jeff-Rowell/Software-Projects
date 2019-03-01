using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public partial class StashedLessonPlan : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LessonId { get; set; }

        public Guid LessonPlanId { get; set; }

        public bool IsSaved { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? InactivationDate { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }


    }
}
