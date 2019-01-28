namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EducationDetails", "Type_Id", "dbo.EducationTypes");
            DropIndex("dbo.EducationDetails", new[] { "Type_Id" });
            AddColumn("dbo.EducationDetails", "HighestQualification", c => c.String());
            AddColumn("dbo.EducationDetails", "Type", c => c.String());
            DropColumn("dbo.EducationDetails", "CertificateOrDegreeName");
            DropColumn("dbo.EducationDetails", "Type_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationDetails", "Type_Id", c => c.Int());
            AddColumn("dbo.EducationDetails", "CertificateOrDegreeName", c => c.String());
            DropColumn("dbo.EducationDetails", "Type");
            DropColumn("dbo.EducationDetails", "HighestQualification");
            CreateIndex("dbo.EducationDetails", "Type_Id");
            AddForeignKey("dbo.EducationDetails", "Type_Id", "dbo.EducationTypes", "Id");
        }
    }
}
