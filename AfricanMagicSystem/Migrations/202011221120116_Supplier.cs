namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Supplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        SupplierEmail = c.String(),
                        SupplierPhoneNumber = c.String(),
                        ProductsSupplied = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
        }
    }
}
