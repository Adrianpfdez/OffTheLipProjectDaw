namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Equipamiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hardwares", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hardwares", "Image");
        }
    }
}
