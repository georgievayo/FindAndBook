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
                        Id = c.Guid(nullable: false),
                        PlaceId = c.Guid(nullable: false),
                        Country = c.String(nullable: false, maxLength: 20),
                        City = c.String(nullable: false, maxLength: 20),
                        Area = c.String(maxLength: 20),
                        Street = c.String(nullable: false, maxLength: 20),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ManagerId = c.String(maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 30),
                        Contact = c.String(maxLength: 10),
                        WeekendHours = c.String(maxLength: 20),
                        WeekdayHours = c.String(maxLength: 20),
                        Details = c.String(),
                        AverageBill = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlaceId = c.Guid(),
                        UserId = c.String(maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                        NumberOfPeople = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PlaceId)
                .Index(t => t.UserId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.BookedTables",
                c => new
                    {
                        BookingId = c.Guid(nullable: false),
                        TableId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookingId, t.TableId })
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.TableId, cascadeDelete: true)
                .Index(t => t.BookingId)
                .Index(t => t.TableId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlaceId = c.Guid(),
                        NumberOfPeople = c.Int(nullable: false),
                        NumberOfTables = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                        Id = c.Guid(nullable: false),
                        PlaceId = c.Guid(),
                        UserId = c.Guid(),
                        Message = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.PlaceId)
                .Index(t => t.User_Id);
            
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
                "dbo.Menus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlaceId = c.Guid(),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        Ingredients = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.OrderedConsumables",
                c => new
                    {
                        ConsumableId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConsumableId, t.OrderId })
                .ForeignKey("dbo.Menus", t => t.ConsumableId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.ConsumableId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BookingId = c.Guid(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Addresses", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Menus", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.OrderedConsumables", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.OrderedConsumables", "ConsumableId", "dbo.Menus");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookedTables", "TableId", "dbo.Tables");
            DropForeignKey("dbo.Tables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.BookedTables", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "PlaceId", "dbo.Places");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Orders", new[] { "BookingId" });
            DropIndex("dbo.OrderedConsumables", new[] { "OrderId" });
            DropIndex("dbo.OrderedConsumables", new[] { "ConsumableId" });
            DropIndex("dbo.Menus", new[] { "PlaceId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "PlaceId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Tables", new[] { "PlaceId" });
            DropIndex("dbo.BookedTables", new[] { "TableId" });
            DropIndex("dbo.BookedTables", new[] { "BookingId" });
            DropIndex("dbo.Bookings", new[] { "User_Id" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "ManagerId" });
            DropIndex("dbo.Addresses", new[] { "PlaceId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderedConsumables");
            DropTable("dbo.Menus");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tables");
            DropTable("dbo.BookedTables");
            DropTable("dbo.Bookings");
            DropTable("dbo.Places");
            DropTable("dbo.Addresses");
        }
    }
}
