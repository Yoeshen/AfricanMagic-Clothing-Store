namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomDesigns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomDesigns",
                c => new
                    {
                        DesignNumber = c.Int(nullable: false, identity: true),
                        InternalImage = c.Binary(),
                    })
                .PrimaryKey(t => t.DesignNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomDesigns");
        }
    }
}
