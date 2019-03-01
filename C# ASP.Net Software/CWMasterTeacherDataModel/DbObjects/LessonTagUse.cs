using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    [Table("LessonTagUse")]
    public class LessonTagUse : IEntity
    {
        public LessonTagUse()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Guid TagId { get; set; }

        public Guid LessonId { get; set; }


        public virtual Tag Tag { get; set; }

        public virtual Lesson Lesson { get; set; }

    }
}
