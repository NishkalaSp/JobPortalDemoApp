namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModifiedPropToJobPostEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPosts", "ModifiedById", c => c.Int());
            AddColumn("dbo.JobPosts", "ModifiedOn", c => c.DateTime());
            CreateIndex("dbo.JobPosts", "ModifiedById");
            AddForeignKey("dbo.JobPosts", "ModifiedById", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobPosts", "ModifiedById", "dbo.Users");
            DropIndex("dbo.JobPosts", new[] { "ModifiedById" });
            DropColumn("dbo.JobPosts", "ModifiedOn");
            DropColumn("dbo.JobPosts", "ModifiedById");
        }
    }
}
