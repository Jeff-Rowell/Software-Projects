namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageUse")]
    public partial class MessageUse : IEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MessageId { get; set; }

        public Guid LessonId { get; set; }

        public bool IsNew { get; set; }

        public bool IsStored { get; set; }

        public bool IsImportant { get; set; }

        public bool IsArchived { get; set; }

        public bool IsForEndOfSemRev { get; set; }

        public bool ShowArchived { get; set; }

        public DateTime? StorageReferenceTime { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual Message Message { get; set; }

    }
}
