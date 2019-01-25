namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDOBPropToUserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DOB", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DOB");
        }
    }
}
