namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDateField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Musics", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Playlists", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Playlists", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Musics", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
