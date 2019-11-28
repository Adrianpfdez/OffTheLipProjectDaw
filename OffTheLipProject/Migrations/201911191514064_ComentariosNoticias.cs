namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComentariosNoticias : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Comments", newName: "CommentDocumentaries");
            CreateTable(
                "dbo.CommentNews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Text = c.String(),
                        News_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.News_Id)
                .Index(t => t.News_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentNews", "News_Id", "dbo.News");
            DropIndex("dbo.CommentNews", new[] { "News_Id" });
            DropTable("dbo.CommentNews");
            RenameTable(name: "dbo.CommentDocumentaries", newName: "Comments");
        }
    }
}
