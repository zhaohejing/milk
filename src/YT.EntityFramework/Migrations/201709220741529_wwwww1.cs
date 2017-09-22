namespace YT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wwwww1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.milk_chargerecord", "CardId", "dbo.milk_card");
            DropForeignKey("dbo.milk_chargerecord", "CustomerId", "dbo.milk_customer");
            DropIndex("dbo.milk_chargerecord", new[] { "CustomerId" });
            DropIndex("dbo.milk_chargerecord", new[] { "CardId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.milk_chargerecord", "CardId");
            CreateIndex("dbo.milk_chargerecord", "CustomerId");
            AddForeignKey("dbo.milk_chargerecord", "CustomerId", "dbo.milk_customer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.milk_chargerecord", "CardId", "dbo.milk_card", "Id");
        }
    }
}
