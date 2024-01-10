using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data;

public partial class StudentsContext : DbContext
{
    public StudentsContext()
    {
    }

    public StudentsContext(DbContextOptions<StudentsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassDetail> ClassDetails { get; set; }

    public virtual DbSet<StudentDetail> StudentDetails { get; set; }
    public object StudentDetail { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MAPC1KQ\\MSSQLSERVER01;Initial Catalog=Students;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Command Timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassDetail>(entity =>
        {
            entity.HasKey(e => e.ClassId);

            entity.Property(e => e.ClassName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<StudentDetail>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.Property(e => e.StudentEmail).HasMaxLength(50);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentPhone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.StudentDetails)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_StudentDetails_ClassDetails");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
