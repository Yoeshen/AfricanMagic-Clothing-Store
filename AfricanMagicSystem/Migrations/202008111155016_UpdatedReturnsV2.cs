namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedReturnsV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturnItems", "Status", c => c.String());
            AlterColumn("dbo.ReturnItems", "ReturnReason", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReturnItems", "ReturnReason", c => c.String());
            DropColumn("dbo.ReturnItems", "Status");
        }
    }
}
