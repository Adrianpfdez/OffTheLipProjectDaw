namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documentary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documentaries", "Url", c => c.String());
            AddColumn("dbo.Documentaries", "UrlRedirect", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documentaries", "UrlRedirect");
            DropColumn("dbo.Documentaries", "Url");
        }
    }
}
