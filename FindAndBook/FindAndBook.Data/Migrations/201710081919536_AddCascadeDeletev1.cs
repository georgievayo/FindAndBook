namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCascadeDeletev1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "PlaceId", "dbo.Places");
            DropIndex("dbo.Bookings", new[] { "PlaceId" });
            AlterColumn("dbo.Bookings", "PlaceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Bookings", "PlaceId");
            AddForeignKey("dbo.Bookings", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "PlaceId", "dbo.Places");
            DropIndex("dbo.Bookings", new[] { "PlaceId" });
            AlterColumn("dbo.Bookings", "PlaceId", c => c.Guid());
            CreateIndex("dbo.Bookings", "PlaceId");
            AddForeignKey("dbo.Bookings", "PlaceId", "dbo.Places", "Id");
        }
    }
}
