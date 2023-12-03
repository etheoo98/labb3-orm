using System;
using System.Collections.Generic;
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

    public virtual DbSet<AllGradesPastYear> AllGradesPastYears { get; set; }

    public virtual DbSet<AllStudent> AllStudents { get; set; }

    public virtual DbSet<AllStudentsYear1> AllStudentsYear1s { get; set; }

    public virtual DbSet<AllTeacher> AllTeachers { get; set; }

    public virtual DbSet<Compartment> Compartments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseTeacher> CourseTeachers { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Principal> Principals { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffRole> StaffRoles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<YearGroup> YearGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=../../../Database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasIndex(e => e.StaffRolesId, "IX_Administrators_Staff_Roles_Id").IsUnique();

            entity.HasIndex(e => e.Username, "IX_Administrators_Username").IsUnique();

            entity.Property(e => e.Password).HasColumnType("varchar(120)");
            entity.Property(e => e.StaffRolesId).HasColumnName("Staff_Roles_Id");
            entity.Property(e => e.Username).HasColumnType("varchar(32)");

            entity.HasOne(d => d.StaffRoles).WithOne(p => p.Administrator)
                .HasPrincipalKey<StaffRole>(p => p.StaffId)
                .HasForeignKey<Administrator>(d => d.StaffRolesId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AllGradesPastYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("All_Grades_Past_Year");

            entity.Property(e => e.Course).HasColumnType("varchar(30)");
            entity.Property(e => e.DateSet).HasColumnName("Date set");
            entity.Property(e => e.Grade).HasColumnType("varchar(1)");
            entity.Property(e => e.LastName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("Last name");
        });

        modelBuilder.Entity<AllStudent>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("All_Students");

            entity.Property(e => e.FirstName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("First name");
            entity.Property(e => e.LastName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("Last name");
        });

        modelBuilder.Entity<AllStudentsYear1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("All_Students_Year_1");

            entity.Property(e => e.FirstName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("Last_Name");
        });

        modelBuilder.Entity<AllTeacher>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("All_Teachers");

            entity.Property(e => e.FirstName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("First name");
            entity.Property(e => e.LastName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("Last name");
        });

        modelBuilder.Entity<Compartment>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Compartments_Name").IsUnique();

            entity.Property(e => e.Name).HasColumnType("varchar(20)");
            entity.Property(e => e.SalariesId).HasColumnName("Salaries_Id");

            entity.HasOne(d => d.Salaries).WithMany(p => p.Compartments)
                .HasForeignKey(d => d.SalariesId)
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

            entity.Property(e => e.CoursesId).HasColumnName("Courses_Id");
            entity.Property(e => e.TeachersId).HasColumnName("Teachers_Id");

            entity.HasOne(d => d.Courses).WithMany(p => p.CourseTeachers)
                .HasForeignKey(d => d.CoursesId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Teachers).WithMany(p => p.CourseTeachers)
                .HasForeignKey(d => d.TeachersId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.CoursesId).HasColumnName("Courses_Id");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.StudentGrade)
                .HasColumnType("varchar(1)")
                .HasColumnName("Student_Grade");
            entity.Property(e => e.StudentsId).HasColumnName("Students_Id");
            entity.Property(e => e.TeachersId).HasColumnName("Teachers_Id");

            entity.HasOne(d => d.Courses).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CoursesId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Students).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentsId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Teachers).WithMany(p => p.Grades)
                .HasForeignKey(d => d.TeachersId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(e => e.Ssn, "IX_Persons_SSN").IsUnique();

            entity.Property(e => e.FirstName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("First_Name");
            entity.Property(e => e.Gender).HasColumnType("varchar(20)");
            entity.Property(e => e.LastName)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("Last_Name");
            entity.Property(e => e.Ssn)
                .HasColumnType("varchar(10)")
                .HasColumnName("SSN");
        });

        modelBuilder.Entity<Principal>(entity =>
        {
            entity.HasIndex(e => e.StaffRolesId, "IX_Principals_Staff_Roles_Id").IsUnique();

            entity.Property(e => e.StaffRolesId).HasColumnName("Staff_Roles_Id");

            entity.HasOne(d => d.StaffRoles).WithOne(p => p.Principal)
                .HasForeignKey<Principal>(d => d.StaffRolesId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.Property(e => e.CoursesId).HasColumnName("Courses_Id");
            entity.Property(e => e.StudentsId).HasColumnName("Students_Id");

            entity.HasOne(d => d.Courses).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CoursesId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Students).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.StudentsId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.PersonsId, "IX_Roles_Persons_Id").IsUnique();

            entity.Property(e => e.PersonsId).HasColumnName("Persons_Id");

            entity.HasOne(d => d.Persons).WithOne(p => p.Role)
                .HasForeignKey<Role>(d => d.PersonsId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.CompartmentsId).HasColumnName("Compartments_Id");
            entity.Property(e => e.StaffRolesId).HasColumnName("Staff_Roles_Id");

            entity.HasOne(d => d.Compartments).WithMany(p => p.Staff)
                .HasForeignKey(d => d.CompartmentsId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.StaffRoles).WithMany(p => p.Staff).HasForeignKey(d => d.StaffRolesId);
        });

        modelBuilder.Entity<StaffRole>(entity =>
        {
            entity.ToTable("Staff_Roles");

            entity.HasIndex(e => e.StaffId, "IX_Staff_Roles_Staff_Id").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.RolesId).HasColumnName("Roles_Id");
            entity.Property(e => e.YearGroupsId).HasColumnName("Year_Groups_Id");

            entity.HasOne(d => d.Roles).WithMany(p => p.Students)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.YearGroups).WithMany(p => p.Students)
                .HasForeignKey(d => d.YearGroupsId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasIndex(e => e.StaffRolesId, "IX_Teachers_Staff_Roles_Id").IsUnique();

            entity.Property(e => e.StaffRolesId).HasColumnName("Staff_Roles_Id");

            entity.HasOne(d => d.StaffRoles).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.StaffRolesId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<YearGroup>(entity =>
        {
            entity.ToTable("Year_Groups");

            entity.HasIndex(e => e.Year, "IX_Year_Groups_Year").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
