namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMusicArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "Artist", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musics", "Artist");
        }
    }
}
