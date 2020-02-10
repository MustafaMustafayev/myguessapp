using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GuessAppAPI
{
    public partial class DbGuessContext : DbContext
    {
        public DbGuessContext()
        {
        }

        public DbGuessContext(DbContextOptions<DbGuessContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCondition> UserCondition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-DPCC3SS\\MUSTAFA;Database=DbGuess;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Condition).HasColumnName("condition");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostContent)
                    .IsRequired()
                    .HasColumnName("post_content")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.PostImagePath)
                    .HasColumnName("post_image_path")
                    .HasMaxLength(255);

                entity.Property(e => e.PostSlug)
                    .IsRequired()
                    .HasColumnName("post_slug")
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TokenValue)
                    .IsRequired()
                    .HasColumnName("token_value")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Token)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Token_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsMailVerified)
                    .HasColumnName("is_mail_verified")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserAim)
                    .HasColumnName("user_aim")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserMail)
                    .IsRequired()
                    .HasColumnName("user_mail")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("user_password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasColumnName("user_phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserSlug)
                    .IsRequired()
                    .HasColumnName("user_slug")
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<UserCondition>(entity =>
            {
                entity.ToTable("User_Condition");

                entity.Property(e => e.UserConditionId).HasColumnName("user_condition_id");

                entity.Property(e => e.FailureCount).HasColumnName("failure_count");

                entity.Property(e => e.SuccessCount).HasColumnName("success_count");

                entity.Property(e => e.UnknownCount).HasColumnName("unknown_count");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCondition)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Condition_User");
            });
        }
    }
}
