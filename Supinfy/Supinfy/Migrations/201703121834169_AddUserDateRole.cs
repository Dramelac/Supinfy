namespace Supinfy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDateRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreationDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Role");
            DropColumn("dbo.Users", "CreationDateTime");
        }
    }
}
