using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    [Table("MetaCourse")]
    public partial class MetaCourse : IEntity
    {
        public MetaCourse()
        {
            Courses = new HashSet<Course>();
            MetaLessons = new HashSet<MetaLesson>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid WorkingGroupId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public Guid? TagSetId { get; set; }

        public Guid? TemplateSetId { get; set; }

        public virtual WorkingGroup WorkingGroup { get; set; }

        public virtual ICollection<MetaLesson> MetaLessons { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual TagSet TagSet { get; set; }

        public virtual TemplateSet TemplateSet { get; set; }

    }
}
