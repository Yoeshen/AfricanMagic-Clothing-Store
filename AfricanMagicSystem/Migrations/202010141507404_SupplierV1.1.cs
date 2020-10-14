namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplierV11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SupplierShippings",
                c => new
                    {
                        ShippingID = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        ThemeColour = c.String(),
                    })
                .PrimaryKey(t => t.ShippingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SupplierShippings");
        }
    }
}
