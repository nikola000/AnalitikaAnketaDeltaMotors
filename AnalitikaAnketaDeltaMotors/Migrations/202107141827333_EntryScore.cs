namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntryScore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntryScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntryId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        Subtopic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entries", t => t.EntryId, cascadeDelete: true)
                .ForeignKey("dbo.Subtopics", t => t.Subtopic_Id)
                .Index(t => t.EntryId)
                .Index(t => t.Subtopic_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EntryScores", "Subtopic_Id", "dbo.Subtopics");
            DropForeignKey("dbo.EntryScores", "EntryId", "dbo.Entries");
            DropIndex("dbo.EntryScores", new[] { "Subtopic_Id" });
            DropIndex("dbo.EntryScores", new[] { "EntryId" });
            DropTable("dbo.EntryScores");
        }
    }
}
