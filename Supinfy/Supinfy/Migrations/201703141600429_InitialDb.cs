namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        InitiatorId = c.Guid(nullable: false),
                        TargetId = c.Guid(nullable: false),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.InitiatorId)
                .ForeignKey("dbo.Users", t => t.TargetId)
                .Index(t => t.InitiatorId)
                .Index(t => t.TargetId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Nickname = c.String(maxLength: 450),
                        Password = c.String(),
                        Email = c.String(maxLength: 450),
                        CreationDateTime = c.DateTime(nullable: false),
                        Role = c.Int(nullable: false),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Nickname, unique: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.User_Id);
            
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
                "dbo.MusicPlaylists",
                c => new
                    {
                        Music_Id = c.Guid(nullable: false),
                        Playlist_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Music_Id, t.Playlist_Id })
                .ForeignKey("dbo.Musics", t => t.Music_Id, cascadeDelete: true)
                .ForeignKey("dbo.Playlists", t => t.Playlist_Id, cascadeDelete: true)
                .Index(t => t.Music_Id)
                .Index(t => t.Playlist_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendRequests", "TargetId", "dbo.Users");
            DropForeignKey("dbo.FriendRequests", "InitiatorId", "dbo.Users");
            DropForeignKey("dbo.Playlists", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.MusicPlaylists", "Playlist_Id", "dbo.Playlists");
            DropForeignKey("dbo.MusicPlaylists", "Music_Id", "dbo.Musics");
            DropForeignKey("dbo.Users", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FriendRequests", "User_Id", "dbo.Users");
            DropIndex("dbo.MusicPlaylists", new[] { "Playlist_Id" });
            DropIndex("dbo.MusicPlaylists", new[] { "Music_Id" });
            DropIndex("dbo.Playlists", new[] { "OwnerId" });
            DropIndex("dbo.Users", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Nickname" });
            DropIndex("dbo.FriendRequests", new[] { "User_Id" });
            DropIndex("dbo.FriendRequests", new[] { "TargetId" });
            DropIndex("dbo.FriendRequests", new[] { "InitiatorId" });
            DropTable("dbo.MusicPlaylists");
            DropTable("dbo.Musics");
            DropTable("dbo.Playlists");
            DropTable("dbo.Users");
            DropTable("dbo.FriendRequests");
        }
    }
}
