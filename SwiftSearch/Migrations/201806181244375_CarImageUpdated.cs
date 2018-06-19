namespace SwiftSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarImageUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "CarImage", c => c.String());
            DropColumn("dbo.Vehicles", "CarImageOne");
            DropColumn("dbo.Vehicles", "CarImageTwo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "CarImageTwo", c => c.String());
            AddColumn("dbo.Vehicles", "CarImageOne", c => c.String());
            DropColumn("dbo.Vehicles", "CarImage");
        }
    }
}
