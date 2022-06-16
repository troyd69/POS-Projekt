using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Model
{
    public partial class MusicDBContext : DbContext
    {
        public MusicDBContext()
        {
        }

        public MusicDBContext(DbContextOptions<MusicDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AArtist> AArtists { get; set; } = null!;
        public virtual DbSet<CCategory> CCategories { get; set; } = null!;
        public virtual DbSet<PPlaylist> PPlaylists { get; set; } = null!;
        public virtual DbSet<SSong> SSongs { get; set; } = null!;
        public virtual DbSet<UUser> UUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=MusicDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<AArtist>(entity =>
            {
                entity.HasKey(e => e.AId)
                    .HasName("PRIMARY");

                entity.ToTable("a_artist");

                entity.Property(e => e.AId)
                    .HasColumnType("int(11)")
                    .HasColumnName("a_id");

                entity.Property(e => e.AName)
                    .HasMaxLength(45)
                    .HasColumnName("a_name");
            });

            modelBuilder.Entity<CCategory>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("PRIMARY");

                entity.ToTable("c_category");

                entity.Property(e => e.CId)
                    .HasMaxLength(2)
                    .HasColumnName("c_id")
                    .IsFixedLength();

                entity.Property(e => e.CName)
                    .HasMaxLength(45)
                    .HasColumnName("c_name");
            });

            modelBuilder.Entity<PPlaylist>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PRIMARY");

                entity.ToTable("p_playlist");

                entity.HasIndex(e => e.PUUser, "p_u_user_idx");

                entity.Property(e => e.PId)
                    .HasColumnType("int(11)")
                    .HasColumnName("p_id");

                entity.Property(e => e.PName)
                    .HasMaxLength(45)
                    .HasColumnName("p_name");

                entity.Property(e => e.PUUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("p_u_user");

                entity.HasOne(d => d.PUUserNavigation)
                    .WithMany(p => p.PPlaylists)
                    .HasForeignKey(d => d.PUUser)
                    .HasConstraintName("p_u_user");

                entity.HasMany(d => d.ISSongs)
                    .WithMany(p => p.IPPlaylists)
                    .UsingEntity<Dictionary<string, object>>(
                        "IInclude",
                        l => l.HasOne<SSong>().WithMany().HasForeignKey("ISSong").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("i_s_song"),
                        r => r.HasOne<PPlaylist>().WithMany().HasForeignKey("IPPlaylist").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("i_p_playlist"),
                        j =>
                        {
                            j.HasKey("IPPlaylist", "ISSong").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("i_includes");

                            j.HasIndex(new[] { "ISSong" }, "i_s_song_idx");

                            j.IndexerProperty<int>("IPPlaylist").HasColumnType("int(11)").HasColumnName("i_p_playlist");

                            j.IndexerProperty<int>("ISSong").HasColumnType("int(11)").HasColumnName("i_s_song");
                        });
            });

            modelBuilder.Entity<SSong>(entity =>
            {
                entity.HasKey(e => e.SId)
                    .HasName("PRIMARY");

                entity.ToTable("s_song");

                entity.HasIndex(e => e.SAArtist, "s_a_artist_idx");

                entity.HasIndex(e => e.SCCategory, "s_c_category_idx");

                entity.HasIndex(e => e.SPath, "s_path_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.STitel, "s_titel_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SId)
                    .HasColumnType("int(11)")
                    .HasColumnName("s_id");

                entity.Property(e => e.SAArtist)
                    .HasColumnType("int(11)")
                    .HasColumnName("s_a_artist");

                entity.Property(e => e.SCCategory)
                    .HasMaxLength(2)
                    .HasColumnName("s_c_category")
                    .IsFixedLength();

                entity.Property(e => e.SPath)
                    .HasMaxLength(75)
                    .HasColumnName("s_path");

                entity.Property(e => e.STimespan)
                    .HasColumnType("time")
                    .HasColumnName("s_timespan");

                entity.Property(e => e.STitel)
                    .HasMaxLength(45)
                    .HasColumnName("s_titel");

                entity.HasOne(d => d.SAArtistNavigation)
                    .WithMany(p => p.SSongs)
                    .HasForeignKey(d => d.SAArtist)
                    .HasConstraintName("s_a_artist");

                entity.HasOne(d => d.SCCategoryNavigation)
                    .WithMany(p => p.SSongs)
                    .HasForeignKey(d => d.SCCategory)
                    .HasConstraintName("s_c_category");
            });

            modelBuilder.Entity<UUser>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PRIMARY");

                entity.ToTable("u_user");

                entity.HasIndex(e => e.UUsername, "u_username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UId)
                    .HasColumnType("int(11)")
                    .HasColumnName("u_id");

                entity.Property(e => e.UBirthdate).HasColumnName("u_birthdate");

                entity.Property(e => e.UPassword)
                    .HasMaxLength(45)
                    .HasColumnName("u_password");

                entity.Property(e => e.UUsername)
                    .HasMaxLength(45)
                    .HasColumnName("u_username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
