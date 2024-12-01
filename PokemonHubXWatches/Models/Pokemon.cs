using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }

        [Required(ErrorMessage = "Pokemon name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string PokemonName { get; set; }

        [Required(ErrorMessage = "Pokemon role is required.")]
        public string PokemonRole { get; set; } // Example: Attacker, Defender

        [Required(ErrorMessage = "Pokemon style is required.")]
        public string PokemonStyle { get; set; } // Example: Melee, Ranged

        [Range(1, 9999, ErrorMessage = "HP must be between 1 and 9999.")]
        public int PokemonHP { get; set; }

        [Range(1, 999, ErrorMessage = "Attack must be between 1 and 999.")]
        public int PokemonAttack { get; set; }

        [Range(1, 999, ErrorMessage = "Defense must be between 1 and 999.")]
        public int PokemonDefense { get; set; }

        [Range(1, 999, ErrorMessage = "Special Attack must be between 1 and 999.")]
        public int PokemonSpAttack { get; set; }

        [Range(1, 999, ErrorMessage = "Special Defense must be between 1 and 999.")]
        public int PokemonSpDefense { get; set; }

        [Range(0, 100, ErrorMessage = "Cooldown Reduction must be between 0 and 100.")]
        public int PokemonCDR { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string PokemonImage { get; set; } // URL or Path to the image file

        // Relationships
        public ICollection<Build> Builds { get; set; } = new List<Build>();
        public int? WatchID { get; set; }
        public virtual Watch? ThemedWatch { get; set; }
    }

    public class PokemonDTO
    {
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public string PokemonRole { get; set; }
        public string PokemonStyle { get; set; }
        public int PokemonHP { get; set; }
        public int PokemonAttack { get; set; }
        public int PokemonDefense { get; set; }
        public int PokemonSpAttack { get; set; }
        public int PokemonSpDefense { get; set; }
        public int PokemonCDR { get; set; }
        public string PokemonImage { get; set; }
        public WatchDTO? ThemedWatch { get; set; }
    }
}
