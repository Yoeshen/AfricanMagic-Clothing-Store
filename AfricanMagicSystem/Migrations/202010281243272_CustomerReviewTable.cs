namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerReviewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerReviews",
                c => new
                    {
                        CustomerReviewID = c.Int(nullable: false, identity: true),
                        SaleID = c.Int(nullable: false),
                        Username = c.String(),
                        Vote = c.Int(nullable: false),
                        Flagged = c.Boolean(nullable: false),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerReviewID)
                .ForeignKey("dbo.Sales", t => t.SaleID, cascadeDelete: true)
                .Index(t => t.SaleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerReviews", "SaleID", "dbo.Sales");
            DropIndex("dbo.CustomerReviews", new[] { "SaleID" });
            DropTable("dbo.CustomerReviews");
        }
    }
}
