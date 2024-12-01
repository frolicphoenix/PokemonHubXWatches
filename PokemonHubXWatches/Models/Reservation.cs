using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [Required(ErrorMessage = "Reservation date is required.")]
        public DateTime ReservationDate { get; set; }

        // Foreign Key for User
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Foreign Key for Watch
        public int WatchID { get; set; }
        public virtual Watch Watch { get; set; }
    }

    public class ReservationDTO
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }

        public int UserID { get; set; }
        public string UserFullName { get; set; } = string.Empty;

        public int WatchID { get; set; }
        public string WatchName { get; set; } = string.Empty;
    }
}
