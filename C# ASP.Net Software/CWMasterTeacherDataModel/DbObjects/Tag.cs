using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{

    [Table("Tag")]
    public class Tag: IEntity
    {
        public Tag()
        {
            LessonTagUses = new HashSet<LessonTagUse>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Guid TagSetId { get; set; }

        public string HTML_Style { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public int SequenceNumber { get; set; }

        public bool IsArchived { get; set; }
        public DateTime DateArchived { get; set; }

        public bool IsLessonTag { get; set; }
        public bool IsLessonPlanTag { get; set; }

        public bool DoHideAsOptional { get; set; }


        public virtual TagSet TagSet { get; set; }

        public virtual ICollection<LessonTagUse>LessonTagUses { get; set; }

    }
}
