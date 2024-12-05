using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.ViewModels
{
    public class BuildHeldItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Build is required")]
        [Display(Name = "Build")]
        public int BuildId { get; set; }

        [Required(ErrorMessage = "Held Item is required")]
        [Display(Name = "Held Item")]
        public int HeldItemId { get; set; }

        [Required(ErrorMessage = "Item Level is required")]
        [Display(Name = "Item Level")]
        [Range(1, 30, ErrorMessage = "Item level must be between 1 and 30")]
        public int ItemLevel { get; set; }

        // Navigation properties for display
        public BuildViewModel Build { get; set; }
        public HeldItemViewModel HeldItem { get; set; }

        // Metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}