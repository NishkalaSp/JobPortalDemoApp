namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserTypeToRoleInUserEntity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserTypes", newName: "Roles");
            RenameColumn(table: "dbo.Users", name: "Type_Id", newName: "Role_Id");
            RenameIndex(table: "dbo.Users", name: "IX_Type_Id", newName: "IX_Role_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_Role_Id", newName: "IX_Type_Id");
            RenameColumn(table: "dbo.Users", name: "Role_Id", newName: "Type_Id");
            RenameTable(name: "dbo.Roles", newName: "UserTypes");
        }
    }
}
