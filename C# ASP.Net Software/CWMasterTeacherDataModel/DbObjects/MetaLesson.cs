using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    [Table("MetaLesson")]
    public class MetaLesson: IEntity
    {
        public MetaLesson()
        {
            Lessons = new HashSet<Lesson>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MetaCourseId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public virtual MetaCourse MetaCourse { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
