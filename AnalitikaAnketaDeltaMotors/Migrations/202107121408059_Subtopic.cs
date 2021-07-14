namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subtopic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subtopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subtopics");
        }
    }
}
