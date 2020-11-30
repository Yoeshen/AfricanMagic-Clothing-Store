namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Paid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomDesignSales", "Paid", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomDesignSales", "Paid");
        }
    }
}
