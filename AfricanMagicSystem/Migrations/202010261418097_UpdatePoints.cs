namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePoints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Points", c => c.String());
            DropColumn("dbo.AspNetUsers", "isVerified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "isVerified", c => c.Boolean());
            AlterColumn("dbo.AspNetUsers", "Points", c => c.Int());
        }
    }
}
