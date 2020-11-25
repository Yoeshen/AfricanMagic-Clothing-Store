namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomSalesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomDesignSales",
                c => new
                    {
                        CustomSalesID = c.Int(nullable: false, identity: true),
                        CustomSalesName = c.String(),
                        Email = c.String(),
                        DesignID = c.Int(nullable: false),
                        ShirtText = c.String(),
                        InternalImage = c.Binary(),
                        Size = c.String(),
                        Colour = c.String(),
                    })
                .PrimaryKey(t => t.CustomSalesID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomDesignSales");
        }
    }
}
