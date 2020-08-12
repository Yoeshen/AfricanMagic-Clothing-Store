namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AF3update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleDetails", "ReturnItem_ReturnCode", c => c.Int());
            CreateIndex("dbo.SaleDetails", "ReturnItem_ReturnCode");
            AddForeignKey("dbo.SaleDetails", "ReturnItem_ReturnCode", "dbo.ReturnItems", "ReturnCode");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleDetails", "ReturnItem_ReturnCode", "dbo.ReturnItems");
            DropIndex("dbo.SaleDetails", new[] { "ReturnItem_ReturnCode" });
            DropColumn("dbo.SaleDetails", "ReturnItem_ReturnCode");
        }
    }
}
