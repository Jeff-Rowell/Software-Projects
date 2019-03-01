using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ReleasedDocument :IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        public Guid LessonUseId { get; set; }

        [StringLength(500)]
        public string ModificationNotes { get; set; }

        public virtual Document Document { get; set; }
        public virtual LessonUse LessonUse { get; set; }
    }
}
