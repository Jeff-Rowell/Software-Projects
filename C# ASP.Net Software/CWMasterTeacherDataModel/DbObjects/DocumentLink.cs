namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocumentLink")]
    public partial class DocumentLink : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MessageId { get; set; }

        public Guid DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Message Message { get; set; }
    }
}
