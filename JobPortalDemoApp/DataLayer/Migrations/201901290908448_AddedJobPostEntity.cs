namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJobPostEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostedOn = c.DateTime(nullable: false),
                        JobBrief = c.String(),
                        Responsibilities = c.String(),
                        Requirements = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .Index(t => t.CreatedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobPosts", "CreatedBy_Id", "dbo.Users");
            DropIndex("dbo.JobPosts", new[] { "CreatedBy_Id" });
            DropTable("dbo.JobPosts");
        }
    }
}
