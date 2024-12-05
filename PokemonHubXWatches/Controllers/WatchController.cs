using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class WatchController : Controller
    {
        private readonly IWatchService _watchService;
        private readonly IPokemonService _pokemonService;

        public WatchController(IWatchService watchService, IPokemonService pokemonService)
        {
            _watchService = watchService;
            _pokemonService = pokemonService;
        }

        // GET: Watch
        public async Task<IActionResult> Index()
        {
            var watches = await _watchService.GetAllWatchesAsync();
            var viewModels = watches.Select(w => new WatchViewModel
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
            }).ToList();

            return View(viewModels);
        }

        // GET: Watch/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var watch = await _watchService.GetWatchByIdAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            var viewModel = new WatchViewModel
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

            return View(viewModel);
        }

        // GET: Watch/Create
        public async Task<IActionResult> Create()
        {
            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");
            return View();
        }

        // POST: Watch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WatchViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var watch = new Watch
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    ImageUrl = viewModel.ImageUrl,
                    IsAvailable = viewModel.IsAvailable,
                    StockQuantity = viewModel.StockQuantity,
                    PokemonID = viewModel.PokemonID
                };

                await _watchService.CreateWatchAsync(watch);
                return RedirectToAction(nameof(Index));
            }

            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");
            return View(viewModel);
        }

        // GET: Watch/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var watch = await _watchService.GetWatchByIdAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            var viewModel = new WatchViewModel
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Description = watch.Description,
                Price = watch.Price,
                ImageUrl = watch.ImageUrl,
                IsAvailable = watch.IsAvailable,
                StockQuantity = watch.StockQuantity,
                PokemonID = watch.PokemonID
            };

            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName", watch.PokemonID);
            return View(viewModel);
        }

        // POST: Watch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WatchViewModel viewModel)
        {
            if (id != viewModel.WatchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var watch = new Watch
                {
                    WatchID = viewModel.WatchID,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    ImageUrl = viewModel.ImageUrl,
                    IsAvailable = viewModel.IsAvailable,
                    StockQuantity = viewModel.StockQuantity,
                    PokemonID = viewModel.PokemonID
                };

                await _watchService.UpdateWatchAsync(id, watch);
                return RedirectToAction(nameof(Index));
            }

            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName", viewModel.PokemonID);
            return View(viewModel);
        }

        // GET: Watch/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var watch = await _watchService.GetWatchByIdAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            var viewModel = new WatchViewModel
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Description = watch.Description,
                Price = watch.Price,
                ImageUrl = watch.ImageUrl,
                IsAvailable = watch.IsAvailable,
                StockQuantity = watch.StockQuantity,
                PokemonID = watch.PokemonID,
                ThemedPokemonName = watch.ThemedPokemon?.PokemonName
            };

            return View(viewModel);
        }

        // POST: Watch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _watchService.DeleteWatchAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}