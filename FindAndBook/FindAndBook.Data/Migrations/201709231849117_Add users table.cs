namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adduserstable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Addresses");
            DropPrimaryKey("dbo.BookedTables");
            DropPrimaryKey("dbo.Bookings");
            DropPrimaryKey("dbo.Menus");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Places");
            DropPrimaryKey("dbo.Reviews");
            DropPrimaryKey("dbo.Tables");
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.Int(nullable: false),
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.Addresses", "Place_Id", c => c.Guid());
            AddColumn("dbo.Addresses", "Place_Name", c => c.String(maxLength: 30));
            AddColumn("dbo.BookedTables", "Booking_Id", c => c.Guid());
            AddColumn("dbo.BookedTables", "Booking_PlaceId", c => c.Guid());
            AddColumn("dbo.BookedTables", "Booking_UserId", c => c.Guid());
            AddColumn("dbo.BookedTables", "Booking_Date", c => c.String(maxLength: 10, fixedLength: true));
            AddColumn("dbo.BookedTables", "Booking_Time", c => c.String(maxLength: 10, fixedLength: true));
            AddColumn("dbo.BookedTables", "Booking_NumberOfPeople", c => c.Int());
            AddColumn("dbo.Bookings", "Place_Id", c => c.Guid());
            AddColumn("dbo.Bookings", "Place_Name", c => c.String(maxLength: 30));
            AddColumn("dbo.Bookings", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Menus", "Place_Id", c => c.Guid());
            AddColumn("dbo.Menus", "Place_Name", c => c.String(maxLength: 30));
            AddColumn("dbo.Orders", "Booking_Id", c => c.Guid());
            AddColumn("dbo.Orders", "Booking_PlaceId", c => c.Guid());
            AddColumn("dbo.Orders", "Booking_UserId", c => c.Guid());
            AddColumn("dbo.Orders", "Booking_Date", c => c.String(maxLength: 10, fixedLength: true));
            AddColumn("dbo.Orders", "Booking_Time", c => c.String(maxLength: 10, fixedLength: true));
            AddColumn("dbo.Orders", "Booking_NumberOfPeople", c => c.Int());
            AddColumn("dbo.Places", "Manager_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Reviews", "Message", c => c.String(nullable: false));
            AddColumn("dbo.Reviews", "Place_Id", c => c.Guid());
            AddColumn("dbo.Reviews", "Place_Name", c => c.String(maxLength: 30));
            AddColumn("dbo.Reviews", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Tables", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Tables", "Place_Id", c => c.Guid());
            AddColumn("dbo.Tables", "Place_Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.Addresses", "PlaceId", c => c.Guid(nullable: false));
            AlterColumn("dbo.BookedTables", "BookingId", c => c.Guid(nullable: false));
            AlterColumn("dbo.BookedTables", "TableId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Bookings", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Bookings", "PlaceId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Bookings", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Menus", "PlaceId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Orders", "BookingId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Places", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Reviews", "PlaceId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Reviews", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Tables", "PlaceId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Addresses", new[] { "PlaceId", "Country", "City", "Street", "Number" });
            AddPrimaryKey("dbo.BookedTables", new[] { "BookingId", "TableId" });
            AddPrimaryKey("dbo.Bookings", new[] { "Id", "PlaceId", "UserId", "Date", "Time", "NumberOfPeople" });
            AddPrimaryKey("dbo.Menus", "PlaceId");
            AddPrimaryKey("dbo.Orders", new[] { "BookingId", "Name", "Quantity" });
            AddPrimaryKey("dbo.Places", new[] { "Id", "Name" });
            AddPrimaryKey("dbo.Reviews", new[] { "PlaceId", "UserId" });
            AddPrimaryKey("dbo.Tables", "Id");
            CreateIndex("dbo.Addresses", new[] { "Place_Id", "Place_Name" });
            CreateIndex("dbo.Places", "Manager_Id");
            CreateIndex("dbo.BookedTables", "TableId");
            CreateIndex("dbo.BookedTables", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" });
            CreateIndex("dbo.Bookings", new[] { "Place_Id", "Place_Name" });
            CreateIndex("dbo.Bookings", "User_Id");
            CreateIndex("dbo.Tables", new[] { "Place_Id", "Place_Name" });
            CreateIndex("dbo.Menus", new[] { "Place_Id", "Place_Name" });
            CreateIndex("dbo.Orders", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" });
            CreateIndex("dbo.Reviews", new[] { "Place_Id", "Place_Name" });
            CreateIndex("dbo.Reviews", "User_Id");
            AddForeignKey("dbo.Addresses", new[] { "Place_Id", "Place_Name" }, "dbo.Places", new[] { "Id", "Name" });
            AddForeignKey("dbo.Bookings", new[] { "Place_Id", "Place_Name" }, "dbo.Places", new[] { "Id", "Name" });
            AddForeignKey("dbo.Bookings", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Places", "Manager_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BookedTables", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" }, "dbo.Bookings", new[] { "Id", "PlaceId", "UserId", "Date", "Time", "NumberOfPeople" });
            AddForeignKey("dbo.Tables", new[] { "Place_Id", "Place_Name" }, "dbo.Places", new[] { "Id", "Name" });
            AddForeignKey("dbo.BookedTables", "TableId", "dbo.Tables", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Menus", new[] { "Place_Id", "Place_Name" }, "dbo.Places", new[] { "Id", "Name" });
            AddForeignKey("dbo.Orders", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" }, "dbo.Bookings", new[] { "Id", "PlaceId", "UserId", "Date", "Time", "NumberOfPeople" });
            AddForeignKey("dbo.Reviews", new[] { "Place_Id", "Place_Name" }, "dbo.Places", new[] { "Id", "Name" });
            AddForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Reviews", "Review");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "Review", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", new[] { "Place_Id", "Place_Name" }, "dbo.Places");
            DropForeignKey("dbo.Orders", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" }, "dbo.Bookings");
            DropForeignKey("dbo.Menus", new[] { "Place_Id", "Place_Name" }, "dbo.Places");
            DropForeignKey("dbo.BookedTables", "TableId", "dbo.Tables");
            DropForeignKey("dbo.Tables", new[] { "Place_Id", "Place_Name" }, "dbo.Places");
            DropForeignKey("dbo.BookedTables", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" }, "dbo.Bookings");
            DropForeignKey("dbo.Places", "Manager_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", new[] { "Place_Id", "Place_Name" }, "dbo.Places");
            DropForeignKey("dbo.Addresses", new[] { "Place_Id", "Place_Name" }, "dbo.Places");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "Place_Id", "Place_Name" });
            DropIndex("dbo.Orders", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" });
            DropIndex("dbo.Menus", new[] { "Place_Id", "Place_Name" });
            DropIndex("dbo.Tables", new[] { "Place_Id", "Place_Name" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Bookings", new[] { "User_Id" });
            DropIndex("dbo.Bookings", new[] { "Place_Id", "Place_Name" });
            DropIndex("dbo.BookedTables", new[] { "Booking_Id", "Booking_PlaceId", "Booking_UserId", "Booking_Date", "Booking_Time", "Booking_NumberOfPeople" });
            DropIndex("dbo.BookedTables", new[] { "TableId" });
            DropIndex("dbo.Places", new[] { "Manager_Id" });
            DropIndex("dbo.Addresses", new[] { "Place_Id", "Place_Name" });
            DropPrimaryKey("dbo.Tables");
            DropPrimaryKey("dbo.Reviews");
            DropPrimaryKey("dbo.Places");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Menus");
            DropPrimaryKey("dbo.Bookings");
            DropPrimaryKey("dbo.BookedTables");
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Tables", "PlaceId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Reviews", "UserId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Reviews", "PlaceId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Places", "Id", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Orders", "BookingId", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Menus", "PlaceId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Bookings", "UserId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Bookings", "PlaceId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Bookings", "Id", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.BookedTables", "TableId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.BookedTables", "BookingId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Addresses", "PlaceId", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Tables", "Place_Name");
            DropColumn("dbo.Tables", "Place_Id");
            DropColumn("dbo.Tables", "Id");
            DropColumn("dbo.Reviews", "User_Id");
            DropColumn("dbo.Reviews", "Place_Name");
            DropColumn("dbo.Reviews", "Place_Id");
            DropColumn("dbo.Reviews", "Message");
            DropColumn("dbo.Places", "Manager_Id");
            DropColumn("dbo.Orders", "Booking_NumberOfPeople");
            DropColumn("dbo.Orders", "Booking_Time");
            DropColumn("dbo.Orders", "Booking_Date");
            DropColumn("dbo.Orders", "Booking_UserId");
            DropColumn("dbo.Orders", "Booking_PlaceId");
            DropColumn("dbo.Orders", "Booking_Id");
            DropColumn("dbo.Menus", "Place_Name");
            DropColumn("dbo.Menus", "Place_Id");
            DropColumn("dbo.Bookings", "User_Id");
            DropColumn("dbo.Bookings", "Place_Name");
            DropColumn("dbo.Bookings", "Place_Id");
            DropColumn("dbo.BookedTables", "Booking_NumberOfPeople");
            DropColumn("dbo.BookedTables", "Booking_Time");
            DropColumn("dbo.BookedTables", "Booking_Date");
            DropColumn("dbo.BookedTables", "Booking_UserId");
            DropColumn("dbo.BookedTables", "Booking_PlaceId");
            DropColumn("dbo.BookedTables", "Booking_Id");
            DropColumn("dbo.Addresses", "Place_Name");
            DropColumn("dbo.Addresses", "Place_Id");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            AddPrimaryKey("dbo.Tables", new[] { "PlaceId", "NumberOfPeople", "NumberOfTables" });
            AddPrimaryKey("dbo.Reviews", new[] { "PlaceId", "UserId", "Review", "Rating" });
            AddPrimaryKey("dbo.Places", new[] { "Id", "Name" });
            AddPrimaryKey("dbo.Orders", new[] { "BookingId", "Name", "Quantity" });
            AddPrimaryKey("dbo.Menus", "PlaceId");
            AddPrimaryKey("dbo.Bookings", new[] { "Id", "PlaceId", "UserId", "Date", "Time", "NumberOfPeople" });
            AddPrimaryKey("dbo.BookedTables", new[] { "BookingId", "TableId" });
            AddPrimaryKey("dbo.Addresses", new[] { "PlaceId", "Country", "City", "Street", "Number" });
        }
    }
}
