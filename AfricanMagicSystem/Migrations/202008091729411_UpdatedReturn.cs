namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedReturn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReturnItems", "ProductID", "dbo.Products");
            DropIndex("dbo.ReturnItems", new[] { "ProductID" });
            DropColumn("dbo.ReturnItems", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReturnItems", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.ReturnItems", "ProductID");
            AddForeignKey("dbo.ReturnItems", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
        }
    }
}
