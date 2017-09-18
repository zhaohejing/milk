namespace YT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wwwww : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.milk_customer", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.milk_customer", "Balance");
        }
    }
}
