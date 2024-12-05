using PokemonHubXWatches.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonHubXWatches.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationID { get; set; }

        [Required(ErrorMessage = "Reservation date is required")]
        [Display(Name = "Reservation Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public ReservationStatus Status { get; set; }

        [Required(ErrorMessage = "Total price is required")]
        [Display(Name = "Total Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than zero")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "User is required")]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Watch is required")]
        [Display(Name = "Watch")]
        public int WatchID { get; set; }

        // Navigation properties for display
        public UserViewModel User { get; set; }
        public WatchViewModel Watch { get; set; }

        // Display-only properties
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Watch Name")]
        public string WatchName { get; set; }

        // Metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}