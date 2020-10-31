namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerReviewFinalUpdateAgen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "CustomerReviews_CustomerReviewID", "dbo.CustomerReviews");
            DropIndex("dbo.Sales", new[] { "CustomerReviews_CustomerReviewID" });
            DropColumn("dbo.Sales", "CustomerReviews_CustomerReviewID");
            DropColumn("dbo.CustomerReviews", "Approved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerReviews", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sales", "CustomerReviews_CustomerReviewID", c => c.Int());
            CreateIndex("dbo.Sales", "CustomerReviews_CustomerReviewID");
            AddForeignKey("dbo.Sales", "CustomerReviews_CustomerReviewID", "dbo.CustomerReviews", "CustomerReviewID");
        }
    }
}
