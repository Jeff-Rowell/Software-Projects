using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public partial class StashedNarrative : IEntity 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        public Guid LessonId { get; set; }

        public Guid NarrativeId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? InactivationDate { get; set; }

        public bool IsSaved { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual Narrative Narrative { get; set; }
    }
}
