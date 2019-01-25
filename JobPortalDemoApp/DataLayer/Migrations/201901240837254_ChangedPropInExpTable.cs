namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPropInExpTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "ExperienceDetails_Id", "dbo.ExperienceDetails");
            DropIndex("dbo.Skills", new[] { "ExperienceDetails_Id" });
            DropTable("dbo.Skills");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ExperienceDetails_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Skills", "ExperienceDetails_Id");
            AddForeignKey("dbo.Skills", "ExperienceDetails_Id", "dbo.ExperienceDetails", "Id");
        }
    }
}
