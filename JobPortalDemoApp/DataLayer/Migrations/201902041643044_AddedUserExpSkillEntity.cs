namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserExpSkillEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkillExperienceDetails", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.SkillExperienceDetails", "ExperienceDetails_Id", "dbo.ExperienceDetails");
            DropIndex("dbo.SkillExperienceDetails", new[] { "Skill_Id" });
            DropIndex("dbo.SkillExperienceDetails", new[] { "ExperienceDetails_Id" });
            CreateTable(
                "dbo.UserExperienceSkills",
                c => new
                    {
                        SeekerId = c.Int(nullable: false),
                        ExperienceDetailId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeekerId, t.ExperienceDetailId, t.SkillId })
                .ForeignKey("dbo.ExperienceDetails", t => t.ExperienceDetailId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SeekerId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.SeekerId)
                .Index(t => t.ExperienceDetailId)
                .Index(t => t.SkillId);
            
            DropTable("dbo.SkillExperienceDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SkillExperienceDetails",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        ExperienceDetails_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.ExperienceDetails_Id });
            
            DropForeignKey("dbo.UserExperienceSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.UserExperienceSkills", "SeekerId", "dbo.Users");
            DropForeignKey("dbo.UserExperienceSkills", "ExperienceDetailId", "dbo.ExperienceDetails");
            DropIndex("dbo.UserExperienceSkills", new[] { "SkillId" });
            DropIndex("dbo.UserExperienceSkills", new[] { "ExperienceDetailId" });
            DropIndex("dbo.UserExperienceSkills", new[] { "SeekerId" });
            DropTable("dbo.UserExperienceSkills");
            CreateIndex("dbo.SkillExperienceDetails", "ExperienceDetails_Id");
            CreateIndex("dbo.SkillExperienceDetails", "Skill_Id");
            AddForeignKey("dbo.SkillExperienceDetails", "ExperienceDetails_Id", "dbo.ExperienceDetails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SkillExperienceDetails", "Skill_Id", "dbo.Skills", "Id", cascadeDelete: true);
        }
    }
}
