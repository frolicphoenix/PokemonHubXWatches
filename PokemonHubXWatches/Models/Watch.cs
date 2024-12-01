using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Watch
    {
        [Key]
        public int WatchID { get; set; }

        [Required(ErrorMessage = "Watch name is required.")]
        [StringLength(100, ErrorMessage = "Watch name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;

        // Navigation property for Reservation
        public Reservation? Reservation { get; set; }

        // One-to-one relationship with Pokemon
        public int? PokemonID { get; set; }
        public virtual Pokemon? ThemedPokemon { get; set; }
    }

    public class WatchDTO
    {
        public int WatchID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? PokemonID { get; set; }
        public string? ThemedPokemonName { get; set; }
    }
}
