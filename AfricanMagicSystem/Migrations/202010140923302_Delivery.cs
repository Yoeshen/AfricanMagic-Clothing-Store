namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delivery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deliveries", "CurrentLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "CurrentLocation");
        }
    }
}
