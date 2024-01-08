using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIApplication.Models
{
    public partial class APIAPPContext : DbContext
    {
        public APIAPPContext()
        {
        }

        public APIAPPContext(DbContextOptions<APIAPPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<Archive> Archives { get; set; } = null!;
        public virtual DbSet<Delete> Deletes { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
 }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity", "CW1");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("Activity_Id");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("date")
                    .HasColumnName("Activity_Date");

                entity.Property(e => e.ActivityType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Activity_Type");

                entity.Property(e => e.ActivityUserId).HasColumnName("Activity_User_Id");

                entity.HasOne(d => d.ActivityUser)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ActivityUserId)
                    .HasConstraintName("FK__Activity__Activi__3A81B327");
            });

            modelBuilder.Entity<Archive>(entity =>
            {
                entity.ToTable("Archive", "CW1");

                entity.Property(e => e.ArchiveId)
                    .ValueGeneratedNever()
                    .HasColumnName("Archive_Id");

                entity.Property(e => e.ArchiveDate)
                    .HasColumnType("date")
                    .HasColumnName("Archive_Date");

                entity.Property(e => e.ArchiveUserId).HasColumnName("Archive_User_Id");

                entity.HasOne(d => d.ArchiveUser)
                    .WithMany(p => p.Archives)
                    .HasForeignKey(d => d.ArchiveUserId)
                    .HasConstraintName("FK__Archive__Archive__4316F928");
            });

            modelBuilder.Entity<Delete>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__Delete__E9C5B3732ECB98FB");

                entity.ToTable("Delete", "CW1");

                entity.Property(e => e.RequestId)
                    .ValueGeneratedNever()
                    .HasColumnName("Request_Id");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("date")
                    .HasColumnName("Request_Date");

                entity.Property(e => e.RequestUserId).HasColumnName("Request_User_Id");

                entity.HasOne(d => d.RequestUser)
                    .WithMany(p => p.Deletes)
                    .HasForeignKey(d => d.RequestUserId)
                    .HasConstraintName("FK__Delete__Request___403A8C7D");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("List", "CW1");

                entity.Property(e => e.ListId)
                    .ValueGeneratedNever()
                    .HasColumnName("List_Id");

                entity.Property(e => e.ListName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("List_Name");

                entity.Property(e => e.ListUserId).HasColumnName("List_User_Id");

                entity.HasOne(d => d.ListUser)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.ListUserId)
                    .HasConstraintName("FK__List__List_User___3D5E1FD2");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserProf__206D9170ACC45176");

                entity.ToTable("UserProfile", "CW1");

                entity.HasIndex(e => e.EmailAddress, "UQ__UserProf__1E66F691B2A73124")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("User_Id");

                entity.Property(e => e.ActivityTimeframe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Activity_Timeframe");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Email_Address");

                entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.JoinedDate)
                    .HasColumnType("date")
                    .HasColumnName("Joined_Date");

                entity.Property(e => e.Language)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Picture_URL");

                entity.Property(e => e.Units)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");

                entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
