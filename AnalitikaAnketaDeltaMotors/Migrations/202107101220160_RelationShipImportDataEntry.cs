namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShipImportDataEntry : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entries", "ImportData_Id", "dbo.ImportDatas");
            DropIndex("dbo.Entries", new[] { "ImportData_Id" });
            RenameColumn(table: "dbo.Entries", name: "ImportData_Id", newName: "ImportDataId");
            AlterColumn("dbo.Entries", "ImportDataId", c => c.Int(nullable: false));
            CreateIndex("dbo.Entries", "ImportDataId");
            AddForeignKey("dbo.Entries", "ImportDataId", "dbo.ImportDatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entries", "ImportDataId", "dbo.ImportDatas");
            DropIndex("dbo.Entries", new[] { "ImportDataId" });
            AlterColumn("dbo.Entries", "ImportDataId", c => c.Int());
            RenameColumn(table: "dbo.Entries", name: "ImportDataId", newName: "ImportData_Id");
            CreateIndex("dbo.Entries", "ImportData_Id");
            AddForeignKey("dbo.Entries", "ImportData_Id", "dbo.ImportDatas", "Id");
        }
    }
}
