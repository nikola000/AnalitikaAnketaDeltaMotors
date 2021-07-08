namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipGroupTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "GroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tags", "GroupId");
            AddForeignKey("dbo.Tags", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "GroupId", "dbo.Groups");
            DropIndex("dbo.Tags", new[] { "GroupId" });
            DropColumn("dbo.Tags", "GroupId");
        }
    }
}
