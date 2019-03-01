using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CWMasterTeacherDataModel
{
    [Table("WorkingRelationship")]
    public partial class WorkingRelationship : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public Guid UserId { get; set; }

        public bool FollowUserNarratives { get; set; }

        public int UserNarrativeSequenceNumber { get; set; }

        public virtual Course Course { get; set; }

        public virtual User User { get; set; }

        public string Name { get; set; }


    }//End Class
}
