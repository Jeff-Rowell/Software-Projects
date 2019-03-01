namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message : IEntity
    {
        public Message()
        {
            DocumentLinks = new HashSet<DocumentLink>();
            LessonPlanLinks = new HashSet<LessonPlanLink>();
            ThreadChildren = new HashSet<Message>();
            MessageUses = new HashSet<MessageUse>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }
        
        //Note that "Parent" means something different than it does in Lessons
        //Parent always points back to the original thread parent, which has no parent Id.
        public Guid? ThreadParentId { get; set; }

        [Required]
        [StringLength(255)]
        public string Subject { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public virtual ICollection<DocumentLink> DocumentLinks { get; set; }

        public virtual ICollection<LessonPlanLink> LessonPlanLinks { get; set; }

        public virtual ICollection<Message> ThreadChildren { get; set; }

        public virtual Message ThreadParent { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<MessageUse> MessageUses { get; set; }


    }//End Class
}
