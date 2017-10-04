namespace FindAndBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "TwoPeopleCount", c => c.Int(nullable: false));
            AddColumn("dbo.Places", "FourPeopleCount", c => c.Int(nullable: false));
            AddColumn("dbo.Places", "SixPeopleCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "SixPeopleCount");
            DropColumn("dbo.Places", "FourPeopleCount");
            DropColumn("dbo.Places", "TwoPeopleCount");
        }
    }
}
