namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Añadir_Comentarios_Likes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Autor = c.String(),
                        Texto = c.String(),
                        Documentary_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documentaries", t => t.Documentary_Id)
                .Index(t => t.Documentary_Id);
            
            AddColumn("dbo.Documentaries", "Like", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comentarios", "Documentary_Id", "dbo.Documentaries");
            DropIndex("dbo.Comentarios", new[] { "Documentary_Id" });
            DropColumn("dbo.Documentaries", "Like");
            DropTable("dbo.Comentarios");
        }
    }
}
