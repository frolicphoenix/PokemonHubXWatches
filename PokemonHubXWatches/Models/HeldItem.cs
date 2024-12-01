using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class HeldItem
    {
        [Key]
        public int HeldItemId { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name cannot exceed 100 characters.")]
        public string HeldItemName { get; set; }

        [Range(0, 9999, ErrorMessage = "HP boost must be between 0 and 9999.")]
        public int HeldItemHP { get; set; }

        [Range(0, 999, ErrorMessage = "Attack boost must be between 0 and 999.")]
        public int HeldItemAttack { get; set; }

        [Range(0, 999, ErrorMessage = "Defense boost must be between 0 and 999.")]
        public int HeldItemDefense { get; set; }

        [Range(0, 999, ErrorMessage = "Special Attack boost must be between 0 and 999.")]
        public int HeldItemSpAttack { get; set; }

        [Range(0, 999, ErrorMessage = "Special Defense boost must be between 0 and 999.")]
        public int HeldItemSpDefense { get; set; }

        [Range(0, 100, ErrorMessage = "Cooldown Reduction boost must be between 0 and 100.")]
        public int HeldItemCDR { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string HeldItemImage { get; set; }

        // Relationships
        public ICollection<BuildHeldItem> Builds { get; set; } = new List<BuildHeldItem>();
    }

    public class HeldItemDTO
    {
        public int HeldItemId { get; set; }
        public string HeldItemName { get; set; }
        public int HeldItemHP { get; set; }
        public int HeldItemAttack { get; set; }
        public int HeldItemDefense { get; set; }
        public int HeldItemSpAttack { get; set; }
        public int HeldItemSpDefense { get; set; }
        public int HeldItemCDR { get; set; }
        public string HeldItemImage { get; set; }
    }
}
