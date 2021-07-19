namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entryscoreuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntryScores", "UserId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntryScores", "UserId");
        }
    }
}
