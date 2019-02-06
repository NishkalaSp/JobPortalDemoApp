namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJobTypeToJobPostEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPosts", "JobTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.JobPosts", "JobTypeId");
            AddForeignKey("dbo.JobPosts", "JobTypeId", "dbo.JobTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobPosts", "JobTypeId", "dbo.JobTypes");
            DropIndex("dbo.JobPosts", new[] { "JobTypeId" });
            DropColumn("dbo.JobPosts", "JobTypeId");
        }
    }
}
