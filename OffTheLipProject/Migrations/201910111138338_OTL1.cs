namespace OffTheLipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OTL1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Price = c.Int(nullable: false),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.Surfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Natinality = c.String(),
                        Competitor = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Competitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documentaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
                        Currency = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Payment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .Index(t => t.Payment_Id);
            
            CreateTable(
                "dbo.CompetitionSurfers",
                c => new
                    {
                        Competition_Id = c.Int(nullable: false),
                        Surfer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Competition_Id, t.Surfer_Id })
                .ForeignKey("dbo.Competitions", t => t.Competition_Id, cascadeDelete: true)
                .ForeignKey("dbo.Surfers", t => t.Surfer_Id, cascadeDelete: true)
                .Index(t => t.Competition_Id)
                .Index(t => t.Surfer_Id);
            
            CreateTable(
                "dbo.DocumentarySurfers",
                c => new
                    {
                        Documentary_Id = c.Int(nullable: false),
                        Surfer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Documentary_Id, t.Surfer_Id })
                .ForeignKey("dbo.Documentaries", t => t.Documentary_Id, cascadeDelete: true)
                .ForeignKey("dbo.Surfers", t => t.Surfer_Id, cascadeDelete: true)
                .Index(t => t.Documentary_Id)
                .Index(t => t.Surfer_Id);
            
            CreateTable(
                "dbo.SurferHardwares",
                c => new
                    {
                        Surfer_Id = c.Int(nullable: false),
                        Hardware_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Surfer_Id, t.Hardware_Id })
                .ForeignKey("dbo.Surfers", t => t.Surfer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Hardwares", t => t.Hardware_Id, cascadeDelete: true)
                .Index(t => t.Surfer_Id)
                .Index(t => t.Hardware_Id);
            
            AddColumn("dbo.AspNetUsers", "Cart_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Cart_Id");
            AddForeignKey("dbo.AspNetUsers", "Cart_Id", "dbo.Carts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.PaymentMethods", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Payments", "Id", "dbo.Carts");
            DropForeignKey("dbo.Hardwares", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.SurferHardwares", "Hardware_Id", "dbo.Hardwares");
            DropForeignKey("dbo.SurferHardwares", "Surfer_Id", "dbo.Surfers");
            DropForeignKey("dbo.DocumentarySurfers", "Surfer_Id", "dbo.Surfers");
            DropForeignKey("dbo.DocumentarySurfers", "Documentary_Id", "dbo.Documentaries");
            DropForeignKey("dbo.CompetitionSurfers", "Surfer_Id", "dbo.Surfers");
            DropForeignKey("dbo.CompetitionSurfers", "Competition_Id", "dbo.Competitions");
            DropIndex("dbo.SurferHardwares", new[] { "Hardware_Id" });
            DropIndex("dbo.SurferHardwares", new[] { "Surfer_Id" });
            DropIndex("dbo.DocumentarySurfers", new[] { "Surfer_Id" });
            DropIndex("dbo.DocumentarySurfers", new[] { "Documentary_Id" });
            DropIndex("dbo.CompetitionSurfers", new[] { "Surfer_Id" });
            DropIndex("dbo.CompetitionSurfers", new[] { "Competition_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cart_Id" });
            DropIndex("dbo.PaymentMethods", new[] { "Payment_Id" });
            DropIndex("dbo.Payments", new[] { "Id" });
            DropIndex("dbo.Hardwares", new[] { "Cart_Id" });
            DropColumn("dbo.AspNetUsers", "Cart_Id");
            DropTable("dbo.SurferHardwares");
            DropTable("dbo.DocumentarySurfers");
            DropTable("dbo.CompetitionSurfers");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Payments");
            DropTable("dbo.Documentaries");
            DropTable("dbo.Competitions");
            DropTable("dbo.Surfers");
            DropTable("dbo.Hardwares");
            DropTable("dbo.Carts");
        }
    }
}
