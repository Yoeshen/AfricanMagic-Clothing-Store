namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSupplierTableV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierShippings", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplierShippings", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupplierShippings", "Notes");
            DropColumn("dbo.SupplierShippings", "Confirmed");
        }
    }
}
