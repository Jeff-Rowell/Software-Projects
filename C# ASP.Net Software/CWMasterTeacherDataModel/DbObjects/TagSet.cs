using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    [Table("TagSet")]
    public partial class TagSet : IEntity
    {

        public TagSet()
        {
            MetaCourses = new HashSet<MetaCourse>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// This is set up to support both WorkingGroup and User Tag sets.  Thus, these Ids are optional.
        /// Code should require one of them to be set.
        /// </summary>
        public Guid? WorkingGroupId { get; set; }
        public Guid? UserId { get; set; }

        public bool IsArchived { get; set; }
        public DateTime DateArchived { get; set; }

        public virtual WorkingGroup WorkingGroup { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<MetaCourse> MetaCourses { get; set; }

        public virtual ICollection <Tag> Tags { get; }

    }
}
