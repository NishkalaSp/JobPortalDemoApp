namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJobPostActivityEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPostActivities",
                c => new
                    {
                        JobPostId = c.Int(nullable: false),
                        SeekerId = c.Int(nullable: false),
                        AppliedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobPostId, t.SeekerId })
                .ForeignKey("dbo.JobPosts", t => t.JobPostId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SeekerId)
                .Index(t => t.JobPostId)
                .Index(t => t.SeekerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobPostActivities", "SeekerId", "dbo.Users");
            DropForeignKey("dbo.JobPostActivities", "JobPostId", "dbo.JobPosts");
            DropIndex("dbo.JobPostActivities", new[] { "SeekerId" });
            DropIndex("dbo.JobPostActivities", new[] { "JobPostId" });
            DropTable("dbo.JobPostActivities");
        }
    }
}
