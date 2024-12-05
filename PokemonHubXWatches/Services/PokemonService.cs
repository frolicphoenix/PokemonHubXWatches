using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly ApplicationDbContext _context;

        public PokemonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> GetAllPokemonAsync()
        {
            return await _context.Pokemons
                .Include(p => p.ThemedWatches)
                .ToListAsync();
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            return await _context.Pokemons
                .Include(p => p.ThemedWatches)
                .Include(p => p.Builds)
                .FirstOrDefaultAsync(p => p.PokemonId == id);
        }

        public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemon)
        {
            var existingPokemon = await GetPokemonByIdAsync(id);
            if (existingPokemon == null) return null;

            existingPokemon.PokemonName = pokemon.PokemonName;
            existingPokemon.PokemonRole = pokemon.PokemonRole;
            existingPokemon.PokemonStyle = pokemon.PokemonStyle;
            existingPokemon.Description = pokemon.Description;
            existingPokemon.PokemonHP = pokemon.PokemonHP;
            existingPokemon.PokemonAttack = pokemon.PokemonAttack;
            existingPokemon.PokemonDefense = pokemon.PokemonDefense;
            existingPokemon.PokemonSpAttack = pokemon.PokemonSpAttack;
            existingPokemon.PokemonSpDefense = pokemon.PokemonSpDefense;
            existingPokemon.PokemonCDR = pokemon.PokemonCDR;
            existingPokemon.PokemonImage = pokemon.PokemonImage;
            existingPokemon.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingPokemon;
        }

        public async Task<bool> DeletePokemonAsync(int id)
        {
            var pokemon = await GetPokemonByIdAsync(id);
            if (pokemon == null) return false;

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Pokemon>> GetTopPokemonAsync(int count)
        {
            return await _context.Pokemons
                .OrderByDescending(p => p.Builds.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<HeldItem>> GetHeldItemsForPokemonAsync(int pokemonId)
        {
            var builds = await _context.Builds
                .Include(b => b.HeldItems)
                .ThenInclude(bhi => bhi.HeldItem)
                .Where(b => b.PokemonId == pokemonId)
                .ToListAsync();

            return builds.SelectMany(b => b.HeldItems.Select(bhi => bhi.HeldItem)).Distinct();
        }

        public async Task<bool> ValidatePokemonExistsAsync(int id)
        {
            return await _context.Pokemons.AnyAsync(p => p.PokemonId == id);
        }
    }
}