namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CertificateOrDegreeName = c.String(),
                        InstituteOrUniversityName = c.String(),
                        MajorBranch = c.String(),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Seeker_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Seeker_Id)
                .ForeignKey("dbo.EducationTypes", t => t.Type_Id)
                .Index(t => t.Seeker_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        ContactNumber = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EducationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExperienceDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        JobTitle = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Seeker_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Seeker_Id)
                .ForeignKey("dbo.JobTypes", t => t.Type_Id)
                .Index(t => t.Seeker_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.JobTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExperienceDetails", "Type_Id", "dbo.JobTypes");
            DropForeignKey("dbo.ExperienceDetails", "Seeker_Id", "dbo.Users");
            DropForeignKey("dbo.EducationDetails", "Type_Id", "dbo.EducationTypes");
            DropForeignKey("dbo.EducationDetails", "Seeker_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Type_Id", "dbo.UserTypes");
            DropIndex("dbo.ExperienceDetails", new[] { "Type_Id" });
            DropIndex("dbo.ExperienceDetails", new[] { "Seeker_Id" });
            DropIndex("dbo.Users", new[] { "Type_Id" });
            DropIndex("dbo.EducationDetails", new[] { "Type_Id" });
            DropIndex("dbo.EducationDetails", new[] { "Seeker_Id" });
            DropTable("dbo.JobTypes");
            DropTable("dbo.ExperienceDetails");
            DropTable("dbo.EducationTypes");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
            DropTable("dbo.EducationDetails");
        }
    }
}
