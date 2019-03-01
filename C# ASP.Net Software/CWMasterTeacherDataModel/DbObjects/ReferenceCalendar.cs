namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReferenceCalendar")]
    public partial class ReferenceCalendar : IEntity
    {
        public ReferenceCalendar()
        {
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ClassSectionId { get; set; }

        public Guid ReferenceClassSectionId { get; set; }

        public virtual ClassSection ClassSection { get; set; }

        public virtual ClassSection ReferenceClassSection { get; set; }
    }
}
