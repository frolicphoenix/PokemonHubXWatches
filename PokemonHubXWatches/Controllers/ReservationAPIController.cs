using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationAPIController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IWatchService _watchService;

        public ReservationAPIController(
            IReservationService reservationService,
            IUserService userService,
            IWatchService watchService)
        {
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _watchService = watchService ?? throw new ArgumentNullException(nameof(watchService));
        }

        // GET: api/ReservationAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            try
            {
                var reservations = await _reservationService.GetAllReservationsAsync();
                var reservationDTOs = reservations.Select(r => new ReservationDTO
                {
                    ReservationID = r.ReservationID,
                    ReservationDate = r.ReservationDate,
                    EndDate = r.EndDate,
                    Status = r.Status,
                    TotalPrice = r.TotalPrice,
                    Notes = r.Notes,
                    UserID = r.UserId,
                    UserName = r.User.UserName,
                    UserEmail = r.User.Email,
                    WatchID = r.WatchID,
                    WatchName = r.Watch.Name,
                    WatchImage = r.Watch.ImageUrl
                });

                return Ok(reservationDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ReservationAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound($"Reservation with ID {id} not found.");
                }

                var reservationDTO = new ReservationDTO
                {
                    ReservationID = reservation.ReservationID,
                    ReservationDate = reservation.ReservationDate,
                    EndDate = reservation.EndDate,
                    Status = reservation.Status,
                    TotalPrice = reservation.TotalPrice,
                    Notes = reservation.Notes,
                    UserID = reservation.UserId,
                    UserName = reservation.User.UserName,
                    UserEmail = reservation.User.Email,
                    WatchID = reservation.WatchID,
                    WatchName = reservation.Watch.Name,
                    WatchImage = reservation.Watch.ImageUrl
                };

                return Ok(reservationDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ReservationAPI
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> CreateReservation([FromBody] Reservation reservation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate user exists
                if (!await _userService.ValidateUserExistsAsync(reservation.UserId))
                {
                    return BadRequest($"User with ID {reservation.UserId} does not exist.");
                }

                // Validate watch exists and is available
                if (!await _watchService.ValidateWatchExistsAsync(reservation.WatchID))
                {
                    return BadRequest($"Watch with ID {reservation.WatchID} does not exist.");
                }

                if (!await _watchService.IsWatchAvailableAsync(reservation.WatchID))
                {
                    return BadRequest($"Watch with ID {reservation.WatchID} is not available.");
                }

                // Validate date range
                if (!await _reservationService.IsWatchAvailableForPeriodAsync(
                    reservation.WatchID,
                    reservation.ReservationDate,
                    reservation.EndDate))
                {
                    return BadRequest("Watch is not available for the selected period.");
                }

                var createdReservation = await _reservationService.CreateReservationAsync(reservation);
                var reservationDTO = new ReservationDTO
                {
                    ReservationID = createdReservation.ReservationID,
                    ReservationDate = createdReservation.ReservationDate,
                    EndDate = createdReservation.EndDate,
                    Status = createdReservation.Status,
                    TotalPrice = createdReservation.TotalPrice,
                    Notes = createdReservation.Notes,
                    UserID = createdReservation.UserId,
                    WatchID = createdReservation.WatchID
                };

                return CreatedAtAction(nameof(GetReservation), new { id = reservationDTO.ReservationID }, reservationDTO);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ReservationAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] Reservation reservation)
        {
            try
            {
                if (id != reservation.ReservationID)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate date range if dates are being updated
                var existingReservation = await _reservationService.GetReservationByIdAsync(id);
                if (existingReservation == null)
                {
                    return NotFound($"Reservation with ID {id} not found.");
                }

                if (reservation.ReservationDate != existingReservation.ReservationDate ||
                    reservation.EndDate != existingReservation.EndDate)
                {
                    if (!await _reservationService.IsWatchAvailableForPeriodAsync(
                        reservation.WatchID,
                        reservation.ReservationDate,
                        reservation.EndDate,
                        id))
                    {
                        return BadRequest("Watch is not available for the selected period.");
                    }
                }

                var updatedReservation = await _reservationService.UpdateReservationAsync(id, reservation);
                if (updatedReservation == null)
                {
                    return NotFound($"Reservation with ID {id} not found.");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ReservationAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                var result = await _reservationService.DeleteReservationAsync(id);
                if (!result)
                {
                    return NotFound($"Reservation with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ReservationAPI/watch/5
        [HttpGet("watch/{watchId}")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetWatchReservations(int watchId)
        {
            try
            {
                if (!await _watchService.ValidateWatchExistsAsync(watchId))
                {
                    return NotFound($"Watch with ID {watchId} not found.");
                }

                var reservations = await _reservationService.GetReservationsByWatchIdAsync(watchId);
                var reservationDTOs = reservations.Select(r => new ReservationDTO
                {
                    ReservationID = r.ReservationID,
                    ReservationDate = r.ReservationDate,
                    EndDate = r.EndDate,
                    Status = r.Status,
                    TotalPrice = r.TotalPrice,
                    UserID = r.UserId,
                    UserName = r.User.UserName,
                    WatchID = r.WatchID
                });

                return Ok(reservationDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}