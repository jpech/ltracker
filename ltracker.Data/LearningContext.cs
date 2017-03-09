namespace ltracker.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class LearningContext : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'LearningContext' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'ltracker.Data.Entities.LearningContext' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'LearningContext'  en el archivo de configuración de la aplicación.
        public LearningContext()
            : base("name=LearningContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var individual = modelBuilder.Entity<Individual>();
            individual.ToTable("Individuals");
            individual.HasKey(x => x.Id);
            individual.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            individual.Property(x => x.Email).HasMaxLength(500).IsRequired();
            individual.Property(x => x.Name).HasMaxLength(500).IsRequired();
            //individual.HasMany(x => x.Courses);

            var topic = modelBuilder.Entity<Topic>();
            topic.ToTable("Topics");
            topic.HasKey(x => x.Id);
            topic.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            topic.Property(x => x.Name).HasMaxLength(500).IsRequired();
            topic.Property(x => x.Description).HasMaxLength(500).IsRequired();
            //Relación
            topic.HasMany(x => x.Courses);

            var course = modelBuilder.Entity<Course>();
            course.ToTable("Courses");
            course.HasKey(x => x.Id);
            course.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            course.Property(x => x.Name).HasMaxLength(500).IsRequired();
            course.Property(x => x.Description).HasMaxLength(500).IsOptional();
            course.Property(x => x.DurationAVG).IsOptional();
            //Relación
            course.HasMany(x => x.Topics);
            //course.HasMany(x => x.Individuals);

            var assignedCourse = modelBuilder.Entity<AssignedCourse>();
            assignedCourse.ToTable("AssignedCourses");
            assignedCourse.HasKey(x => x.Id);
            assignedCourse.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            assignedCourse.Property(x => x.IsCompleted).IsRequired();
            assignedCourse.Property(x => x.StartDate).IsOptional();
            assignedCourse.Property(x => x.FinishDate).IsOptional();
            assignedCourse.Property(x => x.TotalHours).IsOptional();
            assignedCourse.Property(x => x.AssingmentDate).IsRequired();
            //Relación
            assignedCourse.HasRequired(x => x.Individual).WithMany().HasForeignKey(x => x.IndividualId);
            assignedCourse.HasRequired(x => x.Course).WithMany().HasForeignKey(x => x.CourseId);

            base.OnModelCreating(modelBuilder);
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet <Individual> Individuals { get; set; }
        public virtual DbSet<AssignedCourse> AssignedCourses { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}