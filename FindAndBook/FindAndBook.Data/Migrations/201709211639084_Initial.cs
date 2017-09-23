namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        PlaceId = c.String(nullable: false, maxLength: 10),
                        Country = c.String(nullable: false, maxLength: 10),
                        City = c.String(nullable: false, maxLength: 10),
                        Street = c.String(nullable: false, maxLength: 10),
                        Number = c.Int(nullable: false),
                        Area = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => new { t.PlaceId, t.Country, t.City, t.Street, t.Number });
            
            CreateTable(
                "dbo.BookedTables",
                c => new
                    {
                        BookingId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        TableId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.BookingId, t.TableId });
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        PlaceId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        UserId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Date = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Time = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        NumberOfPeople = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.PlaceId, t.UserId, t.Date, t.Time, t.NumberOfPeople });
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        PlaceId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Name = c.String(maxLength: 20),
                        Type = c.String(maxLength: 10),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        Ingredients = c.String(),
                    })
                .PrimaryKey(t => t.PlaceId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        BookingId = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 20),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookingId, t.Name, t.Quantity });
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Contact = c.String(maxLength: 10),
                        WeekendHours = c.String(maxLength: 20),
                        WeekdayHours = c.String(maxLength: 20),
                        Details = c.String(),
                        AverageBill = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.Name });
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        PlaceId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        UserId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Review = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlaceId, t.UserId, t.Review, t.Rating });
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        PlaceId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        NumberOfPeople = c.Int(nullable: false),
                        NumberOfTables = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlaceId, t.NumberOfPeople, t.NumberOfTables });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tables");
            DropTable("dbo.Reviews");
            DropTable("dbo.Places");
            DropTable("dbo.Orders");
            DropTable("dbo.Menus");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookedTables");
            DropTable("dbo.Addresses");
        }
    }
}
