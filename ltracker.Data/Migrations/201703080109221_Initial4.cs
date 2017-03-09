namespace ltracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseIndividuals", "Individual_Id", "dbo.Individuals");
            DropForeignKey("dbo.CourseIndividuals", "Course_Id", "dbo.Courses");
            DropIndex("dbo.CourseIndividuals", new[] { "Individual_Id" });
            DropIndex("dbo.CourseIndividuals", new[] { "Course_Id" });
            DropTable("dbo.CourseIndividuals");
        }
    }
}
