using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class WorkingGroupStudent: IEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid WorkingGroupId { get; set; }

        public Guid StudentUserId { get; set; }

        public virtual WorkingGroup WorkingGroup { get; set; }

        public virtual StudentUser StudentUser { get; set; }
    }
}
