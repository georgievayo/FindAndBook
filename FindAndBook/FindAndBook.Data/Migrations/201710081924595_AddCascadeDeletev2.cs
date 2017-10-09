namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCascadeDeletev2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Consumables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tables", "PlaceId", "dbo.Places");
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Consumables", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "ManagerId" });
            DropIndex("dbo.Reviews", new[] { "PlaceId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Tables", new[] { "PlaceId" });
            AlterColumn("dbo.Bookings", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Consumables", "PlaceId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Places", "ManagerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Reviews", "PlaceId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Reviews", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Tables", "PlaceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Bookings", "UserId");
            CreateIndex("dbo.Consumables", "PlaceId");
            CreateIndex("dbo.Places", "ManagerId");
            CreateIndex("dbo.Reviews", "PlaceId");
            CreateIndex("dbo.Reviews", "UserId");
            CreateIndex("dbo.Tables", "PlaceId");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Consumables", "PlaceId", "dbo.Places", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Places", "ManagerId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Reviews", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tables", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Consumables", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tables", new[] { "PlaceId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "ManagerId" });
            DropIndex("dbo.Consumables", new[] { "PlaceId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            AlterColumn("dbo.Tables", "PlaceId", c => c.Guid());
            AlterColumn("dbo.Reviews", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Reviews", "PlaceId", c => c.Guid());
            AlterColumn("dbo.Places", "ManagerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Consumables", "PlaceId", c => c.Guid());
            AlterColumn("dbo.Bookings", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tables", "PlaceId");
            CreateIndex("dbo.Reviews", "UserId");
            CreateIndex("dbo.Reviews", "PlaceId");
            CreateIndex("dbo.Places", "ManagerId");
            CreateIndex("dbo.Consumables", "PlaceId");
            CreateIndex("dbo.Bookings", "UserId");
            AddForeignKey("dbo.Tables", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reviews", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Places", "ManagerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Consumables", "PlaceId", "dbo.Places", "Id");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
