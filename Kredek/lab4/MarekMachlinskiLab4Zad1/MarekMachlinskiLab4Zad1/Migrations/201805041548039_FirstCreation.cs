namespace MarekMachlinskiLab4Zad1.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FirstCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alliances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Clan1Name = c.String(nullable: false, maxLength: 128),
                        Clan2Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clans", t => t.Clan1Name, cascadeDelete: false)
                .ForeignKey("dbo.Clans", t => t.Clan2Name, cascadeDelete: false)
                .Index(t => t.Clan1Name)
                .Index(t => t.Clan2Name);
            
            CreateTable(
                "dbo.Clans",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.DocumentCreations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 128),
                        CreatedDocument = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserName, cascadeDelete: false)
                .Index(t => t.UserName);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Login = c.String(nullable: false, maxLength: 128),
                        RankId = c.Int(nullable: false),
                        ClanName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Login)
                .ForeignKey("dbo.Clans", t => t.ClanName, cascadeDelete: false)
                .ForeignKey("dbo.Ranks", t => t.RankId, cascadeDelete: false)
                .Index(t => t.RankId)
                .Index(t => t.ClanName);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Clan1Name = c.String(nullable: false, maxLength: 128),
                        Clan2Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clans", t => t.Clan1Name, cascadeDelete: false)
                .ForeignKey("dbo.Clans", t => t.Clan2Name, cascadeDelete: false)
                .Index(t => t.Clan1Name)
                .Index(t => t.Clan2Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wars", "Clan2Name", "dbo.Clans");
            DropForeignKey("dbo.Wars", "Clan1Name", "dbo.Clans");
            DropForeignKey("dbo.DocumentCreations", "UserName", "dbo.Users");
            DropForeignKey("dbo.Users", "RankId", "dbo.Ranks");
            DropForeignKey("dbo.Users", "ClanName", "dbo.Clans");
            DropForeignKey("dbo.Alliances", "Clan2Name", "dbo.Clans");
            DropForeignKey("dbo.Alliances", "Clan1Name", "dbo.Clans");
            DropIndex("dbo.Wars", new[] { "Clan2Name" });
            DropIndex("dbo.Wars", new[] { "Clan1Name" });
            DropIndex("dbo.Users", new[] { "ClanName" });
            DropIndex("dbo.Users", new[] { "RankId" });
            DropIndex("dbo.DocumentCreations", new[] { "UserName" });
            DropIndex("dbo.Alliances", new[] { "Clan2Name" });
            DropIndex("dbo.Alliances", new[] { "Clan1Name" });
            DropTable("dbo.Wars");
            DropTable("dbo.Ranks");
            DropTable("dbo.Users");
            DropTable("dbo.DocumentCreations");
            DropTable("dbo.Clans");
            DropTable("dbo.Alliances");
        }
    }
}
