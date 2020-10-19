namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19OctoberImportedOldDB : DbMigration
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
                        EndTime = c.DateTime(),
                        ThemeColour = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShippingID);
            
            AddColumn("dbo.Deliveries", "CurrentLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "CurrentLocation");
            DropTable("dbo.SupplierShippings");
        }
    }
}
