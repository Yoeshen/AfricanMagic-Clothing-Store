namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedAgainCR : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerReviews", "Flagged", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerReviews", "Flagged", c => c.Boolean(nullable: false));
        }
    }
}
