namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurferImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surfers", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surfers", "Image");
        }
    }
}
