namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOrderTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.OrderConsumables", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderConsumables", "Consumable_Id", "dbo.Consumables");
            DropIndex("dbo.Orders", new[] { "BookingId" });
            DropIndex("dbo.OrderConsumables", new[] { "Order_Id" });
            DropIndex("dbo.OrderConsumables", new[] { "Consumable_Id" });
            CreateTable(
                "dbo.ConsumableBookings",
                c => new
                    {
                        Consumable_Id = c.Guid(nullable: false),
                        Booking_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Consumable_Id, t.Booking_Id })
                .ForeignKey("dbo.Consumables", t => t.Consumable_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.Booking_Id, cascadeDelete: true)
                .Index(t => t.Consumable_Id)
                .Index(t => t.Booking_Id);
            
            DropTable("dbo.Orders");
            DropTable("dbo.OrderConsumables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderConsumables",
                c => new
                    {
                        Order_Id = c.Guid(nullable: false),
                        Consumable_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Consumable_Id });
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        BookingId = c.Guid(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ConsumableBookings", "Booking_Id", "dbo.Bookings");
            DropForeignKey("dbo.ConsumableBookings", "Consumable_Id", "dbo.Consumables");
            DropIndex("dbo.ConsumableBookings", new[] { "Booking_Id" });
            DropIndex("dbo.ConsumableBookings", new[] { "Consumable_Id" });
            DropTable("dbo.ConsumableBookings");
            CreateIndex("dbo.OrderConsumables", "Consumable_Id");
            CreateIndex("dbo.OrderConsumables", "Order_Id");
            CreateIndex("dbo.Orders", "BookingId");
            AddForeignKey("dbo.OrderConsumables", "Consumable_Id", "dbo.Consumables", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderConsumables", "Order_Id", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "BookingId", "dbo.Bookings", "Id");
        }
    }
}
