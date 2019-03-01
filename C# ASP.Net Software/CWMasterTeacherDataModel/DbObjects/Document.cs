namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    

    [Table("Document")]
    public partial class Document : IEntity
    {

        public Document()
        {
            OriginalDocumentChildren = new HashSet<Document>();
            DocumentLinks = new HashSet<DocumentLink>();
            DocumentUses = new HashSet<DocumentUse>();
            ReleasedDocuments = new HashSet<ReleasedDocument>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ModifiedByUserId { get; set; }

        public Guid? OriginalDocumentId { get; set; }

        public Guid? PredecessorId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Path { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(255)]
        public string PathToPdfCopy { get; set; }

        [StringLength(255)]
        public string FileNameOfPdfCopy { get; set; }

        [StringLength(500)]
        public string ModificationNotes { get; set; }

        public DateTime? DateModified { get; set; }

        public virtual ICollection<Document> OriginalDocumentChildren { get; set; }

        public virtual Document OriginalDocument { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<DocumentLink> DocumentLinks { get; set; }

        public virtual ICollection<DocumentUse> DocumentUses { get; set; }

        public virtual ICollection<ReleasedDocument> ReleasedDocuments { get; set; }


    }//End Class
}
