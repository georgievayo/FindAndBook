namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropColumn("dbo.Reviews", "UserId");
            RenameColumn(table: "dbo.Reviews", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Reviews", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            AlterColumn("dbo.Reviews", "UserId", c => c.Guid());
            RenameColumn(table: "dbo.Reviews", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Reviews", "UserId", c => c.Guid());
            CreateIndex("dbo.Reviews", "User_Id");
        }
    }
}
