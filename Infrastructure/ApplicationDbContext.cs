using KPM.Domain;
using Microsoft.EntityFrameworkCore;

namespace KPM.Infrastructure
{
  public class ApplicationDbContext: DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

    public DbSet<Function> Functions { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentFunction> DepartmentFunctions { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Lesson> Lessons {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      //CompositeKey
      modelBuilder.Entity<DepartmentFunction>().HasKey(df => new { df.DepartmentId, df.FunctionId });
      //Many to many 
      modelBuilder.Entity<DepartmentFunction>()
         .HasOne(df => df.Department)
         .WithMany(d => d.DepartmentFunctions)
         .HasForeignKey(df => df.DepartmentId);
      modelBuilder.Entity<DepartmentFunction>()
         .HasOne(df => df.Function)
         .WithMany(f => f.DepartmentFunctions)
         .HasForeignKey(df => df.FunctionId);

      //Lesson's one to many
      modelBuilder.Entity<Lesson>()
         .HasOne(l => l.Department)
         .WithMany(d => d.Lessons)
         .HasForeignKey(l => l.DepartmentId);

      modelBuilder.Entity<Lesson>()
          .HasOne(l => l.Function)
          .WithMany(f => f.Lessons)
          .HasForeignKey(l => l.FunctionId);

      modelBuilder.Entity<Lesson>()
          .HasOne(l => l.Industry)
          .WithMany(i => i.Lessons)
          .HasForeignKey(l => l.IndustryId);

      modelBuilder.Entity<Function>().Property(f => f.Name).HasMaxLength(150);
      modelBuilder.Entity<Department>().Property(d => d.Name).HasMaxLength(150);
      modelBuilder.Entity<Industry>().Property(i => i.Name).HasMaxLength(150);
      modelBuilder.Entity<Lesson>().Property(l => l.Title).HasMaxLength(200);
      modelBuilder.Entity<Lesson>().Property(l => l.ProjectName).HasMaxLength(200);
      modelBuilder.Entity<Lesson>().Property(l => l.ValueProposition).HasMaxLength(300);
      modelBuilder.Entity<Lesson>().Property(l => l.LogoUrl).HasMaxLength(500);
      modelBuilder.Entity<Lesson>().Property(l => l.PersonToContact).HasMaxLength(150);


















    }
  }
}
