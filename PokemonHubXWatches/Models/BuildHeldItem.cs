using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class BuildHeldItem
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key for Build
        public int BuildId { get; set; }
        public virtual Build Build { get; set; }

        // Foreign Key for HeldItem
        public int HeldItemId { get; set; }
        public virtual HeldItem HeldItem { get; set; }
    }

    public class BuildHeldItemDTO
    {
        public int Id { get; set; }
        public int BuildId { get; set; }
        public int HeldItemId { get; set; }
    }
}
