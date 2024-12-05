using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string PasswordHash { get; set; }

        [Required]
        [Display(Name = "Role")]
        [StringLength(50)]
        public string Role { get; set; } = "User";

        // Navigation properties for display
        public ICollection<ReservationViewModel> Reservations { get; set; } = new List<ReservationViewModel>();
        public ICollection<BuildViewModel> Builds { get; set; } = new List<BuildViewModel>();

        // Metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}