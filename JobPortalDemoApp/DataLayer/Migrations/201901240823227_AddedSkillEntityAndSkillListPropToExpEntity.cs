namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSkillEntityAndSkillListPropToExpEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ExperienceDetails_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExperienceDetails", t => t.ExperienceDetails_Id)
                .Index(t => t.ExperienceDetails_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "ExperienceDetails_Id", "dbo.ExperienceDetails");
            DropIndex("dbo.Skills", new[] { "ExperienceDetails_Id" });
            DropTable("dbo.Skills");
        }
    }
}
