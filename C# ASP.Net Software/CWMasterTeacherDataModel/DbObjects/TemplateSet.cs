using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class TemplateSet :IEntity 
    {

        public TemplateSet()
        {
            Templates = new HashSet<Template>();
            MetaCourses = new HashSet<MetaCourse>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid WorkingGroupId { get; set; }

        public string Name { get; set; }

        public virtual WorkingGroup WorkingGroup { get; set; }

        public ICollection<Template> Templates { get; set; }

        public ICollection<MetaCourse> MetaCourses { get; set; }
    }
}
