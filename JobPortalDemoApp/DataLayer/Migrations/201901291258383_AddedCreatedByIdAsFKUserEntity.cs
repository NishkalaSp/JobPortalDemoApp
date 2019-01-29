namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatedByIdAsFKUserEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobPosts", "CreatedBy_Id", "dbo.Users");
            DropIndex("dbo.JobPosts", new[] { "CreatedBy_Id" });
            RenameColumn(table: "dbo.JobPosts", name: "CreatedBy_Id", newName: "CreatedById");
            AlterColumn("dbo.JobPosts", "CreatedById", c => c.Int(nullable: false));
            CreateIndex("dbo.JobPosts", "CreatedById");
            AddForeignKey("dbo.JobPosts", "CreatedById", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobPosts", "CreatedById", "dbo.Users");
            DropIndex("dbo.JobPosts", new[] { "CreatedById" });
            AlterColumn("dbo.JobPosts", "CreatedById", c => c.Int());
            RenameColumn(table: "dbo.JobPosts", name: "CreatedById", newName: "CreatedBy_Id");
            CreateIndex("dbo.JobPosts", "CreatedBy_Id");
            AddForeignKey("dbo.JobPosts", "CreatedBy_Id", "dbo.Users", "Id");
        }
    }
}
