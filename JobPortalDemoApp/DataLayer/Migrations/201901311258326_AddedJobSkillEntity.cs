namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJobSkillEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobSkills",
                c => new
                    {
                        JobPostId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobPostId, t.SkillId })
                .ForeignKey("dbo.JobPosts", t => t.JobPostId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.JobPostId)
                .Index(t => t.SkillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.JobSkills", "JobPostId", "dbo.JobPosts");
            DropIndex("dbo.JobSkills", new[] { "SkillId" });
            DropIndex("dbo.JobSkills", new[] { "JobPostId" });
            DropTable("dbo.JobSkills");
        }
    }
}
