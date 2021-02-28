namespace MarekMachlinskiLab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSCore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Score");
        }
    }
}
