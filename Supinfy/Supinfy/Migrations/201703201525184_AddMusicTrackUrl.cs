namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMusicTrackUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "TrackUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musics", "TrackUrl");
        }
    }
}
