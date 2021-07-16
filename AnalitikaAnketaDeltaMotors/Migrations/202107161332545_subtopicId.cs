namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subtopicId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EntryScores", "Subtopic_Id", "dbo.Subtopics");
            DropIndex("dbo.EntryScores", new[] { "Subtopic_Id" });
            RenameColumn(table: "dbo.EntryScores", name: "Subtopic_Id", newName: "SubtopicId");
            AlterColumn("dbo.EntryScores", "SubtopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.EntryScores", "SubtopicId");
            AddForeignKey("dbo.EntryScores", "SubtopicId", "dbo.Subtopics", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EntryScores", "SubtopicId", "dbo.Subtopics");
            DropIndex("dbo.EntryScores", new[] { "SubtopicId" });
            AlterColumn("dbo.EntryScores", "SubtopicId", c => c.Int());
            RenameColumn(table: "dbo.EntryScores", name: "SubtopicId", newName: "Subtopic_Id");
            CreateIndex("dbo.EntryScores", "Subtopic_Id");
            AddForeignKey("dbo.EntryScores", "Subtopic_Id", "dbo.Subtopics", "Id");
        }
    }
}
