namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedCustomerReviews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReviews", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReviews", "Comment");
        }
    }
}
