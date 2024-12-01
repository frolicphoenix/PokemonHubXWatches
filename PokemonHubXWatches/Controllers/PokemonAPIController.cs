using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonAPIController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonAPIController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pokemons = await _pokemonService.GetAllAsync();
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);
            if (pokemon == null)
                return NotFound();
            return Ok(pokemon);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pokemon pokemon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pokemonService.AddAsync(pokemon);
            return CreatedAtAction(nameof(GetById), new { id = pokemon.PokemonId }, pokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pokemon pokemon)
        {
            if (id != pokemon.PokemonId)
                return BadRequest("Pokemon ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pokemonService.UpdateAsync(pokemon);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPokemon = await _pokemonService.GetByIdAsync(id);
            if (existingPokemon == null)
                return NotFound();

            await _pokemonService.DeleteAsync(id);
            return NoContent();
        }
    }
}
