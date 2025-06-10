using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BaiTapGTSC_API.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeInfo> EmployeeInfos { get; set; }

    public virtual DbSet<EmployeeProgress> EmployeeProgresses { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<QuaTrinhLamViec> QuaTrinhLamViecs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("employee_info");

            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("employee_code");
            entity.Property(e => e.EmployeeDob).HasColumnName("employee_dob");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("employee_name");
        });

        modelBuilder.Entity<EmployeeProgress>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("employee_progress");

            entity.Property(e => e.AnnualLeave).HasColumnName("annual_leave");
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("employee_code");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeePosition)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee_position");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.YearsOfService).HasColumnName("years_of_service");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NhanVien__3214EC0727F1FD40");

            entity.ToTable("NhanVien");

            entity.Property(e => e.Ma).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(100);
        });

        modelBuilder.Entity<QuaTrinhLamViec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuaTrinh__3214EC07EDF0D6D1");

            entity.ToTable("QuaTrinhLamViec");

            entity.Property(e => e.MoTaViTri).HasMaxLength(255);

            entity.HasOne(d => d.NhanVien).WithMany(p => p.QuaTrinhLamViecs)
                .HasForeignKey(d => d.NhanVienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuaTrinhL__NhanV__29E1370A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
