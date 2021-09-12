using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Model.DomainModel
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("logs");

                entity.Property(e => e.Exception).HasMaxLength(3000);

                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.LogLevel).HasMaxLength(3000);

                entity.Property(e => e.Message).HasMaxLength(3000);

                entity.Property(e => e.MessageTemplate).HasMaxLength(3000);

                entity.Property(e => e.Properties).HasMaxLength(3000);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
