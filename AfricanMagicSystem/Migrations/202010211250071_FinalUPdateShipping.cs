namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalUPdateShipping : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SupplierShippings", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupplierShippings", "Date", c => c.DateTime(nullable: false));
        }
    }
}
