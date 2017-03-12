namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Playlists", "User_Id", "dbo.Users");
            DropIndex("dbo.Playlists", new[] { "User_Id" });
            RenameColumn(table: "dbo.Playlists", name: "User_Id", newName: "OwnerId");
            AlterColumn("dbo.Playlists", "OwnerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Playlists", "OwnerId");
            AddForeignKey("dbo.Playlists", "OwnerId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Playlists", "OwnerId", "dbo.Users");
            DropIndex("dbo.Playlists", new[] { "OwnerId" });
            AlterColumn("dbo.Playlists", "OwnerId", c => c.Guid());
            RenameColumn(table: "dbo.Playlists", name: "OwnerId", newName: "User_Id");
            CreateIndex("dbo.Playlists", "User_Id");
            AddForeignKey("dbo.Playlists", "User_Id", "dbo.Users", "Id");
        }
    }
}
