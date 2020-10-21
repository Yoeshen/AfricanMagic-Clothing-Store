namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdatedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierShippings", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.SupplierShippings", "Time", c => c.String());
            DropColumn("dbo.SupplierShippings", "StartTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SupplierShippings", "StartTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.SupplierShippings", "Time");
            DropColumn("dbo.SupplierShippings", "Date");
        }
    }
}
