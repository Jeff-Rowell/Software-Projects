namespace CWMasterTeacherDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Narrative")]
    public partial class Narrative : IEntity
    {
        public Narrative()
        {
            Lessons = new HashSet<Lesson>();
            CommentNarratives = new HashSet<Narrative>();
            StashedNarratives = new HashSet<StashedNarrative>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ModifiedByUserId { get; set; }

        public Guid? MasterNarrativeId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Text { get; set; }

        public DateTime DateModified { get; set; }

        public bool DoSuggestRemovingComment { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Narrative> CommentNarratives { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual Narrative MasterNarrative { get; set; }

        public virtual ICollection<StashedNarrative> StashedNarratives { get; set; }

    }
}
