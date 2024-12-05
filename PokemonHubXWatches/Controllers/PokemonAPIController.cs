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

        /// <summary>
        /// Initializes a new instance of the PokemonAPIController.
        /// </summary>
        /// <param name="pokemonService">The Pokemon service instance.</param>
        public PokemonAPIController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService ?? throw new ArgumentNullException(nameof(pokemonService));
        }

        // GET: api/PokemonAPI
        /// <summary>
        /// Gets all Pokemon from the database.
        /// </summary>
        /// <returns>A list of PokemonDTOs.</returns>
        /// <example>GET api/pokemonapi</example>
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
        /// <summary>
        /// Gets a specific Pokemon by ID.
        /// </summary>
        /// <param name="id">The ID of the Pokemon.</param>
        /// <returns>A PokemonDTO if found.</returns>
        /// <example>GET api/pokemonapi/5</example>
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
        /// <summary>
        /// Creates a new Pokemon.
        /// </summary>
        /// <param name="pokemon">The Pokemon data to create.</param>
        /// <returns>The created PokemonDTO.</returns>
        /// <example>POST api/pokemonapi</example>
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
        /// <summary>
        /// Updates an existing Pokemon.
        /// </summary>
        /// <param name="id">The ID of the Pokemon to update.</param>
        /// <param name="pokemon">The updated Pokemon data.</param>
        /// <returns>No content if successful.</returns>
        /// <example>PUT api/pokemonapi/5</example>
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
        /// <summary>
        /// Deletes a Pokemon.
        /// </summary>
        /// <param name="id">The ID of the Pokemon to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <example>DELETE api/pokemonapi/5</example>
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
        /// <summary>
        /// Gets the top Pokemon by specified count.
        /// </summary>
        /// <param name="count">Number of Pokemon to return.</param>
        /// <returns>A list of PokemonDTOs.</returns>
        /// <example>GET api/pokemonapi/top/5</example>
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
        /// <summary>
        /// Gets all held items for a specific Pokemon.
        /// </summary>
        /// <param name="id">The Pokemon ID.</param>
        /// <returns>A list of HeldItemDTOs.</returns>
        /// <example>GET api/pokemonapi/5/helditems</example>
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