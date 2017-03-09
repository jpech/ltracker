namespace ltracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssingmentDate = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        StartDate = c.DateTime(),
                        FinishDate = c.DateTime(),
                        TotalHours = c.Decimal(precision: 18, scale: 2),
                        IndividualId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Individuals", t => t.IndividualId, cascadeDelete: true)
                .Index(t => t.IndividualId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        DurationAVG = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Individuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Email = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseTopics",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        Topic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.Topic_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.Topic_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.Topic_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedCourses", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.AssignedCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseTopics", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.CourseTopics", "Course_Id", "dbo.Courses");
            DropIndex("dbo.CourseTopics", new[] { "Topic_Id" });
            DropIndex("dbo.CourseTopics", new[] { "Course_Id" });
            DropIndex("dbo.AssignedCourses", new[] { "CourseId" });
            DropIndex("dbo.AssignedCourses", new[] { "IndividualId" });
            DropTable("dbo.CourseTopics");
            DropTable("dbo.Individuals");
            DropTable("dbo.Topics");
            DropTable("dbo.Courses");
            DropTable("dbo.AssignedCourses");
        }
    }
}
