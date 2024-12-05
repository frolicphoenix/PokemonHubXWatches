using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonHubXWatches.Models
{
    public class Watch
    {
        [Key]
        public int WatchID { get; set; }

        [Required(ErrorMessage = "Watch name is required.")]
        [StringLength(100, ErrorMessage = "Watch name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string ImageUrl { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be non-negative.")]
        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public int? PokemonID { get; set; }
        public virtual Pokemon? ThemedPokemon { get; set; }
        public virtual Reservation? Reservation { get; set; }
    }

    public class WatchDTO
    {
        public int WatchID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public int StockQuantity { get; set; }
        public int? PokemonID { get; set; }
        public string? ThemedPokemonName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}