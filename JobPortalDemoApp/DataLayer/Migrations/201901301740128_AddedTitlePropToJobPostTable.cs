namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTitlePropToJobPostTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPosts", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobPosts", "Title");
        }
    }
}
