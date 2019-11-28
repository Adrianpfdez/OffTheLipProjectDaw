namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OTL2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            DropColumn("dbo.AspNetUsers", "Password");
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            DropColumn("dbo.AspNetUsers", "LastName");
        }
    }
}
