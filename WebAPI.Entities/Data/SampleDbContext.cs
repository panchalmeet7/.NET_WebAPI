using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities.Models;

namespace WebAPI.Entities.Data;

public partial class SampleDbContext : DbContext
{
    public SampleDbContext()
    {
    }

    public SampleDbContext(DbContextOptions<SampleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDto> UserDtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PCT38\\SQL2019;Database=sampleDB;User Id=sa;Password=Tatva@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("employee");

            entity.Property(e => e.Employeeid)
                .ValueGeneratedNever()
                .HasColumnName("employeeid");
            entity.Property(e => e.Addresss)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("addresss");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User");

            entity.Property(e => e.Passwordhash)
                .HasColumnType("text")
                .HasColumnName("passwordhash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserDto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserDTO");

            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
