using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Build
    {
        [Key]
        public int BuildId { get; set; }

        [Range(1, 9999, ErrorMessage = "Updated HP must be between 1 and 9999.")]
        public int PokemonUpdatedHP { get; set; }

        [Range(1, 999, ErrorMessage = "Updated Attack must be between 1 and 999.")]
        public int PokemonUpdatedAttack { get; set; }

        [Range(1, 999, ErrorMessage = "Updated Defense must be between 1 and 999.")]
        public int PokemonUpdatedDefense { get; set; }

        [Range(1, 999, ErrorMessage = "Updated Special Attack must be between 1 and 999.")]
        public int PokemonUpdatedSpAttack { get; set; }

        [Range(1, 999, ErrorMessage = "Updated Special Defense must be between 1 and 999.")]
        public int PokemonUpdatedSpDefense { get; set; }

        [Range(0, 100, ErrorMessage = "Updated Cooldown Reduction must be between 0 and 100.")]
        public int PokemonUpdatedCDR { get; set; }

        // Relationships
        public int PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; }

        public ICollection<BuildHeldItem> HeldItems { get; set; } = new List<BuildHeldItem>();
    }

    public class BuildDTO
    {
        public int BuildId { get; set; }
        public int PokemonUpdatedHP { get; set; }
        public int PokemonUpdatedAttack { get; set; }
        public int PokemonUpdatedDefense { get; set; }
        public int PokemonUpdatedSpAttack { get; set; }
        public int PokemonUpdatedSpDefense { get; set; }
        public int PokemonUpdatedCDR { get; set; }

        public int PokemonId { get; set; }
        public PokemonDTO Pokemon { get; set; }
        public List<HeldItemDTO> HeldItems { get; set; } = new List<HeldItemDTO>();
    }
}
