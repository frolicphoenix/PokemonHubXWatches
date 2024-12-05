using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonHubXWatches.ViewModels
{
    public class WatchViewModel
    {
        public int WatchID { get; set; }

        [Required(ErrorMessage = "Watch name is required")]
        [Display(Name = "Watch Name")]
        [StringLength(100, ErrorMessage = "Watch name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Stock Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be non-negative")]
        public int StockQuantity { get; set; }

        [Display(Name = "Pokemon Theme")]
        public int? PokemonID { get; set; }

        // Navigation properties for display
        public PokemonViewModel ThemedPokemon { get; set; }
        public ReservationViewModel Reservation { get; set; }

        // Display-only properties
        [Display(Name = "Pokemon Name")]
        public string ThemedPokemonName { get; set; }

        // Metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}