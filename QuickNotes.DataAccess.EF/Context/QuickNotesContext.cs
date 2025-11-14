using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickNotes.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuickNotesAPI.DataAccess.EF.Context
{
    public partial class QuickNotesContext : DbContext
    {
        public QuickNotesContext()
        {
        }

        public QuickNotesContext(DbContextOptions<QuickNotesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("QuickNotesConnection");

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8_unicode_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.NoteId).HasName("PRIMARY");

                entity.ToTable("Note");

                entity.HasIndex(e => e.UserId, "User_id_idx");

                entity.Property(e => e.NoteId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("Note_id");
                entity.Property(e => e.NoteContent)
                    .HasColumnType("text")
                    .HasColumnName("Note_content");
                entity.Property(e => e.NoteTitle)
                    .HasMaxLength(100)
                    .HasColumnName("Note_title");
                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("User_id");

                entity.HasOne(d => d.User).WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("User_id");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagId).HasName("PRIMARY");

                entity
                    .ToTable("Tag")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.NoteId, "Note_id");
                entity.HasIndex(e => e.UserId, "User_id");

                entity.Property(e => e.TagId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("Tag_id");
                entity.Property(e => e.NoteId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("Note_id");
                entity.Property(e => e.TagName)
                    .HasMaxLength(100)
                    .HasColumnName("Tag_name");
                entity.Property(e => e.UserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("User_id");

                entity.HasOne(d => d.Note).WithMany(p => p.Tags)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("Tag_ibfk_2");

                entity.HasOne(d => d.User).WithMany(p => p.Tags)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Tag_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PRIMARY");

                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("User_id");
                entity.Property(e => e.UserEmail)
                    .HasMaxLength(75)
                    .HasColumnName("User_email");
                entity.Property(e => e.UserPassword)
                    .HasMaxLength(75)
                    .HasColumnName("User_password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
