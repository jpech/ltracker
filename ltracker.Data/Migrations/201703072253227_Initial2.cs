namespace ltracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Individual_Id", "dbo.Individuals");
            DropIndex("dbo.Courses", new[] { "Individual_Id" });
            CreateTable(
                "dbo.CourseIndividuals",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        Individual_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.Individual_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.Individuals", t => t.Individual_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.Individual_Id);
            
            DropColumn("dbo.Courses", "Individual_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Individual_Id", c => c.Int());
            DropForeignKey("dbo.CourseIndividuals", "Individual_Id", "dbo.Individuals");
            DropForeignKey("dbo.CourseIndividuals", "Course_Id", "dbo.Courses");
            DropIndex("dbo.CourseIndividuals", new[] { "Individual_Id" });
            DropIndex("dbo.CourseIndividuals", new[] { "Course_Id" });
            DropTable("dbo.CourseIndividuals");
            CreateIndex("dbo.Courses", "Individual_Id");
            AddForeignKey("dbo.Courses", "Individual_Id", "dbo.Individuals", "Id");
        }
    }
}
