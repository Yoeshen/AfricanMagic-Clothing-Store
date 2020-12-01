namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedTblreceive : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockReceiveds", "DateReceived", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockReceiveds", "DateReceived", c => c.DateTime(nullable: false));
        }
    }
}
