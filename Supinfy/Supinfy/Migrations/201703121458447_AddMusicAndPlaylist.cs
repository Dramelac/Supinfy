namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMusicAndPlaylist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Musics",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        TrackId = c.Int(nullable: false),
                        ArtworkUrl = c.String(),
                        PlayCount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlaylistMusics",
                c => new
                    {
                        Playlist_Id = c.Guid(nullable: false),
                        Music_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Playlist_Id, t.Music_Id })
                .ForeignKey("dbo.Playlists", t => t.Playlist_Id, cascadeDelete: true)
                .ForeignKey("dbo.Musics", t => t.Music_Id, cascadeDelete: true)
                .Index(t => t.Playlist_Id)
                .Index(t => t.Music_Id);
            
            AddColumn("dbo.Users", "Playlist_Id", c => c.Guid());
            CreateIndex("dbo.Users", "Playlist_Id");
            AddForeignKey("dbo.Users", "Playlist_Id", "dbo.Playlists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Playlist_Id", "dbo.Playlists");
            DropForeignKey("dbo.PlaylistMusics", "Music_Id", "dbo.Musics");
            DropForeignKey("dbo.PlaylistMusics", "Playlist_Id", "dbo.Playlists");
            DropIndex("dbo.PlaylistMusics", new[] { "Music_Id" });
            DropIndex("dbo.PlaylistMusics", new[] { "Playlist_Id" });
            DropIndex("dbo.Users", new[] { "Playlist_Id" });
            DropColumn("dbo.Users", "Playlist_Id");
            DropTable("dbo.PlaylistMusics");
            DropTable("dbo.Playlists");
            DropTable("dbo.Musics");
        }
    }
}
