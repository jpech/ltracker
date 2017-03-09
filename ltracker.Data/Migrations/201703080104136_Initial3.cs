namespace ltracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseIndividuals", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseIndividuals", "Individual_Id", "dbo.Individuals");
            DropIndex("dbo.CourseIndividuals", new[] { "Course_Id" });
            DropIndex("dbo.CourseIndividuals", new[] { "Individual_Id" });
            DropTable("dbo.CourseIndividuals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseIndividuals",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        Individual_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.Individual_Id });
            
            CreateIndex("dbo.CourseIndividuals", "Individual_Id");
            CreateIndex("dbo.CourseIndividuals", "Course_Id");
            AddForeignKey("dbo.CourseIndividuals", "Individual_Id", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CourseIndividuals", "Course_Id", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
