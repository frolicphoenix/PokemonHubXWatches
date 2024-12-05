using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class PokemonController : Controller
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        // GET: Pokemon
        public async Task<IActionResult> Index()
        {
            var pokemons = await _pokemonService.GetAllPokemonAsync();
            var viewModels = pokemons.Select(p => new PokemonViewModel
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
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return View(viewModels);
        }

        // GET: Pokemon/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            var viewModel = new PokemonViewModel
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
                UpdatedAt = pokemon.UpdatedAt
            };

            return View(viewModel);
        }

        // GET: Pokemon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokemon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PokemonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var pokemon = new Pokemon
                {
                    PokemonName = viewModel.PokemonName,
                    PokemonRole = viewModel.PokemonRole,
                    PokemonStyle = viewModel.PokemonStyle,
                    Description = viewModel.Description,
                    PokemonHP = viewModel.PokemonHP,
                    PokemonAttack = viewModel.PokemonAttack,
                    PokemonDefense = viewModel.PokemonDefense,
                    PokemonSpAttack = viewModel.PokemonSpAttack,
                    PokemonSpDefense = viewModel.PokemonSpDefense,
                    PokemonCDR = viewModel.PokemonCDR,
                    PokemonImage = viewModel.PokemonImage
                };

                await _pokemonService.CreatePokemonAsync(pokemon);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Pokemon/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            var viewModel = new PokemonViewModel
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
                PokemonImage = pokemon.PokemonImage
            };

            return View(viewModel);
        }

        // POST: Pokemon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PokemonViewModel viewModel)
        {
            if (id != viewModel.PokemonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var pokemon = new Pokemon
                {
                    PokemonId = viewModel.PokemonId,
                    PokemonName = viewModel.PokemonName,
                    PokemonRole = viewModel.PokemonRole,
                    PokemonStyle = viewModel.PokemonStyle,
                    Description = viewModel.Description,
                    PokemonHP = viewModel.PokemonHP,
                    PokemonAttack = viewModel.PokemonAttack,
                    PokemonDefense = viewModel.PokemonDefense,
                    PokemonSpAttack = viewModel.PokemonSpAttack,
                    PokemonSpDefense = viewModel.PokemonSpDefense,
                    PokemonCDR = viewModel.PokemonCDR,
                    PokemonImage = viewModel.PokemonImage
                };

                await _pokemonService.UpdatePokemonAsync(id, pokemon);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Pokemon/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            var viewModel = new PokemonViewModel
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
                PokemonImage = pokemon.PokemonImage
            };

            return View(viewModel);
        }

        // POST: Pokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pokemonService.DeletePokemonAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}