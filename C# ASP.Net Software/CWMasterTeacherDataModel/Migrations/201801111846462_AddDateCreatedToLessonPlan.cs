namespace CWMasterTeacherDataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateCreatedToLessonPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LessonPlan", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LessonPlan", "DateCreated");
        }
    }
}
