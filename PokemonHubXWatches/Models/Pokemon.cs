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
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        public string PokemonRole { get; set; } // Example: Attacker, Defender

        [Required(ErrorMessage = "Pokemon style is required.")]
        [StringLength(50, ErrorMessage = "Style cannot exceed 50 characters.")]
        public string PokemonStyle { get; set; } // Example: Melee, Ranged

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "HP must be between 1 and 9999.")]
        public int PokemonHP { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Attack must be between 1 and 999.")]
        public int PokemonAttack { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Defense must be between 1 and 999.")]
        public int PokemonDefense { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Special Attack must be between 1 and 999.")]
        public int PokemonSpAttack { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Special Defense must be between 1 and 999.")]
        public int PokemonSpDefense { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Cooldown Reduction must be between 0 and 100.")]
        public int PokemonCDR { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string PokemonImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Relationships
        public ICollection<Build> Builds { get; set; } = new List<Build>();
        public ICollection<Watch> ThemedWatches { get; set; } = new List<Watch>();
    }

    public class PokemonDTO
    {
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public string PokemonRole { get; set; }
        public string PokemonStyle { get; set; }
        public string Description { get; set; }
        public int PokemonHP { get; set; }
        public int PokemonAttack { get; set; }
        public int PokemonDefense { get; set; }
        public int PokemonSpAttack { get; set; }
        public int PokemonSpDefense { get; set; }
        public int PokemonCDR { get; set; }
        public string PokemonImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<WatchDTO> ThemedWatches { get; set; }
    }
}