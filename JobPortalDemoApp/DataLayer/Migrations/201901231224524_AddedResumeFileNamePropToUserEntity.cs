namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedResumeFileNamePropToUserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ResumeFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ResumeFileName");
        }
    }
}