namespace CWMasterTeacherDataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MasterTeacherContext : DbContext
    {
        public MasterTeacherContext()
            : base("name=CWMT_DB")
        {
        }


        public virtual DbSet<ClassMeeting> ClassMeetings { get; set; }
        public virtual DbSet<ClassSection> ClassSections { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentLink> DocumentLinks { get; set; }
        public virtual DbSet<DocumentUse> DocumentUses { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<LessonUse> LessonUses { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Narrative> Narratives { get; set; }
        public virtual DbSet<ReferenceCalendar> ReferenceCalendars { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<LessonPlanLink> LessonPlanLinks { get; set; }
        public virtual DbSet<MessageUse> MessageUses { get; set; }
        public virtual DbSet<StashedLessonPlan> StashedLessonPlans { get; set; }
        public virtual DbSet<StashedNarrative> StashedNarratives { get; set; }
        public virtual DbSet<WorkingGroup> WorkingGroups { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagSet>TagSets { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TemplateSet> TemplateSets { get; set; }
        public virtual DbSet<LessonTagUse> LessonTagUses { get; set; }
        public virtual DbSet<UserPreference> UserPreferences { get; set; }
        public virtual DbSet<WorkingGroupPreference> WorkingGroupPreferences { get; set; }
        public virtual DbSet<CoursePreference> CoursePreferences { get; set; }
        public virtual DbSet<MetaCourse>MetaCourses { get; set; }
        public virtual DbSet<MetaLesson>MetaLessons { get; set; }
        public virtual DbSet<StudentUser> StudentUsers { get; set; }
        public virtual DbSet<ClassSectionStudent>ClassSectionStudents { get; set; }
        public virtual DbSet<ReleasedDocument>ReleasedDocuments { get; set; }
        public virtual DbSet<WorkingGroupStudent> WorkingGroupStudents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Entity<ClassMeeting>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<ClassMeeting>()
                .HasMany(e => e.LessonUses)
                .WithRequired(e => e.ClassMeeting)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassSection>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ClassSection>()
                .HasMany(e => e.ClassMeetings)
                .WithRequired(e => e.ClassSection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassSection>()
                .HasMany(e => e.ReferenceCalendars)
                .WithRequired(e => e.ClassSection)
                .HasForeignKey(e => e.ClassSectionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassSection>()
                .HasMany(e => e.ReferToMeCalendars)
                .WithRequired(e => e.ReferenceClassSection)
                .HasForeignKey(e => e.ReferenceClassSectionId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<ClassSection>()
                .HasMany(e => e.ClassSectionStudents)
                .WithRequired(e => e.ClassSection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassSection>()
                .HasMany(e => e.MirrorClassSections)
                .WithOptional(e => e.MirrorTargetClassSection)
                .HasForeignKey(e => e.MirrorTargetClassSectionId);

            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.ClassSections)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Lessons)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.MasterCourseChildren)
                .WithOptional(e => e.MasterCourse)
                .HasForeignKey(e => e.MasterCourseId);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.PredecessorCourseChildren)
                .WithOptional(e => e.PredecessorCourse)
                .HasForeignKey(e => e.PredecessorCourseId);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.MirrorCourses )
                .WithOptional(e => e.MirrorTargetCourse)
                .HasForeignKey(e => e.MirrorTargetCourseId);

            modelBuilder.Entity<CoursePreference>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.CoursePreference)
                .HasForeignKey(e => e.CoursePreferenceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.ModificationNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.OriginalDocumentChildren)
                .WithOptional(e => e.OriginalDocument)
                .HasForeignKey(e => e.OriginalDocumentId);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.DocumentLinks)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.DocumentUses)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.ReleasedDocuments)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Holiday>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Lesson>()
                .Property(e => e.ChangeNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.DocumentUses)
                .WithRequired(e => e.Lesson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.ContainerLessonChildren)
                .WithOptional(e => e.ContainerLesson)
                .HasForeignKey(e => e.ContainerLessonId);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.MasterLessonChildren)
                .WithOptional(e => e.MasterLesson)
                .HasForeignKey(e => e.MasterLessonId);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.PredecessorLessonChildren)
                .WithOptional(e => e.PredecessorLesson)
                .HasForeignKey(e => e.PredecessorLessonId);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.StashedLessonPlans)
                .WithRequired(e => e.Lesson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.StashedNarratives)
                .WithRequired(e => e.Lesson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.MessageUses)
                .WithRequired(e => e.Lesson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.LessonTagUses)
                .WithRequired(e => e.Lesson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.MirrorLessons)
                .WithOptional(e => e.MirrorTargetLesson)
                .HasForeignKey(e => e.MirrorTargetLessonId);

            modelBuilder.Entity<LessonUse>()
                .Property(e => e.CustomName)
                .IsUnicode(false);

            modelBuilder.Entity<LessonUse>()
                .HasMany(e => e.ReleasedDocuments)
                .WithRequired(e => e.LessonUse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LessonPlan>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LessonPlan>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<LessonPlan>()
                .Property(e => e.ModificationNotes)
                .IsUnicode(false);

            modelBuilder.Entity<LessonPlan>()
                .HasMany(e => e.Lessons)
                .WithRequired(e => e.LessonPlan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LessonPlan>()
                .HasMany(e => e.StashedLessonPlans)
                .WithRequired(e => e.LessonPlan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LessonPlan>()
                .HasMany(e => e.LessonPlanLinks)
                .WithRequired(e => e.LessonPlan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LessonUse>()
                .Property(e => e.CustomName)
                .IsUnicode(false);

            modelBuilder.Entity<LessonUse>()
                .Property(e => e.CustomText)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .HasMany(e => e.DocumentLinks)
                .WithRequired(e => e.Message)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasMany(e => e.LessonPlanLinks)
                .WithRequired(e => e.Message)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasMany(e => e.ThreadChildren)
                .WithOptional(e => e.ThreadParent)
                .HasForeignKey(e => e.ThreadParentId);

            modelBuilder.Entity<Message>()
                .HasMany(e => e.MessageUses)
                .WithRequired(e => e.Message)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MetaCourse>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MetaCourse>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.MetaCourse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MetaCourse>()
                .HasMany(e => e.MetaLessons)
                .WithRequired(e => e.MetaCourse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MetaLesson>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MetaLesson>()
                .HasMany(e => e.Lessons)
                .WithRequired(e => e.MetaLesson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Narrative>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Narrative>()
                .HasMany(e => e.Lessons)
                .WithRequired(e => e.Narrative)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Narrative>()
                .HasMany(e => e.CommentNarratives)
                .WithOptional(e => e.MasterNarrative)
                .HasForeignKey(e => e.MasterNarrativeId);

            modelBuilder.Entity<Narrative>()
                .HasMany(e => e.StashedNarratives)
                .WithRequired(e => e.Narrative)
                .HasForeignKey(e => e.NarrativeId);

            modelBuilder.Entity<StudentUser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<StudentUser>()
                .HasMany(e => e.ClassSectionStudents)
                .WithRequired(e => e.StudentUser)
                .HasForeignKey(e => e.StudentUserId);

            modelBuilder.Entity<StudentUser>()
                .HasMany(e => e.WorkingGroupStudents)
                .WithRequired(e => e.StudentUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.LessonTagUses)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TagSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TagSet>()
                .HasMany(e => e.MetaCourses)
                .WithOptional(e => e.TagSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TagSet>()
                .HasMany(e => e.Tags)
                .WithRequired(e => e.TagSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Template>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TemplateSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TemplateSet>()
                .HasMany(e => e.MetaCourses)
                .WithOptional(e => e.TemplateSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TemplateSet>()
                .HasMany(e => e.Templates)
                .WithRequired(e => e.TemplateSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Term>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Term>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Term)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Term>()
                .HasMany(e => e.Holidays)
                .WithRequired(e => e.Term)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ModifiedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.LessonPlans)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.ModifiedByUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Narratives)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ModifiedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TagSets)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserPreference>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserPreference)
                .HasForeignKey(e => e.UserPreferenceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<WorkingGroup>()
                .HasMany(e => e.MetaCourses)
                .WithRequired(e => e.WorkingGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroup>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.WorkingGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroup>()
                .HasMany(e => e.Terms)
                .WithRequired(e => e.WorkingGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroup>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.WorkingGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroup>()
                .HasMany(e => e.TemplateSets)
                .WithRequired(e => e.WorkingGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroup>()
                .HasMany(e => e.TagSets)
                .WithOptional(e => e.WorkingGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroup>()
                .HasMany(e => e.WorkingGroupStudents)
                .WithRequired(e => e.WorkingGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkingGroupPreference>()
                .HasMany(e => e.WorkingGroups)
                .WithRequired(e => e.WorkingGroupPreference)
                .HasForeignKey(e => e.WorkingGroupPreferenceId)
                .WillCascadeOnDelete(false);
        }
    }
}
