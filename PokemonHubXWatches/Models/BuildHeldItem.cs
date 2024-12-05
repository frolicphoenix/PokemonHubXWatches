using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonHubXWatches.Models
{
    public class BuildHeldItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BuildId { get; set; }
        public virtual Build Build { get; set; }

        //[Required]
        public int HeldItemId { get; set; }
        public virtual HeldItem HeldItem { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Range(1, 30, ErrorMessage = "Item level must be between 1 and 30")]
        public int ItemLevel { get; set; } = 1;

        // Calculate bonus stats based on item level
        [NotMapped]
        public decimal StatMultiplier => 1 + (ItemLevel * 0.1m);
    }

    public class BuildHeldItemDTO
    {
        public int Id { get; set; }
        public int BuildId { get; set; }
        public string BuildName { get; set; }
        public int HeldItemId { get; set; }
        public string HeldItemName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ItemLevel { get; set; }
        public decimal StatMultiplier { get; set; }
    }
}