using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonHubXWatches.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        [Required(ErrorMessage = "Total price is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than zero.")]
        public decimal TotalPrice { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys and Navigation properties
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int WatchID { get; set; }
        public virtual Watch Watch { get; set; }
    }

    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class ReservationDTO
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public int WatchID { get; set; }
        public string WatchName { get; set; }
        public string WatchImage { get; set; }
    }
}