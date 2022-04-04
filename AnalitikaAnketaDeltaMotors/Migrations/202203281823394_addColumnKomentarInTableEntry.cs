namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnKomentarInTableEntry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entries", "Komentar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Entries", "Komentar");
        }
    }
}
