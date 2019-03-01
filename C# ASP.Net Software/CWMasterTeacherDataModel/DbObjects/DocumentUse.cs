namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocumentUse")]
    public partial class DocumentUse : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        public Guid LessonId { get; set; }

        public Guid? DocumentTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsReference { get; set; }

        public bool IsInstructorOnly { get; set; }

        public bool IsVisibleToStudents { get; set; }

        public bool IsOutForEdit { get; set; }

        ///// <summary>
        ///// Deprecated Do not use.  Use the one in Document.
        ///// </summary>
        //public DateTime DateModified { get; set; }

        ///// <summary>
        ///// Deprecated. Do not use.  Use the one in Lesson.
        ///// </summary>
        //public DateTime DateChangesAccepted { get; set; }

        public virtual Document Document { get; set; }

        public virtual Lesson Lesson { get; set; }



    }//End class
}
