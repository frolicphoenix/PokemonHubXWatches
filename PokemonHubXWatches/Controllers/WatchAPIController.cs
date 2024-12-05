using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchAPIController : ControllerBase
    {
        private readonly IWatchService _watchService;
        private readonly IPokemonService _pokemonService;

        public WatchAPIController(IWatchService watchService, IPokemonService pokemonService)
        {
            _watchService = watchService ?? throw new ArgumentNullException(nameof(watchService));
            _pokemonService = pokemonService ?? throw new ArgumentNullException(nameof(pokemonService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchDTO>>> GetWatches()
        {
            try
            {
                var watches = await _watchService.GetAllWatchesAsync();
                var watchDTOs = watches.Select(w => new WatchDTO
                {
                    WatchID = w.WatchID,
                    Name = w.Name,
                    Description = w.Description,
                    Price = w.Price,
                    ImageUrl = w.ImageUrl,
                    IsAvailable = w.IsAvailable,
                    StockQuantity = w.StockQuantity,
                    PokemonID = w.PokemonID,
                    ThemedPokemonName = w.ThemedPokemon?.PokemonName,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                });

                return Ok(watchDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WatchDTO>> GetWatch(int id)
        {
            try
            {
                var watch = await _watchService.GetWatchByIdAsync(id);
                if (watch == null)
                {
                    return NotFound($"Watch with ID {id} not found.");
                }

                var watchDTO = new WatchDTO
                {
                    WatchID = watch.WatchID,
                    Name = watch.Name,
                    Description = watch.Description,
                    Price = watch.Price,
                    ImageUrl = watch.ImageUrl,
                    IsAvailable = watch.IsAvailable,
                    StockQuantity = watch.StockQuantity,
                    PokemonID = watch.PokemonID,
                    ThemedPokemonName = watch.ThemedPokemon?.PokemonName,
                    CreatedAt = watch.CreatedAt,
                    UpdatedAt = watch.UpdatedAt
                };

                return Ok(watchDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<WatchDTO>> CreateWatch([FromBody] Watch watch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (watch.PokemonID.HasValue)
                {
                    var pokemonExists = await _pokemonService.ValidatePokemonExistsAsync(watch.PokemonID.Value);
                    if (!pokemonExists)
                    {
                        return BadRequest($"Pokemon with ID {watch.PokemonID} does not exist.");
                    }
                }

                var createdWatch = await _watchService.CreateWatchAsync(watch);
                var watchDTO = new WatchDTO
                {
                    WatchID = createdWatch.WatchID,
                    Name = createdWatch.Name,
                    Description = createdWatch.Description,
                    Price = createdWatch.Price,
                    ImageUrl = createdWatch.ImageUrl,
                    IsAvailable = createdWatch.IsAvailable,
                    StockQuantity = createdWatch.StockQuantity,
                    PokemonID = createdWatch.PokemonID,
                    ThemedPokemonName = createdWatch.ThemedPokemon?.PokemonName,
                    CreatedAt = createdWatch.CreatedAt,
                    UpdatedAt = createdWatch.UpdatedAt
                };

                return CreatedAtAction(nameof(GetWatch), new { id = watchDTO.WatchID }, watchDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWatch(int id, [FromBody] Watch watch)
        {
            try
            {
                if (id != watch.WatchID)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (watch.PokemonID.HasValue)
                {
                    var pokemonExists = await _pokemonService.ValidatePokemonExistsAsync(watch.PokemonID.Value);
                    if (!pokemonExists)
                    {
                        return BadRequest($"Pokemon with ID {watch.PokemonID} does not exist.");
                    }
                }

                var updatedWatch = await _watchService.UpdateWatchAsync(id, watch);
                if (updatedWatch == null)
                {
                    return NotFound($"Watch with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatch(int id)
        {
            try
            {
                var result = await _watchService.DeleteWatchAsync(id);
                if (!result)
                {
                    return NotFound($"Watch with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("top/{count}")]
        public async Task<ActionResult<IEnumerable<WatchDTO>>> GetTopWatches(int count)
        {
            try
            {
                var watches = await _watchService.GetTopWatchesAsync(count);
                var watchDTOs = watches.Select(w => new WatchDTO
                {
                    WatchID = w.WatchID,
                    Name = w.Name,
                    Price = w.Price,
                    ImageUrl = w.ImageUrl,
                    IsAvailable = w.IsAvailable,
                    StockQuantity = w.StockQuantity,
                    ThemedPokemonName = w.ThemedPokemon?.PokemonName,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                });

                return Ok(watchDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<WatchDTO>>> GetAvailableWatches()
        {
            try
            {
                var watches = await _watchService.GetAvailableWatchesAsync();
                var watchDTOs = watches.Select(w => new WatchDTO
                {
                    WatchID = w.WatchID,
                    Name = w.Name,
                    Price = w.Price,
                    ImageUrl = w.ImageUrl,
                    IsAvailable = w.IsAvailable,
                    StockQuantity = w.StockQuantity,
                    ThemedPokemonName = w.ThemedPokemon?.PokemonName,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                });

                return Ok(watchDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}