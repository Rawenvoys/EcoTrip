namespace SamochodElektryczny.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        PlaceID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PlaceID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Places");
        }
    }
}
