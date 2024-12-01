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
        public DbSet<HeldItem> HeldItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<BuildHeldItem> BuildHeldItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<BuildHeldItem>()
                .HasOne(bhi => bhi.Build)
                .WithMany(b => b.HeldItems)
                .HasForeignKey(bhi => bhi.BuildId);

            modelBuilder.Entity<BuildHeldItem>()
                .HasOne(bhi => bhi.HeldItem)
                .WithMany(hi => hi.Builds)
                .HasForeignKey(bhi => bhi.HeldItemId);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.ThemedWatch)
                .WithOne(w => w.ThemedPokemon)
                .HasForeignKey<Watch>(w => w.PokemonID);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Watch)
                .WithOne(w => w.Reservation)
                .HasForeignKey<Reservation>(r => r.WatchID);
        }
    }
}
