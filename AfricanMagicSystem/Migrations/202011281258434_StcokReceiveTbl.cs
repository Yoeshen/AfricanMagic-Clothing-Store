namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StcokReceiveTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockReceiveds",
                c => new
                    {
                        ReceivedID = c.Int(nullable: false, identity: true),
                        ProductsReceived = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Supplier = c.String(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        Notes = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReceivedID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockReceiveds");
        }
    }
}
