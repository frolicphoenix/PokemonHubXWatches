using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.ViewModels
{
    public class HeldItemViewModel
    {
        public int HeldItemId { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        [Display(Name = "Item Name")]
        [StringLength(100, ErrorMessage = "Item name cannot exceed 100 characters")]
        public string HeldItemName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "HP bonus is required")]
        [Display(Name = "HP Bonus")]
        [Range(0, int.MaxValue, ErrorMessage = "HP bonus must be non-negative")]
        public int HeldItemHP { get; set; }

        [Required(ErrorMessage = "Attack bonus is required")]
        [Display(Name = "Attack Bonus")]
        [Range(0, int.MaxValue, ErrorMessage = "Attack bonus must be non-negative")]
        public int HeldItemAttack { get; set; }

        [Required(ErrorMessage = "Defense bonus is required")]
        [Display(Name = "Defense Bonus")]
        [Range(0, int.MaxValue, ErrorMessage = "Defense bonus must be non-negative")]
        public int HeldItemDefense { get; set; }

        [Required(ErrorMessage = "Special Attack bonus is required")]
        [Display(Name = "Special Attack Bonus")]
        [Range(0, int.MaxValue, ErrorMessage = "Special Attack bonus must be non-negative")]
        public int HeldItemSpAttack { get; set; }

        [Required(ErrorMessage = "Special Defense bonus is required")]
        [Display(Name = "Special Defense Bonus")]
        [Range(0, int.MaxValue, ErrorMessage = "Special Defense bonus must be non-negative")]
        public int HeldItemSpDefense { get; set; }

        [Required(ErrorMessage = "CDR bonus is required")]
        [Display(Name = "CDR Bonus")]
        [Range(0, 100, ErrorMessage = "CDR bonus must be between 0 and 100")]
        public int HeldItemCDR { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string HeldItemImage { get; set; }

        // Navigation properties for display
        public ICollection<BuildViewModel> Builds { get; set; } = new List<BuildViewModel>();

        // Metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}