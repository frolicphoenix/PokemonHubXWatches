using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        // Navigation properties
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public virtual ICollection<Build> Builds { get; set; } = new List<Build>();
    }

    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}