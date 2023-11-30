using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Compartment> Compartments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseTeacher> CourseTeachers { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

    public virtual DbSet<Principal> Principals { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=Database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasIndex(e => e.Username, "IX_Administrators_Username").IsUnique();

            entity.Property(e => e.Password).HasColumnType("varchar(120)");
            entity.Property(e => e.RoleName)
                .HasColumnType("varchar(30)")
                .HasColumnName("Role_Name");
            entity.Property(e => e.Username).HasColumnType("varchar(32)");

            entity.HasOne(d => d.RoleNameNavigation).WithMany(p => p.Administrators)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.RoleName)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Compartment>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Compartments_Name").IsUnique();

            entity.Property(e => e.SalaryId).HasColumnName("Salary_Id");

            entity.HasOne(d => d.Salary).WithMany(p => p.Compartments)
                .HasForeignKey(d => d.SalaryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Courses_Name").IsUnique();

            entity.Property(e => e.Name).HasColumnType("varchar(30)");
        });

        modelBuilder.Entity<CourseTeacher>(entity =>
        {
            entity.ToTable("Course_Teacher");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.TeacherId).HasColumnName("Teacher_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseTeachers)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Teacher).WithMany(p => p.CourseTeachers)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Grade1)
                .HasColumnType("varchar(1)")
                .HasColumnName("Grade");
            entity.Property(e => e.RegistrationId).HasColumnName("Registration_Id");
            entity.Property(e => e.TeacherId).HasColumnName("Teacher_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Registration).WithMany(p => p.Grades)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PersonalInfo>(entity =>
        {
            entity.ToTable("PersonalInfo");

            entity.HasIndex(e => e.Ssn, "IX_PersonalInfo_SSN").IsUnique();

            entity.Property(e => e.FirstName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.Gender).HasColumnType("varchar(20)");
            entity.Property(e => e.LastName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.Ssn).HasColumnName("SSN");
        });

        modelBuilder.Entity<Principal>(entity =>
        {
            entity.Property(e => e.RoleName)
                .HasColumnType("varchar(30)")
                .HasColumnName("Role_Name");

            entity.HasOne(d => d.RoleNameNavigation).WithMany(p => p.Principals)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.RoleName)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.StudentId).HasColumnName("Student_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Student).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Roles_Name").IsUnique();

            entity.Property(e => e.Name).HasColumnType("varchar(30)");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.CompartmentId).HasColumnName("Compartment_Id");
            entity.Property(e => e.PersonalInfoId).HasColumnName("PersonalInfo_Id");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.HasOne(d => d.Compartment).WithMany(p => p.Staff)
                .HasForeignKey(d => d.CompartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PersonalInfo).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PersonalInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.PersonalInfoId).HasColumnName("PersonalInfo_Id");

            entity.HasOne(d => d.PersonalInfo).WithMany(p => p.Students)
                .HasForeignKey(d => d.PersonalInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.RoleName)
                .HasColumnType("varchar(30)")
                .HasColumnName("Role_Name");

            entity.HasOne(d => d.RoleNameNavigation).WithMany(p => p.Teachers)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.RoleName)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
