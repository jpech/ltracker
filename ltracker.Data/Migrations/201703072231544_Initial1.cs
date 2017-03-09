namespace ltracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Individual_Id", c => c.Int());
            CreateIndex("dbo.Courses", "Individual_Id");
            AddForeignKey("dbo.Courses", "Individual_Id", "dbo.Individuals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Individual_Id", "dbo.Individuals");
            DropIndex("dbo.Courses", new[] { "Individual_Id" });
            DropColumn("dbo.Courses", "Individual_Id");
        }
    }
}
