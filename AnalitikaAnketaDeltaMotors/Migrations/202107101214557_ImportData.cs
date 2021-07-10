namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImportData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Entries", "ImportData_Id", c => c.Int());
            AddColumn("dbo.Tags", "ImportData_Id", c => c.Int());
            CreateIndex("dbo.Entries", "ImportData_Id");
            CreateIndex("dbo.Tags", "ImportData_Id");
            AddForeignKey("dbo.Entries", "ImportData_Id", "dbo.ImportDatas", "Id");
            AddForeignKey("dbo.Tags", "ImportData_Id", "dbo.ImportDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "ImportData_Id", "dbo.ImportDatas");
            DropForeignKey("dbo.Entries", "ImportData_Id", "dbo.ImportDatas");
            DropIndex("dbo.Tags", new[] { "ImportData_Id" });
            DropIndex("dbo.Entries", new[] { "ImportData_Id" });
            DropColumn("dbo.Tags", "ImportData_Id");
            DropColumn("dbo.Entries", "ImportData_Id");
            DropTable("dbo.ImportDatas");
        }
    }
}
