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
                        Id = c.Guid(nullable: false, identity: true),
                        Country = c.String(),
                        City = c.String(),
                        Area = c.String(),
                        Street = c.String(),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PlaceId = c.Guid(),
                        UserId = c.String(maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                        NumberOfPeople = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PlaceId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Consumables",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PlaceId = c.Guid(),
                        Name = c.String(),
                        Type = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        Ingredients = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AddressId = c.Guid(),
                        ManagerId = c.String(maxLength: 128),
                        Name = c.String(),
                        Type = c.String(),
                        PhotoUrl = c.String(),
                        Contact = c.String(),
                        WeekendHours = c.String(),
                        WeekdayHours = c.String(),
                        Details = c.String(),
                        AverageBill = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerId)
                .Index(t => t.AddressId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PlaceId = c.Guid(),
                        UserId = c.String(maxLength: 128),
                        Message = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PlaceId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PlaceId = c.Guid(),
                        NumberOfPeople = c.Int(nullable: false),
                        NumberOfTables = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.BookedTables", "TableId", "dbo.Tables");
            DropForeignKey("dbo.BookedTables", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Consumables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Bookings", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ConsumableBookings", "Booking_Id", "dbo.Bookings");
            DropForeignKey("dbo.ConsumableBookings", "Consumable_Id", "dbo.Consumables");
            DropIndex("dbo.ConsumableBookings", new[] { "Booking_Id" });
            DropIndex("dbo.ConsumableBookings", new[] { "Consumable_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tables", new[] { "PlaceId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "PlaceId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Places", new[] { "ManagerId" });
            DropIndex("dbo.Places", new[] { "AddressId" });
            DropIndex("dbo.Consumables", new[] { "PlaceId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "PlaceId" });
            DropIndex("dbo.BookedTables", new[] { "BookingId" });
            DropIndex("dbo.BookedTables", new[] { "TableId" });
            DropTable("dbo.ConsumableBookings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tables");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Places");
            DropTable("dbo.Consumables");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookedTables");
            DropTable("dbo.Addresses");
        }
    }
}
