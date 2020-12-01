namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedsupplier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierShippings", "Accepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplierShippings", "Rejected", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplierShippings", "Completed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupplierShippings", "Completed");
            DropColumn("dbo.SupplierShippings", "Rejected");
            DropColumn("dbo.SupplierShippings", "Accepted");
        }
    }
}
