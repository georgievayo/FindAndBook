namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoUrlToPlace : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Tables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Menus", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Addresses", "PlaceId", "dbo.Places");
            DropPrimaryKey("dbo.Places");
            AddColumn("dbo.Places", "PhotoUrl", c => c.String());
            AlterColumn("dbo.Places", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Places", "Id");
            AddForeignKey("dbo.Bookings", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Tables", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Menus", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Reviews", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Addresses", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Menus", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Tables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Bookings", "PlaceId", "dbo.Places");
            DropPrimaryKey("dbo.Places");
            AlterColumn("dbo.Places", "Id", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.Places", "PhotoUrl");
            AddPrimaryKey("dbo.Places", "Id");
            AddForeignKey("dbo.Addresses", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Menus", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Tables", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Bookings", "PlaceId", "dbo.Places", "Id");
        }
    }
}
