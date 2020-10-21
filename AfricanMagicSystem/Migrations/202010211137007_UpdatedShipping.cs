namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedShipping : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SupplierShippings", "EndTime");
            DropColumn("dbo.SupplierShippings", "ThemeColour");
            DropColumn("dbo.SupplierShippings", "IsFullDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SupplierShippings", "IsFullDay", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplierShippings", "ThemeColour", c => c.String());
            AddColumn("dbo.SupplierShippings", "EndTime", c => c.DateTime());
        }
    }
}
