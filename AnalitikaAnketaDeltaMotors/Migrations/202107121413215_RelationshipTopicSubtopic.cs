namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipTopicSubtopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subtopics", "TopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subtopics", "TopicId");
            AddForeignKey("dbo.Subtopics", "TopicId", "dbo.Topics", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subtopics", "TopicId", "dbo.Topics");
            DropIndex("dbo.Subtopics", new[] { "TopicId" });
            DropColumn("dbo.Subtopics", "TopicId");
        }
    }
}
