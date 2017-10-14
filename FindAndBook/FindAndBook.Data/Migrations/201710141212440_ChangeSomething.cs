namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSomething : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookedTables", "BookingId", "dbo.Bookings");
            DropIndex("dbo.BookedTables", new[] { "BookingId" });
            AlterColumn("dbo.BookedTables", "BookingId", c => c.Guid(nullable: false));
            CreateIndex("dbo.BookedTables", "BookingId");
            AddForeignKey("dbo.BookedTables", "BookingId", "dbo.Bookings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookedTables", "BookingId", "dbo.Bookings");
            DropIndex("dbo.BookedTables", new[] { "BookingId" });
            AlterColumn("dbo.BookedTables", "BookingId", c => c.Guid());
            CreateIndex("dbo.BookedTables", "BookingId");
            AddForeignKey("dbo.BookedTables", "BookingId", "dbo.Bookings", "Id");
        }
    }
}
