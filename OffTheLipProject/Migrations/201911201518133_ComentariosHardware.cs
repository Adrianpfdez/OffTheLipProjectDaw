namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComentariosHardware : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hardwares", "Hardware_Id", "dbo.Hardwares");
            DropIndex("dbo.Hardwares", new[] { "Hardware_Id" });
            CreateTable(
                "dbo.CommentHardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Text = c.String(),
                        Hardware_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hardwares", t => t.Hardware_Id)
                .Index(t => t.Hardware_Id);
            
            DropColumn("dbo.Hardwares", "Hardware_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hardwares", "Hardware_Id", c => c.Int());
            DropForeignKey("dbo.CommentHardwares", "Hardware_Id", "dbo.Hardwares");
            DropIndex("dbo.CommentHardwares", new[] { "Hardware_Id" });
            DropTable("dbo.CommentHardwares");
            CreateIndex("dbo.Hardwares", "Hardware_Id");
            AddForeignKey("dbo.Hardwares", "Hardware_Id", "dbo.Hardwares", "Id");
        }
    }
}
