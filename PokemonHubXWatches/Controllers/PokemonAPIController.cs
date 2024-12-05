using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonAPIController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonAPIController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService ?? throw new ArgumentNullException(nameof(pokemonService));
        }

        // GET: api/PokemonAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonDTO>>> GetPokemons()
        {
            try
            {
                var pokemons = await _pokemonService.GetAllPokemonAsync();
                var pokemonDTOs = pokemons.Select(p => new PokemonDTO
                {
                    PokemonId = p.PokemonId,
                    PokemonName = p.PokemonName,
                    PokemonRole = p.PokemonRole,
                    PokemonStyle = p.PokemonStyle,
                    Description = p.Description,
                    PokemonHP = p.PokemonHP,
                    PokemonAttack = p.PokemonAttack,
                    PokemonDefense = p.PokemonDefense,
                    PokemonSpAttack = p.PokemonSpAttack,
                    PokemonSpDefense = p.PokemonSpDefense,
                    PokemonCDR = p.PokemonCDR,
                    PokemonImage = p.PokemonImage,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    ThemedWatches = p.ThemedWatches?.Select(w => new WatchDTO
                    {
                        WatchID = w.WatchID,
                        Name = w.Name,
                        Price = w.Price,
                        Description = w.Description,
                        ImageUrl = w.ImageUrl,
                        IsAvailable = w.IsAvailable,
                        StockQuantity = w.StockQuantity
                    }).ToList()
                });

                return Ok(pokemonDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PokemonAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonDTO>> GetPokemon(int id)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
                if (pokemon == null)
                {
                    return NotFound($"Pokemon with ID {id} not found.");
                }

                var pokemonDTO = new PokemonDTO
                {
                    PokemonId = pokemon.PokemonId,
                    PokemonName = pokemon.PokemonName,
                    PokemonRole = pokemon.PokemonRole,
                    PokemonStyle = pokemon.PokemonStyle,
                    Description = pokemon.Description,
                    PokemonHP = pokemon.PokemonHP,
                    PokemonAttack = pokemon.PokemonAttack,
                    PokemonDefense = pokemon.PokemonDefense,
                    PokemonSpAttack = pokemon.PokemonSpAttack,
                    PokemonSpDefense = pokemon.PokemonSpDefense,
                    PokemonCDR = pokemon.PokemonCDR,
                    PokemonImage = pokemon.PokemonImage,
                    CreatedAt = pokemon.CreatedAt,
                    UpdatedAt = pokemon.UpdatedAt,
                    ThemedWatches = pokemon.ThemedWatches?.Select(w => new WatchDTO
                    {
                        WatchID = w.WatchID,
                        Name = w.Name,
                        Price = w.Price,
                        Description = w.Description,
                        ImageUrl = w.ImageUrl,
                        IsAvailable = w.IsAvailable,
                        StockQuantity = w.StockQuantity
                    }).ToList()
                };

                return Ok(pokemonDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PokemonAPI
        [HttpPost]
        public async Task<ActionResult<PokemonDTO>> CreatePokemon([FromBody] Pokemon pokemon)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdPokemon = await _pokemonService.CreatePokemonAsync(pokemon);
                var pokemonDTO = new PokemonDTO
                {
                    PokemonId = createdPokemon.PokemonId,
                    PokemonName = createdPokemon.PokemonName,
                    PokemonRole = createdPokemon.PokemonRole,
                    PokemonStyle = createdPokemon.PokemonStyle,
                    Description = createdPokemon.Description,
                    PokemonHP = createdPokemon.PokemonHP,
                    PokemonAttack = createdPokemon.PokemonAttack,
                    PokemonDefense = createdPokemon.PokemonDefense,
                    PokemonSpAttack = createdPokemon.PokemonSpAttack,
                    PokemonSpDefense = createdPokemon.PokemonSpDefense,
                    PokemonCDR = createdPokemon.PokemonCDR,
                    PokemonImage = createdPokemon.PokemonImage,
                    CreatedAt = createdPokemon.CreatedAt
                };

                return CreatedAtAction(nameof(GetPokemon), new { id = pokemonDTO.PokemonId }, pokemonDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PokemonAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon pokemon)
        {
            try
            {
                if (id != pokemon.PokemonId)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedPokemon = await _pokemonService.UpdatePokemonAsync(id, pokemon);
                if (updatedPokemon == null)
                {
                    return NotFound($"Pokemon with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PokemonAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            try
            {
                var result = await _pokemonService.DeletePokemonAsync(id);
                if (!result)
                {
                    return NotFound($"Pokemon with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PokemonAPI/top/5
        [HttpGet("top/{count}")]
        public async Task<ActionResult<IEnumerable<PokemonDTO>>> GetTopPokemons(int count)
        {
            try
            {
                var pokemons = await _pokemonService.GetTopPokemonAsync(count);
                var pokemonDTOs = pokemons.Select(p => new PokemonDTO
                {
                    PokemonId = p.PokemonId,
                    PokemonName = p.PokemonName,
                    PokemonRole = p.PokemonRole,
                    PokemonStyle = p.PokemonStyle,
                    PokemonImage = p.PokemonImage
                });

                return Ok(pokemonDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PokemonAPI/5/helditems
        [HttpGet("{id}/helditems")]
        public async Task<ActionResult<IEnumerable<HeldItemDTO>>> GetPokemonHeldItems(int id)
        {
            try
            {
                if (!await _pokemonService.ValidatePokemonExistsAsync(id))
                {
                    return NotFound($"Pokemon with ID {id} not found.");
                }

                var heldItems = await _pokemonService.GetHeldItemsForPokemonAsync(id);
                var heldItemDTOs = heldItems.Select(h => new HeldItemDTO
                {
                    HeldItemId = h.HeldItemId,
                    HeldItemName = h.HeldItemName,
                    HeldItemHP = h.HeldItemHP,
                    HeldItemAttack = h.HeldItemAttack,
                    HeldItemDefense = h.HeldItemDefense,
                    HeldItemSpAttack = h.HeldItemSpAttack,
                    HeldItemSpDefense = h.HeldItemSpDefense,
                    HeldItemCDR = h.HeldItemCDR,
                    HeldItemImage = h.HeldItemImage
                });

                return Ok(heldItemDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}