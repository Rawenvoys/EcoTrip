namespace SamochodElektryczny.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "Name", c => c.String());
            AddColumn("dbo.Places", "Lat", c => c.Single(nullable: false));
            AddColumn("dbo.Places", "Lng", c => c.Single(nullable: false));
            AddColumn("dbo.Places", "Rate", c => c.Double(nullable: false));
            AddColumn("dbo.Places", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "Type");
            DropColumn("dbo.Places", "Rate");
            DropColumn("dbo.Places", "Lng");
            DropColumn("dbo.Places", "Lat");
            DropColumn("dbo.Places", "Name");
        }
    }
}
