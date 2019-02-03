namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotificationAndUNEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        JobPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobPosts", t => t.JobPostId, cascadeDelete: true)
                .Index(t => t.JobPostId);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        NotificationId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NotificationId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.NotificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "JobPostId", "dbo.JobPosts");
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "JobPostId" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
