namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumerado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surfers", "Stance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surfers", "Stance");
        }
    }
}
