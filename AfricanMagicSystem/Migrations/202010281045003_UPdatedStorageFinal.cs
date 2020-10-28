namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdatedStorageFinal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storages", "Colour", c => c.String());
            AddColumn("dbo.Storages", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Storages", "Size");
            DropColumn("dbo.Storages", "Colour");
        }
    }
}
