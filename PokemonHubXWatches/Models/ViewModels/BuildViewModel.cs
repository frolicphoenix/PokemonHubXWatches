using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.ViewModels
{
    public class BuildViewModel
    {
        public int BuildId { get; set; }

        [Required(ErrorMessage = "Build name is required")]
        [Display(Name = "Build Name")]
        [StringLength(100, ErrorMessage = "Build name cannot exceed 100 characters")]
        public string BuildName { get; set; }

        [Required(ErrorMessage = "Pokemon is required")]
        [Display(Name = "Pokemon")]
        public int PokemonId { get; set; }

        [Required(ErrorMessage = "User is required")]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Display(Name = "Updated HP")]
        [Range(0, int.MaxValue, ErrorMessage = "HP must be non-negative")]
        public int PokemonUpdatedHP { get; set; }

        [Display(Name = "Updated Attack")]
        [Range(0, int.MaxValue, ErrorMessage = "Attack must be non-negative")]
        public int PokemonUpdatedAttack { get; set; }

        [Display(Name = "Updated Defense")]
        [Range(0, int.MaxValue, ErrorMessage = "Defense must be non-negative")]
        public int PokemonUpdatedDefense { get; set; }

        [Display(Name = "Updated Special Attack")]
        [Range(0, int.MaxValue, ErrorMessage = "Special Attack must be non-negative")]
        public int PokemonUpdatedSpAttack { get; set; }

        [Display(Name = "Updated Special Defense")]
        [Range(0, int.MaxValue, ErrorMessage = "Special Defense must be non-negative")]
        public int PokemonUpdatedSpDefense { get; set; }

        [Display(Name = "Updated CDR")]
        [Range(0, 100, ErrorMessage = "CDR must be between 0 and 100")]
        public int PokemonUpdatedCDR { get; set; }

        // Navigation properties for display
        public PokemonViewModel Pokemon { get; set; }
        public UserViewModel User { get; set; }
        public ICollection<BuildHeldItemViewModel> HeldItems { get; set; } = new List<BuildHeldItemViewModel>();

        // Metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}