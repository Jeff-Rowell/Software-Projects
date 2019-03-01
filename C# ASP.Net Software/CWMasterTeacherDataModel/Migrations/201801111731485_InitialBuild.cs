namespace CWMasterTeacherDataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialBuild : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassMeeting",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ClassSectionId = c.Guid(nullable: false),
                        MeetingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        MeetingNumber = c.Int(),
                        Comment = c.String(maxLength: 255, unicode: false),
                        NoClass = c.Boolean(nullable: false),
                        IsExamDay = c.Boolean(nullable: false),
                        IsBeginningOfWeek = c.Boolean(nullable: false),
                        IsReadyToTeach = c.Boolean(nullable: false),
                        NotesForStudents = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassSection", t => t.ClassSectionId)
                .Index(t => t.ClassSectionId);
            
            CreateTable(
                "dbo.ClassSection",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CourseId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        MirrorTargetClassSectionId = c.Guid(),
                        StudentAccessExpirationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastDisplayedClassMeetingId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.ClassSection", t => t.MirrorTargetClassSectionId)
                .Index(t => t.CourseId)
                .Index(t => t.MirrorTargetClassSectionId);
            
            CreateTable(
                "dbo.ClassSectionStudents",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ClassSectionId = c.Guid(nullable: false),
                        StudentUserId = c.Guid(nullable: false),
                        HasBeenApproved = c.Boolean(nullable: false),
                        HasBeenDenied = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentUsers", t => t.StudentUserId, cascadeDelete: true)
                .ForeignKey("dbo.ClassSection", t => t.ClassSectionId)
                .Index(t => t.ClassSectionId)
                .Index(t => t.StudentUserId);
            
            CreateTable(
                "dbo.StudentUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserName = c.String(unicode: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        LastLoginDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkingGroupStudents",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        WorkingGroupId = c.Guid(nullable: false),
                        StudentUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkingGroup", t => t.WorkingGroupId)
                .ForeignKey("dbo.StudentUsers", t => t.StudentUserId)
                .Index(t => t.WorkingGroupId)
                .Index(t => t.StudentUserId);
            
            CreateTable(
                "dbo.WorkingGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                        StudentAccessCode = c.String(),
                        InstructorAccessCode = c.String(),
                        WorkingGroupPreferenceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkingGroupPreferences", t => t.WorkingGroupPreferenceId)
                .Index(t => t.WorkingGroupPreferenceId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        WorkingGroupId = c.Guid(nullable: false),
                        CoursePreferenceId = c.Guid(nullable: false),
                        TermId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        MetaCourseId = c.Guid(nullable: false),
                        MasterCourseId = c.Guid(),
                        OriginalCourseId = c.Guid(),
                        MirrorTargetCourseId = c.Guid(),
                        PredecessorCourseId = c.Guid(),
                        LastDisplayedClassSectionId = c.Guid(),
                        LastDisplayedLessonId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        DateModified = c.DateTime(nullable: false, storeType: "date"),
                        IsActive = c.Boolean(nullable: false),
                        ShowFolders = c.Boolean(nullable: false),
                        ShowOptionalLessons = c.Boolean(nullable: false),
                        ShowHiddenLessons = c.Boolean(nullable: false),
                        HasActiveMessages = c.Boolean(nullable: false),
                        HasActiveStoredMessages = c.Boolean(nullable: false),
                        HasStoredMessages = c.Boolean(nullable: false),
                        HasImportantMessages = c.Boolean(nullable: false),
                        HasNewMessages = c.Boolean(nullable: false),
                        HasOutForEditDocuments = c.Boolean(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        IsMaster = c.Boolean(nullable: false),
                        HasNarrativeEditRights = c.Boolean(nullable: false),
                        HasLessonPlanEditRights = c.Boolean(nullable: false),
                        HasDocumentEditRights = c.Boolean(nullable: false),
                        HasMasterEditRights = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoursePreferences", t => t.CoursePreferenceId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.MetaCourse", t => t.MetaCourseId)
                .ForeignKey("dbo.Course", t => t.MasterCourseId)
                .ForeignKey("dbo.Course", t => t.MirrorTargetCourseId)
                .ForeignKey("dbo.Course", t => t.PredecessorCourseId)
                .ForeignKey("dbo.Term", t => t.TermId)
                .ForeignKey("dbo.WorkingGroup", t => t.WorkingGroupId)
                .Index(t => t.WorkingGroupId)
                .Index(t => t.CoursePreferenceId)
                .Index(t => t.TermId)
                .Index(t => t.UserId)
                .Index(t => t.MetaCourseId)
                .Index(t => t.MasterCourseId)
                .Index(t => t.MirrorTargetCourseId)
                .Index(t => t.PredecessorCourseId);
            
            CreateTable(
                "dbo.CoursePreferences",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        DoShowNarrativeNotifications = c.Boolean(nullable: false),
                        DoShowLessonPlanNotifications = c.Boolean(nullable: false),
                        DoShowDocumentNotifications = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lesson",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CourseId = c.Guid(nullable: false),
                        ContainerLessonId = c.Guid(),
                        MasterLessonId = c.Guid(),
                        PredecessorLessonId = c.Guid(),
                        MirrorTargetLessonId = c.Guid(),
                        MetaLessonId = c.Guid(nullable: false),
                        LessonPlanId = c.Guid(nullable: false),
                        NarrativeId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        EstimatedTimeMin = c.Int(),
                        DateTimeCreated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTimeDocumentsChoiceConfirmed = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReferenceDateTimeDocChoiceConfirmed = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateDocumentsModified = c.DateTime(precision: 7, storeType: "datetime2"),
                        LessonPlanDateChoiceConfirmed = c.DateTime(precision: 7, storeType: "datetime2"),
                        LessonPlanReferenceDateChoiceConfirmed = c.DateTime(precision: 7, storeType: "datetime2"),
                        NarrativeDateChoiceConfirmed = c.DateTime(precision: 7, storeType: "datetime2"),
                        NarrativeReferenceDateChoiceConfirmed = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateCreated = c.DateTime(precision: 7, storeType: "datetime2"),
                        SequenceNumber = c.Int(),
                        IsFolder = c.Boolean(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        IsHidden = c.Boolean(nullable: false),
                        IsCollapsed = c.Boolean(nullable: false),
                        ChangeNotes = c.String(maxLength: 500, unicode: false),
                        HasActiveMessages = c.Boolean(nullable: false),
                        HasActiveStoredMessages = c.Boolean(nullable: false),
                        HasStoredMessages = c.Boolean(nullable: false),
                        HasImportantMessages = c.Boolean(nullable: false),
                        HasNewMessages = c.Boolean(nullable: false),
                        HasOutForEditDocuments = c.Boolean(nullable: false),
                        HasNarrativeEditRights = c.Boolean(nullable: false),
                        HasLessonPlanEditRights = c.Boolean(nullable: false),
                        HasDocumentEditRights = c.Boolean(nullable: false),
                        HasMasterEditRights = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lesson", t => t.ContainerLessonId)
                .ForeignKey("dbo.LessonPlan", t => t.LessonPlanId)
                .ForeignKey("dbo.Narrative", t => t.NarrativeId)
                .ForeignKey("dbo.MetaLesson", t => t.MetaLessonId)
                .ForeignKey("dbo.Lesson", t => t.MasterLessonId)
                .ForeignKey("dbo.Lesson", t => t.MirrorTargetLessonId)
                .ForeignKey("dbo.Lesson", t => t.PredecessorLessonId)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .Index(t => t.CourseId)
                .Index(t => t.ContainerLessonId)
                .Index(t => t.MasterLessonId)
                .Index(t => t.PredecessorLessonId)
                .Index(t => t.MirrorTargetLessonId)
                .Index(t => t.MetaLessonId)
                .Index(t => t.LessonPlanId)
                .Index(t => t.NarrativeId);
            
            CreateTable(
                "dbo.DocumentUse",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DocumentId = c.Guid(nullable: false),
                        LessonId = c.Guid(nullable: false),
                        DocumentTypeId = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsReference = c.Boolean(nullable: false),
                        IsInstructorOnly = c.Boolean(nullable: false),
                        IsVisibleToStudents = c.Boolean(nullable: false),
                        IsOutForEdit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Document", t => t.DocumentId)
                .ForeignKey("dbo.Lesson", t => t.LessonId)
                .Index(t => t.DocumentId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ModifiedByUserId = c.Guid(nullable: false),
                        OriginalDocumentId = c.Guid(),
                        PredecessorId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Path = c.String(nullable: false, maxLength: 255, unicode: false),
                        FileName = c.String(nullable: false, maxLength: 255, unicode: false),
                        PathToPdfCopy = c.String(maxLength: 255),
                        FileNameOfPdfCopy = c.String(maxLength: 255),
                        ModificationNotes = c.String(maxLength: 500, unicode: false),
                        DateModified = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ModifiedByUserId)
                .ForeignKey("dbo.Document", t => t.OriginalDocumentId)
                .Index(t => t.ModifiedByUserId)
                .Index(t => t.OriginalDocumentId);
            
            CreateTable(
                "dbo.DocumentLink",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MessageId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Message", t => t.MessageId)
                .ForeignKey("dbo.Document", t => t.DocumentId)
                .Index(t => t.MessageId)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AuthorId = c.Guid(nullable: false),
                        ThreadParentId = c.Guid(),
                        Subject = c.String(nullable: false, maxLength: 255, unicode: false),
                        Text = c.String(nullable: false, unicode: false, storeType: "text"),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AuthorId)
                .ForeignKey("dbo.Message", t => t.ThreadParentId)
                .Index(t => t.AuthorId)
                .Index(t => t.ThreadParentId);
            
            CreateTable(
                "dbo.LessonPlanLink",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        LessonPlanId = c.Guid(nullable: false),
                        MessageId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LessonPlan", t => t.LessonPlanId)
                .ForeignKey("dbo.Message", t => t.MessageId)
                .Index(t => t.LessonPlanId)
                .Index(t => t.MessageId);
            
            CreateTable(
                "dbo.LessonPlan",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Text = c.String(unicode: false),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedByUserId = c.Guid(),
                        ModificationNotes = c.String(unicode: false),
                        IsMaster = c.Boolean(nullable: false),
                        IsPreNote = c.Boolean(nullable: false),
                        IsPostNote = c.Boolean(nullable: false),
                        DoSuggestUsingMaster = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ModifiedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.StashedLessonPlans",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        LessonId = c.Guid(nullable: false),
                        LessonPlanId = c.Guid(nullable: false),
                        IsSaved = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        InactivationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LessonPlan", t => t.LessonPlanId)
                .ForeignKey("dbo.Lesson", t => t.LessonId)
                .Index(t => t.LessonId)
                .Index(t => t.LessonPlanId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        WorkingGroupId = c.Guid(nullable: false),
                        UserPreferenceId = c.Guid(nullable: false),
                        UserName = c.String(unicode: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        DisplayName = c.String(unicode: false),
                        EmailAddress = c.String(),
                        IsApplicationAdmin = c.Boolean(nullable: false),
                        IsWorkingGroupAdmin = c.Boolean(nullable: false),
                        IsNarrativeEditor = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        HasAdminApproval = c.Boolean(nullable: false),
                        LastLogin = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastDisplayedCourseId = c.Guid(),
                        ShowMastersInCourseList = c.Boolean(nullable: false),
                        ShowAllInCourseList = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserPreferences", t => t.UserPreferenceId)
                .ForeignKey("dbo.WorkingGroup", t => t.WorkingGroupId)
                .Index(t => t.WorkingGroupId)
                .Index(t => t.UserPreferenceId);
            
            CreateTable(
                "dbo.Narrative",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ModifiedByUserId = c.Guid(nullable: false),
                        MasterNarrativeId = c.Guid(),
                        Text = c.String(nullable: false, unicode: false, storeType: "text"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DoSuggestRemovingComment = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Narrative", t => t.MasterNarrativeId)
                .ForeignKey("dbo.User", t => t.ModifiedByUserId)
                .Index(t => t.ModifiedByUserId)
                .Index(t => t.MasterNarrativeId);
            
            CreateTable(
                "dbo.StashedNarratives",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        LessonId = c.Guid(nullable: false),
                        NarrativeId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        InactivationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsSaved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Narrative", t => t.NarrativeId, cascadeDelete: true)
                .ForeignKey("dbo.Lesson", t => t.LessonId)
                .Index(t => t.LessonId)
                .Index(t => t.NarrativeId);
            
            CreateTable(
                "dbo.TagSet",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        WorkingGroupId = c.Guid(),
                        UserId = c.Guid(),
                        IsArchived = c.Boolean(nullable: false),
                        DateArchived = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.WorkingGroup", t => t.WorkingGroupId)
                .Index(t => t.WorkingGroupId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MetaCourse",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        WorkingGroupId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                        TagSetId = c.Guid(),
                        TemplateSetId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TemplateSets", t => t.TemplateSetId)
                .ForeignKey("dbo.TagSet", t => t.TagSetId)
                .ForeignKey("dbo.WorkingGroup", t => t.WorkingGroupId)
                .Index(t => t.WorkingGroupId)
                .Index(t => t.TagSetId)
                .Index(t => t.TemplateSetId);
            
            CreateTable(
                "dbo.MetaLesson",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MetaCourseId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MetaCourse", t => t.MetaCourseId)
                .Index(t => t.MetaCourseId);
            
            CreateTable(
                "dbo.TemplateSets",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        WorkingGroupId = c.Guid(nullable: false),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkingGroup", t => t.WorkingGroupId)
                .Index(t => t.WorkingGroupId);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        TemplateSetId = c.Guid(nullable: false),
                        IsNarrativeTemplate = c.Boolean(nullable: false),
                        IsLessonNotesTemplate = c.Boolean(nullable: false),
                        IsPreClassNotesTemplate = c.Boolean(nullable: false),
                        IsPostClassNotesTemplate = c.Boolean(nullable: false),
                        Name = c.String(unicode: false),
                        SequenceNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TemplateSets", t => t.TemplateSetId)
                .Index(t => t.TemplateSetId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        TagSetId = c.Guid(nullable: false),
                        HTML_Style = c.String(),
                        Prefix = c.String(),
                        Suffix = c.String(),
                        SequenceNumber = c.Int(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        DateArchived = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsLessonTag = c.Boolean(nullable: false),
                        IsLessonPlanTag = c.Boolean(nullable: false),
                        DoHideAsOptional = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TagSet", t => t.TagSetId)
                .Index(t => t.TagSetId);
            
            CreateTable(
                "dbo.LessonTagUse",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        TagId = c.Guid(nullable: false),
                        LessonId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tag", t => t.TagId)
                .ForeignKey("dbo.Lesson", t => t.LessonId)
                .Index(t => t.TagId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.UserPreferences",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageUse",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MessageId = c.Guid(nullable: false),
                        LessonId = c.Guid(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        IsStored = c.Boolean(nullable: false),
                        IsImportant = c.Boolean(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        IsForEndOfSemRev = c.Boolean(nullable: false),
                        ShowArchived = c.Boolean(nullable: false),
                        StorageReferenceTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Message", t => t.MessageId)
                .ForeignKey("dbo.Lesson", t => t.LessonId)
                .Index(t => t.MessageId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.ReleasedDocuments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DocumentId = c.Guid(nullable: false),
                        LessonUseId = c.Guid(nullable: false),
                        ModificationNotes = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LessonUse", t => t.LessonUseId)
                .ForeignKey("dbo.Document", t => t.DocumentId)
                .Index(t => t.DocumentId)
                .Index(t => t.LessonUseId);
            
            CreateTable(
                "dbo.LessonUse",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ClassMeetingId = c.Guid(nullable: false),
                        LessonId = c.Guid(),
                        SequenceNumber = c.Int(),
                        CustomName = c.String(maxLength: 255, unicode: false),
                        CustomText = c.String(unicode: false, storeType: "text"),
                        CustomTime = c.Int(),
                        HasCustomTime = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lesson", t => t.LessonId)
                .ForeignKey("dbo.ClassMeeting", t => t.ClassMeetingId)
                .Index(t => t.ClassMeetingId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Term",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        WorkingGroupId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        IsCurrent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkingGroup", t => t.WorkingGroupId)
                .Index(t => t.WorkingGroupId);
            
            CreateTable(
                "dbo.Holiday",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        TermId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Term", t => t.TermId)
                .Index(t => t.TermId);
            
            CreateTable(
                "dbo.WorkingGroupPreferences",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReferenceCalendar",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ClassSectionId = c.Guid(nullable: false),
                        ReferenceClassSectionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassSection", t => t.ClassSectionId)
                .ForeignKey("dbo.ClassSection", t => t.ReferenceClassSectionId)
                .Index(t => t.ClassSectionId)
                .Index(t => t.ReferenceClassSectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LessonUse", "ClassMeetingId", "dbo.ClassMeeting");
            DropForeignKey("dbo.ReferenceCalendar", "ReferenceClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.ReferenceCalendar", "ClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.ClassSection", "MirrorTargetClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.ClassSectionStudents", "ClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.WorkingGroupStudents", "StudentUserId", "dbo.StudentUsers");
            DropForeignKey("dbo.WorkingGroupStudents", "WorkingGroupId", "dbo.WorkingGroup");
            DropForeignKey("dbo.WorkingGroup", "WorkingGroupPreferenceId", "dbo.WorkingGroupPreferences");
            DropForeignKey("dbo.User", "WorkingGroupId", "dbo.WorkingGroup");
            DropForeignKey("dbo.Term", "WorkingGroupId", "dbo.WorkingGroup");
            DropForeignKey("dbo.TemplateSets", "WorkingGroupId", "dbo.WorkingGroup");
            DropForeignKey("dbo.TagSet", "WorkingGroupId", "dbo.WorkingGroup");
            DropForeignKey("dbo.MetaCourse", "WorkingGroupId", "dbo.WorkingGroup");
            DropForeignKey("dbo.Course", "WorkingGroupId", "dbo.WorkingGroup");
            DropForeignKey("dbo.Holiday", "TermId", "dbo.Term");
            DropForeignKey("dbo.Course", "TermId", "dbo.Term");
            DropForeignKey("dbo.Course", "PredecessorCourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "MirrorTargetCourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "MasterCourseId", "dbo.Course");
            DropForeignKey("dbo.Lesson", "CourseId", "dbo.Course");
            DropForeignKey("dbo.StashedNarratives", "LessonId", "dbo.Lesson");
            DropForeignKey("dbo.StashedLessonPlans", "LessonId", "dbo.Lesson");
            DropForeignKey("dbo.Lesson", "PredecessorLessonId", "dbo.Lesson");
            DropForeignKey("dbo.Lesson", "MirrorTargetLessonId", "dbo.Lesson");
            DropForeignKey("dbo.MessageUse", "LessonId", "dbo.Lesson");
            DropForeignKey("dbo.Lesson", "MasterLessonId", "dbo.Lesson");
            DropForeignKey("dbo.LessonTagUse", "LessonId", "dbo.Lesson");
            DropForeignKey("dbo.DocumentUse", "LessonId", "dbo.Lesson");
            DropForeignKey("dbo.ReleasedDocuments", "DocumentId", "dbo.Document");
            DropForeignKey("dbo.ReleasedDocuments", "LessonUseId", "dbo.LessonUse");
            DropForeignKey("dbo.LessonUse", "LessonId", "dbo.Lesson");
            DropForeignKey("dbo.Document", "OriginalDocumentId", "dbo.Document");
            DropForeignKey("dbo.DocumentUse", "DocumentId", "dbo.Document");
            DropForeignKey("dbo.DocumentLink", "DocumentId", "dbo.Document");
            DropForeignKey("dbo.Message", "ThreadParentId", "dbo.Message");
            DropForeignKey("dbo.MessageUse", "MessageId", "dbo.Message");
            DropForeignKey("dbo.LessonPlanLink", "MessageId", "dbo.Message");
            DropForeignKey("dbo.User", "UserPreferenceId", "dbo.UserPreferences");
            DropForeignKey("dbo.TagSet", "UserId", "dbo.User");
            DropForeignKey("dbo.Tag", "TagSetId", "dbo.TagSet");
            DropForeignKey("dbo.LessonTagUse", "TagId", "dbo.Tag");
            DropForeignKey("dbo.MetaCourse", "TagSetId", "dbo.TagSet");
            DropForeignKey("dbo.Templates", "TemplateSetId", "dbo.TemplateSets");
            DropForeignKey("dbo.MetaCourse", "TemplateSetId", "dbo.TemplateSets");
            DropForeignKey("dbo.MetaLesson", "MetaCourseId", "dbo.MetaCourse");
            DropForeignKey("dbo.Lesson", "MetaLessonId", "dbo.MetaLesson");
            DropForeignKey("dbo.Course", "MetaCourseId", "dbo.MetaCourse");
            DropForeignKey("dbo.Narrative", "ModifiedByUserId", "dbo.User");
            DropForeignKey("dbo.StashedNarratives", "NarrativeId", "dbo.Narrative");
            DropForeignKey("dbo.Lesson", "NarrativeId", "dbo.Narrative");
            DropForeignKey("dbo.Narrative", "MasterNarrativeId", "dbo.Narrative");
            DropForeignKey("dbo.Message", "AuthorId", "dbo.User");
            DropForeignKey("dbo.LessonPlan", "ModifiedByUserId", "dbo.User");
            DropForeignKey("dbo.Document", "ModifiedByUserId", "dbo.User");
            DropForeignKey("dbo.Course", "UserId", "dbo.User");
            DropForeignKey("dbo.StashedLessonPlans", "LessonPlanId", "dbo.LessonPlan");
            DropForeignKey("dbo.Lesson", "LessonPlanId", "dbo.LessonPlan");
            DropForeignKey("dbo.LessonPlanLink", "LessonPlanId", "dbo.LessonPlan");
            DropForeignKey("dbo.DocumentLink", "MessageId", "dbo.Message");
            DropForeignKey("dbo.Lesson", "ContainerLessonId", "dbo.Lesson");
            DropForeignKey("dbo.Course", "CoursePreferenceId", "dbo.CoursePreferences");
            DropForeignKey("dbo.ClassSection", "CourseId", "dbo.Course");
            DropForeignKey("dbo.ClassSectionStudents", "StudentUserId", "dbo.StudentUsers");
            DropForeignKey("dbo.ClassMeeting", "ClassSectionId", "dbo.ClassSection");
            DropIndex("dbo.ReferenceCalendar", new[] { "ReferenceClassSectionId" });
            DropIndex("dbo.ReferenceCalendar", new[] { "ClassSectionId" });
            DropIndex("dbo.Holiday", new[] { "TermId" });
            DropIndex("dbo.Term", new[] { "WorkingGroupId" });
            DropIndex("dbo.LessonUse", new[] { "LessonId" });
            DropIndex("dbo.LessonUse", new[] { "ClassMeetingId" });
            DropIndex("dbo.ReleasedDocuments", new[] { "LessonUseId" });
            DropIndex("dbo.ReleasedDocuments", new[] { "DocumentId" });
            DropIndex("dbo.MessageUse", new[] { "LessonId" });
            DropIndex("dbo.MessageUse", new[] { "MessageId" });
            DropIndex("dbo.LessonTagUse", new[] { "LessonId" });
            DropIndex("dbo.LessonTagUse", new[] { "TagId" });
            DropIndex("dbo.Tag", new[] { "TagSetId" });
            DropIndex("dbo.Templates", new[] { "TemplateSetId" });
            DropIndex("dbo.TemplateSets", new[] { "WorkingGroupId" });
            DropIndex("dbo.MetaLesson", new[] { "MetaCourseId" });
            DropIndex("dbo.MetaCourse", new[] { "TemplateSetId" });
            DropIndex("dbo.MetaCourse", new[] { "TagSetId" });
            DropIndex("dbo.MetaCourse", new[] { "WorkingGroupId" });
            DropIndex("dbo.TagSet", new[] { "UserId" });
            DropIndex("dbo.TagSet", new[] { "WorkingGroupId" });
            DropIndex("dbo.StashedNarratives", new[] { "NarrativeId" });
            DropIndex("dbo.StashedNarratives", new[] { "LessonId" });
            DropIndex("dbo.Narrative", new[] { "MasterNarrativeId" });
            DropIndex("dbo.Narrative", new[] { "ModifiedByUserId" });
            DropIndex("dbo.User", new[] { "UserPreferenceId" });
            DropIndex("dbo.User", new[] { "WorkingGroupId" });
            DropIndex("dbo.StashedLessonPlans", new[] { "LessonPlanId" });
            DropIndex("dbo.StashedLessonPlans", new[] { "LessonId" });
            DropIndex("dbo.LessonPlan", new[] { "ModifiedByUserId" });
            DropIndex("dbo.LessonPlanLink", new[] { "MessageId" });
            DropIndex("dbo.LessonPlanLink", new[] { "LessonPlanId" });
            DropIndex("dbo.Message", new[] { "ThreadParentId" });
            DropIndex("dbo.Message", new[] { "AuthorId" });
            DropIndex("dbo.DocumentLink", new[] { "DocumentId" });
            DropIndex("dbo.DocumentLink", new[] { "MessageId" });
            DropIndex("dbo.Document", new[] { "OriginalDocumentId" });
            DropIndex("dbo.Document", new[] { "ModifiedByUserId" });
            DropIndex("dbo.DocumentUse", new[] { "LessonId" });
            DropIndex("dbo.DocumentUse", new[] { "DocumentId" });
            DropIndex("dbo.Lesson", new[] { "NarrativeId" });
            DropIndex("dbo.Lesson", new[] { "LessonPlanId" });
            DropIndex("dbo.Lesson", new[] { "MetaLessonId" });
            DropIndex("dbo.Lesson", new[] { "MirrorTargetLessonId" });
            DropIndex("dbo.Lesson", new[] { "PredecessorLessonId" });
            DropIndex("dbo.Lesson", new[] { "MasterLessonId" });
            DropIndex("dbo.Lesson", new[] { "ContainerLessonId" });
            DropIndex("dbo.Lesson", new[] { "CourseId" });
            DropIndex("dbo.Course", new[] { "PredecessorCourseId" });
            DropIndex("dbo.Course", new[] { "MirrorTargetCourseId" });
            DropIndex("dbo.Course", new[] { "MasterCourseId" });
            DropIndex("dbo.Course", new[] { "MetaCourseId" });
            DropIndex("dbo.Course", new[] { "UserId" });
            DropIndex("dbo.Course", new[] { "TermId" });
            DropIndex("dbo.Course", new[] { "CoursePreferenceId" });
            DropIndex("dbo.Course", new[] { "WorkingGroupId" });
            DropIndex("dbo.WorkingGroup", new[] { "WorkingGroupPreferenceId" });
            DropIndex("dbo.WorkingGroupStudents", new[] { "StudentUserId" });
            DropIndex("dbo.WorkingGroupStudents", new[] { "WorkingGroupId" });
            DropIndex("dbo.ClassSectionStudents", new[] { "StudentUserId" });
            DropIndex("dbo.ClassSectionStudents", new[] { "ClassSectionId" });
            DropIndex("dbo.ClassSection", new[] { "MirrorTargetClassSectionId" });
            DropIndex("dbo.ClassSection", new[] { "CourseId" });
            DropIndex("dbo.ClassMeeting", new[] { "ClassSectionId" });
            DropTable("dbo.ReferenceCalendar");
            DropTable("dbo.WorkingGroupPreferences");
            DropTable("dbo.Holiday");
            DropTable("dbo.Term");
            DropTable("dbo.LessonUse");
            DropTable("dbo.ReleasedDocuments");
            DropTable("dbo.MessageUse");
            DropTable("dbo.UserPreferences");
            DropTable("dbo.LessonTagUse");
            DropTable("dbo.Tag");
            DropTable("dbo.Templates");
            DropTable("dbo.TemplateSets");
            DropTable("dbo.MetaLesson");
            DropTable("dbo.MetaCourse");
            DropTable("dbo.TagSet");
            DropTable("dbo.StashedNarratives");
            DropTable("dbo.Narrative");
            DropTable("dbo.User");
            DropTable("dbo.StashedLessonPlans");
            DropTable("dbo.LessonPlan");
            DropTable("dbo.LessonPlanLink");
            DropTable("dbo.Message");
            DropTable("dbo.DocumentLink");
            DropTable("dbo.Document");
            DropTable("dbo.DocumentUse");
            DropTable("dbo.Lesson");
            DropTable("dbo.CoursePreferences");
            DropTable("dbo.Course");
            DropTable("dbo.WorkingGroup");
            DropTable("dbo.WorkingGroupStudents");
            DropTable("dbo.StudentUsers");
            DropTable("dbo.ClassSectionStudents");
            DropTable("dbo.ClassSection");
            DropTable("dbo.ClassMeeting");
        }
    }
}
