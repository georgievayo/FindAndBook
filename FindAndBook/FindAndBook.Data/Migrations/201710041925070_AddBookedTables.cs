namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookedTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TableBookings", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.TableBookings", "Booking_Id", "dbo.Bookings");
            DropIndex("dbo.TableBookings", new[] { "Table_Id" });
            DropIndex("dbo.TableBookings", new[] { "Booking_Id" });
            CreateTable(
                "dbo.BookedTables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TableId = c.Guid(),
                        BookingId = c.Guid(),
                        TablesCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .ForeignKey("dbo.Tables", t => t.TableId)
                .Index(t => t.TableId)
                .Index(t => t.BookingId);
            
            DropTable("dbo.TableBookings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TableBookings",
                c => new
                    {
                        Table_Id = c.Guid(nullable: false),
                        Booking_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Table_Id, t.Booking_Id });
            
            DropForeignKey("dbo.BookedTables", "TableId", "dbo.Tables");
            DropForeignKey("dbo.BookedTables", "BookingId", "dbo.Bookings");
            DropIndex("dbo.BookedTables", new[] { "BookingId" });
            DropIndex("dbo.BookedTables", new[] { "TableId" });
            DropTable("dbo.BookedTables");
            CreateIndex("dbo.TableBookings", "Booking_Id");
            CreateIndex("dbo.TableBookings", "Table_Id");
            AddForeignKey("dbo.TableBookings", "Booking_Id", "dbo.Bookings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TableBookings", "Table_Id", "dbo.Tables", "Id", cascadeDelete: true);
        }
    }
}
