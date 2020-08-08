namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Redo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "City", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.Sales", "State", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.Sales", "PostalCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Sales", "Country", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
            AddColumn("dbo.AspNetUsers", "PostalCode", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            DropColumn("dbo.Sales", "Longitude");
            DropColumn("dbo.Sales", "Latitude");
            DropColumn("dbo.Deliveries", "DateDelivered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deliveries", "DateDelivered", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sales", "Latitude", c => c.String());
            AddColumn("dbo.Sales", "Longitude", c => c.String());
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "PostalCode");
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.Sales", "Country");
            DropColumn("dbo.Sales", "PostalCode");
            DropColumn("dbo.Sales", "State");
            DropColumn("dbo.Sales", "City");
        }
    }
}
