namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsAdministratorOnUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsAdministrator", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Surname", c => c.String());
            DropColumn("dbo.Users", "IsAdministrator");
        }
    }
}
