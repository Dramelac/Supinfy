namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlaylistUserRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Playlist_Id", "dbo.Playlists");
            DropIndex("dbo.Users", new[] { "Playlist_Id" });
            AddColumn("dbo.Playlists", "User_Id", c => c.Guid());
            CreateIndex("dbo.Playlists", "User_Id");
            AddForeignKey("dbo.Playlists", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Users", "Playlist_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Playlist_Id", c => c.Guid());
            DropForeignKey("dbo.Playlists", "User_Id", "dbo.Users");
            DropIndex("dbo.Playlists", new[] { "User_Id" });
            DropColumn("dbo.Playlists", "User_Id");
            CreateIndex("dbo.Users", "Playlist_Id");
            AddForeignKey("dbo.Users", "Playlist_Id", "dbo.Playlists", "Id");
        }
    }
}
