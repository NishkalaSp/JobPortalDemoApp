namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExpDetailsListAsPropToSkillTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillExperienceDetails",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        ExperienceDetails_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.ExperienceDetails_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.ExperienceDetails", t => t.ExperienceDetails_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.ExperienceDetails_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillExperienceDetails", "ExperienceDetails_Id", "dbo.ExperienceDetails");
            DropForeignKey("dbo.SkillExperienceDetails", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillExperienceDetails", new[] { "ExperienceDetails_Id" });
            DropIndex("dbo.SkillExperienceDetails", new[] { "Skill_Id" });
            DropTable("dbo.SkillExperienceDetails");
            DropTable("dbo.Skills");
        }
    }
}
