using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationAPIController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationAPIController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reservationService.AddAsync(reservation);
            return CreatedAtAction(nameof(GetById), new { id = reservation.ReservationID }, reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Reservation reservation)
        {
            if (id != reservation.ReservationID)
                return BadRequest("Reservation ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reservationService.UpdateAsync(reservation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingReservation = await _reservationService.GetByIdAsync(id);
            if (existingReservation == null)
                return NotFound();

            await _reservationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
