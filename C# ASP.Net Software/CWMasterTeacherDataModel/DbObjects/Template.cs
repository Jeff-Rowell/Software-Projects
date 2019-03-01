using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public partial class Template : IEntity
    {
        public Template()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TemplateSetId { get; set; }

        public bool IsNarrativeTemplate { get; set; }

        public bool IsLessonNotesTemplate { get; set; }

        public bool IsPreClassNotesTemplate { get; set; }

        public bool IsPostClassNotesTemplate { get; set; }

        public string Name { get; set; }

        public int SequenceNumber { get; set; }


        public virtual TemplateSet TemplateSet { get; set; }
    }
}
