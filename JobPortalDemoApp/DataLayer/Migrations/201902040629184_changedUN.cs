namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedUN : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserNotifications");
            AddColumn("dbo.UserNotifications", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserNotifications", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserNotifications");
            DropColumn("dbo.UserNotifications", "Id");
            AddPrimaryKey("dbo.UserNotifications", new[] { "UserId", "NotificationId" });
        }
    }
}
