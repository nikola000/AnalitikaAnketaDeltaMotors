namespace AnalitikaAnketaDeltaMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "ImportData_Id", "dbo.ImportDatas");
            DropIndex("dbo.Tags", new[] { "ImportData_Id" });
            CreateTable(
                "dbo.TagImportDatas",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        ImportData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.ImportData_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.ImportDatas", t => t.ImportData_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.ImportData_Id);
            
            DropColumn("dbo.Tags", "ImportData_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "ImportData_Id", c => c.Int());
            DropForeignKey("dbo.TagImportDatas", "ImportData_Id", "dbo.ImportDatas");
            DropForeignKey("dbo.TagImportDatas", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagImportDatas", new[] { "ImportData_Id" });
            DropIndex("dbo.TagImportDatas", new[] { "Tag_Id" });
            DropTable("dbo.TagImportDatas");
            CreateIndex("dbo.Tags", "ImportData_Id");
            AddForeignKey("dbo.Tags", "ImportData_Id", "dbo.ImportDatas", "Id");
        }
    }
}
