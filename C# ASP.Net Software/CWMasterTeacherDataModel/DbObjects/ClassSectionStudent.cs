using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class ClassSectionStudent : IEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ClassSectionId { get; set; }
        public Guid StudentUserId { get; set; }

        public bool HasBeenApproved { get; set; }
        public bool HasBeenDenied { get; set; }

        public virtual ClassSection ClassSection { get; set; }
        public virtual StudentUser StudentUser { get; set; }

    }
}
