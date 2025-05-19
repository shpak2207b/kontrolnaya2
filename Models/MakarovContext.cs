using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kontrolnaya.Models;

public partial class MakarovContext : DbContext
{
    public MakarovContext()
    {
    }

    public MakarovContext(DbContextOptions<MakarovContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=dbsrv\\dub2024;Database=makarov;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requests__3214EC075418121F");

            entity.Property(e => e.CreatedDate).HasColumnName("Created_date");
            entity.Property(e => e.MasterId).HasColumnName("Master_id");
            entity.Property(e => e.ProblemDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Problem_description");
            entity.Property(e => e.RequestNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Request_number");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Master).WithMany(p => p.Requests)
                .HasForeignKey(d => d.MasterId)
                .HasConstraintName("FK__Requests__Master__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A05DAF91");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Full_name");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
