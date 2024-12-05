using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.ViewModels
{
    public class PokemonViewModel
    {
        public int PokemonId { get; set; }

        [Required(ErrorMessage = "Pokemon name is required")]
        [Display(Name = "Pokemon Name")]
        [StringLength(100, ErrorMessage = "Pokemon name cannot exceed 100 characters")]
        public string PokemonName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "Role")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string PokemonRole { get; set; }

        [Required(ErrorMessage = "Style is required")]
        [Display(Name = "Style")]
        [StringLength(50, ErrorMessage = "Style cannot exceed 50 characters")]
        public string PokemonStyle { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "HP is required")]
        [Display(Name = "HP")]
        [Range(1, int.MaxValue, ErrorMessage = "HP must be greater than 0")]
        public int PokemonHP { get; set; }

        [Required(ErrorMessage = "Attack is required")]
        [Display(Name = "Attack")]
        [Range(1, int.MaxValue, ErrorMessage = "Attack must be greater than 0")]
        public int PokemonAttack { get; set; }

        [Required(ErrorMessage = "Defense is required")]
        [Display(Name = "Defense")]
        [Range(1, int.MaxValue, ErrorMessage = "Defense must be greater than 0")]
        public int PokemonDefense { get; set; }

        [Required(ErrorMessage = "Special Attack is required")]
        [Display(Name = "Special Attack")]
        [Range(1, int.MaxValue, ErrorMessage = "Special Attack must be greater than 0")]
        public int PokemonSpAttack { get; set; }

        [Required(ErrorMessage = "Special Defense is required")]
        [Display(Name = "Special Defense")]
        [Range(1, int.MaxValue, ErrorMessage = "Special Defense must be greater than 0")]
        public int PokemonSpDefense { get; set; }

        [Required(ErrorMessage = "CDR is required")]
        [Display(Name = "CDR")]
        [Range(0, 100, ErrorMessage = "CDR must be between 0 and 100")]
        public int PokemonCDR { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string PokemonImage { get; set; }

        // Navigation properties for display
        public ICollection<WatchViewModel> ThemedWatches { get; set; } = new List<WatchViewModel>();
        public ICollection<BuildViewModel> Builds { get; set; } = new List<BuildViewModel>();

        // Metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}