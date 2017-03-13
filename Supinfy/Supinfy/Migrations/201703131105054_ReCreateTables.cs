namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReCreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Musics",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
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
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
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
            
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "CreationDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Role", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Nickname", c => c.String(maxLength: 450));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 450));
            CreateIndex("dbo.Users", "Nickname", unique: true);
            CreateIndex("dbo.Users", "Email", unique: true);
            DropColumn("dbo.Users", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            DropForeignKey("dbo.Playlists", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.PlaylistMusics", "Music_Id", "dbo.Musics");
            DropForeignKey("dbo.PlaylistMusics", "Playlist_Id", "dbo.Playlists");
            DropIndex("dbo.PlaylistMusics", new[] { "Music_Id" });
            DropIndex("dbo.PlaylistMusics", new[] { "Playlist_Id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Nickname" });
            DropIndex("dbo.Playlists", new[] { "OwnerId" });
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Nickname", c => c.String());
            DropColumn("dbo.Users", "Role");
            DropColumn("dbo.Users", "CreationDateTime");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
            DropTable("dbo.PlaylistMusics");
            DropTable("dbo.Playlists");
            DropTable("dbo.Musics");
        }
    }
}
