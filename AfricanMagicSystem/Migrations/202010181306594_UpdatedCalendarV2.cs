namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCalendarV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierShippings", "IsFullDay", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SupplierShippings", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupplierShippings", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.SupplierShippings", "IsFullDay");
        }
    }
}
