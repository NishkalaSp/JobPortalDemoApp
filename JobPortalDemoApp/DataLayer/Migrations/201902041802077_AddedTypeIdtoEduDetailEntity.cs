namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeIdtoEduDetailEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationDetails", "EducationTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.EducationTypes", "Name", c => c.String());
            CreateIndex("dbo.EducationDetails", "EducationTypeId");
            AddForeignKey("dbo.EducationDetails", "EducationTypeId", "dbo.EducationTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.EducationDetails", "Type");
            DropColumn("dbo.EducationTypes", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationTypes", "Type", c => c.String());
            AddColumn("dbo.EducationDetails", "Type", c => c.String());
            DropForeignKey("dbo.EducationDetails", "EducationTypeId", "dbo.EducationTypes");
            DropIndex("dbo.EducationDetails", new[] { "EducationTypeId" });
            DropColumn("dbo.EducationTypes", "Name");
            DropColumn("dbo.EducationDetails", "EducationTypeId");
        }
    }
}
