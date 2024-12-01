using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;

namespace PokemonHubXWatches.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly ApplicationDbContext _context;

        public PokemonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> GetAllAsync()
        {
            return await _context.Pokemons.Include(p => p.ThemedWatch).ToListAsync();
        }

        public async Task<Pokemon?> GetByIdAsync(int id)
        {
            return await _context.Pokemons.Include(p => p.ThemedWatch).FirstOrDefaultAsync(p => p.PokemonId == id);
        }

        public async Task AddAsync(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
                await _context.SaveChangesAsync();
            }
        }
    }
}
