namespace SwiftSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FurnitureUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Furnitures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FurnitureName = c.String(),
                        FurnitureModel = c.String(),
                        FurnitureColor = c.String(),
                        FurniturePrice = c.Int(nullable: false),
                        FurnitureDealer = c.String(),
                        FurnitureAddress = c.String(),
                        FurnitureImage = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Funitures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Funitures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FunitureName = c.String(),
                        FunitureModel = c.String(),
                        FunitureColor = c.String(),
                        FuniturePrice = c.Int(nullable: false),
                        FunitureDealer = c.String(),
                        FunitureAddress = c.String(),
                        FunitureImageOne = c.String(),
                        FunitureImageTwo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Furnitures");
        }
    }
}
