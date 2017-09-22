namespace YT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wwwww : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.milk_customer", "SpecialId", c => c.Int());
            AddColumn("dbo.milk_specialcard", "IsUsed", c => c.Boolean(nullable: false));
            CreateIndex("dbo.milk_customer", "SpecialId");
            AddForeignKey("dbo.milk_customer", "SpecialId", "dbo.milk_specialcard", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.milk_customer", "SpecialId", "dbo.milk_specialcard");
            DropIndex("dbo.milk_customer", new[] { "SpecialId" });
            DropColumn("dbo.milk_specialcard", "IsUsed");
            DropColumn("dbo.milk_customer", "SpecialId");
        }
    }
}
