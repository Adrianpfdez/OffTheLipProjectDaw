namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Noticias : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Competitions", newName: "News");
            RenameTable(name: "dbo.CompetitionSurfers", newName: "NewsSurfers");
            RenameColumn(table: "dbo.NewsSurfers", name: "Competition_Id", newName: "News_Id");
            RenameIndex(table: "dbo.NewsSurfers", name: "IX_Competition_Id", newName: "IX_News_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.NewsSurfers", name: "IX_News_Id", newName: "IX_Competition_Id");
            RenameColumn(table: "dbo.NewsSurfers", name: "News_Id", newName: "Competition_Id");
            RenameTable(name: "dbo.NewsSurfers", newName: "CompetitionSurfers");
            RenameTable(name: "dbo.News", newName: "Competitions");
        }
    }
}
