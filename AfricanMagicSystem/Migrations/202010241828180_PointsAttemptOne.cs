namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PointsAttemptOne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Points", c => c.Int());
            AddColumn("dbo.AspNetUsers", "isVerified", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isVerified");
            DropColumn("dbo.AspNetUsers", "Points");
        }
    }
}
