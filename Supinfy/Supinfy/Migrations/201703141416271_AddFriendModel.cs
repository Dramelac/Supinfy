namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlaylistMusics", newName: "MusicPlaylists");
            DropPrimaryKey("dbo.MusicPlaylists");
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        User1_Id = c.Guid(),
                        User2_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1_Id)
                .ForeignKey("dbo.Users", t => t.User2_Id)
                .Index(t => t.User1_Id)
                .Index(t => t.User2_Id);
            
            AddPrimaryKey("dbo.MusicPlaylists", new[] { "Music_Id", "Playlist_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "User2_Id", "dbo.Users");
            DropForeignKey("dbo.Friends", "User1_Id", "dbo.Users");
            DropIndex("dbo.Friends", new[] { "User2_Id" });
            DropIndex("dbo.Friends", new[] { "User1_Id" });
            DropPrimaryKey("dbo.MusicPlaylists");
            DropTable("dbo.Friends");
            AddPrimaryKey("dbo.MusicPlaylists", new[] { "Playlist_Id", "Music_Id" });
            RenameTable(name: "dbo.MusicPlaylists", newName: "PlaylistMusics");
        }
    }
}
