using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchAPIController : ControllerBase
    {
        private readonly IWatchService _watchService;

        public WatchAPIController(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var watches = await _watchService.GetAllAsync();
            return Ok(watches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var watch = await _watchService.GetByIdAsync(id);
            if (watch == null)
                return NotFound();
            return Ok(watch);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Watch watch)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _watchService.AddAsync(watch);
            return CreatedAtAction(nameof(GetById), new { id = watch.WatchID }, watch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Watch watch)
        {
            if (id != watch.WatchID)
                return BadRequest("Watch ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _watchService.UpdateAsync(watch);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingWatch = await _watchService.GetByIdAsync(id);
            if (existingWatch == null)
                return NotFound();

            await _watchService.DeleteAsync(id);
            return NoContent();
        }
    }
}
