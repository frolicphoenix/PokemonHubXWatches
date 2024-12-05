using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<BuildHeldItem> BuildHeldItems { get; set; }
        public DbSet<HeldItem> HeldItems { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pokemon Configuration
            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.HasKey(e => e.PokemonId);
                entity.Property(e => e.PokemonName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PokemonRole).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PokemonStyle).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.PokemonImage).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasMany(p => p.ThemedWatches)
                    .WithOne(w => w.ThemedPokemon)
                    .HasForeignKey(w => w.PokemonID)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(p => p.Builds)
                    .WithOne(b => b.Pokemon)
                    .HasForeignKey(b => b.PokemonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Build Configuration
            modelBuilder.Entity<Build>(entity =>
            {
                entity.HasKey(e => e.BuildId);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(b => b.Pokemon)
                    .WithMany(p => p.Builds)
                    .HasForeignKey(b => b.PokemonId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.User)
                    .WithMany(u => u.Builds)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // BuildHeldItem Configuration
            modelBuilder.Entity<BuildHeldItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ItemLevel).HasDefaultValue(1);

                entity.HasOne(bhi => bhi.Build)
                    .WithMany(b => b.HeldItems)
                    .HasForeignKey(bhi => bhi.BuildId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bhi => bhi.HeldItem)
                    .WithMany(hi => hi.Builds)
                    .HasForeignKey(bhi => bhi.HeldItemId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(bhi => new { bhi.BuildId, bhi.HeldItemId }).IsUnique();
            });

            // HeldItem Configuration
            modelBuilder.Entity<HeldItem>(entity =>
            {
                entity.HasKey(e => e.HeldItemId);
                entity.Property(e => e.HeldItemName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
                entity.Property(e => e.HeldItemImage).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.HeldItemName).IsUnique();
            });

            // Watch Configuration
            modelBuilder.Entity<Watch>(entity =>
            {
                entity.HasKey(e => e.WatchID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.StockQuantity).IsRequired().HasDefaultValue(0);

                entity.HasOne(w => w.Reservation)
                    .WithOne(r => r.Watch)
                    .HasForeignKey<Reservation>(r => r.WatchID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(w => w.ThemedPokemon)
                    .WithMany(p => p.ThemedWatches)
                    .HasForeignKey(w => w.PokemonID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(w => w.Name).IsUnique();
            });

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50).HasDefaultValue("User");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.UserName).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasMany(u => u.Reservations)
                    .WithOne(r => r.User)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Reservation Configuration
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReservationID);
                entity.Property(e => e.ReservationDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasDefaultValue(ReservationStatus.Pending).HasConversion<string>();
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}