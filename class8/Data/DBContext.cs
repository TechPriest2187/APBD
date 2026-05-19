using APBD_TASK_7.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_TASK_7.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // 1. DbSets: These represent the tables in your database.
        public DbSet<PC> PCs { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<PCComponent> PCComponents { get; set; }

        // 2. OnModelCreating: This is where we configure the database structure and relationships.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- FLUENT API CONFIGURATIONS ---

            // PC Configuration
            modelBuilder.Entity<PC>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary Key
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Weight).HasColumnType("float"); // In SQL Server, float(5) maps to float/real
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            });

            // ComponentType Configuration
            modelBuilder.Entity<ComponentType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Abbreviation).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            });

            // ComponentManufacturer Configuration
            modelBuilder.Entity<ComponentManufacturer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Abbreviation).IsRequired().HasMaxLength(30);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(300);
                entity.Property(e => e.FoundationDate).HasColumnType("date");
            });

            // Component Configuration
            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasKey(e => e.Code); // String Primary Key
                entity.Property(e => e.Code).HasColumnType("char(10)");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Description).HasColumnType("nvarchar(max)");

                // One-to-Many Relationships
                entity.HasOne(c => c.ComponentManufacturer)
                      .WithMany(m => m.Components)
                      .HasForeignKey(c => c.ComponentManufacturersId);

                entity.HasOne(c => c.ComponentType)
                      .WithMany(t => t.Components)
                      .HasForeignKey(c => c.ComponentTypesId);
            });

            // PCComponent Configuration (Many-to-Many Junction Table)
            modelBuilder.Entity<PCComponent>(entity =>
            {
                // Composite Primary Key
                entity.HasKey(e => new { e.PCId, e.ComponentCode });
                
                entity.Property(e => e.ComponentCode).HasColumnType("char(10)");

                // Foreign Key to PC
                entity.HasOne(e => e.PC)
                      .WithMany(p => p.PCComponents)
                      .HasForeignKey(e => e.PCId)
                      .OnDelete(DeleteBehavior.Cascade); // Deleting a PC deletes its component links

                // Foreign Key to Component
                entity.HasOne(e => e.Component)
                      .WithMany(c => c.PCComponents)
                      .HasForeignKey(e => e.ComponentCode)
                      .OnDelete(DeleteBehavior.Restrict); 
            });
        }
    }
}