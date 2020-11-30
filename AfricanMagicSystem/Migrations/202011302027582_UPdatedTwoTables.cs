namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdatedTwoTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomDesignSales", "Completed", c => c.Boolean());
            AddColumn("dbo.CustomerReviews", "PartReviewed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReviews", "PartReviewed");
            DropColumn("dbo.CustomDesignSales", "Completed");
        }
    }
}
