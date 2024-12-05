using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class BuildController : Controller
    {
        private readonly IBuildService _buildService;
        private readonly IPokemonService _pokemonService;
        private readonly IUserService _userService;
        private readonly IHeldItemService _heldItemService;

        public BuildController(
            IBuildService buildService,
            IPokemonService pokemonService,
            IUserService userService,
            IHeldItemService heldItemService)
        {
            _buildService = buildService;
            _pokemonService = pokemonService;
            _userService = userService;
            _heldItemService = heldItemService;
        }

        // GET: Build
        public async Task<IActionResult> Index()
        {
            var builds = await _buildService.GetAllBuildsAsync();
            var viewModels = builds.Select(b => new BuildViewModel
            {
                BuildId = b.BuildId,
                BuildName = b.BuildName,
                PokemonId = b.PokemonId,
                UserId = b.UserId,
                PokemonUpdatedHP = b.PokemonUpdatedHP,
                PokemonUpdatedAttack = b.PokemonUpdatedAttack,
                PokemonUpdatedDefense = b.PokemonUpdatedDefense,
                PokemonUpdatedSpAttack = b.PokemonUpdatedSpAttack,
                PokemonUpdatedSpDefense = b.PokemonUpdatedSpDefense,
                PokemonUpdatedCDR = b.PokemonUpdatedCDR,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                Pokemon = new PokemonViewModel
                {
                    PokemonId = b.Pokemon.PokemonId,
                    PokemonName = b.Pokemon.PokemonName,
                    PokemonImage = b.Pokemon.PokemonImage
                }
            }).ToList();

            return View(viewModels);
        }

        // GET: Build/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var build = await _buildService.GetBuildByIdAsync(id);
            if (build == null)
            {
                return NotFound();
            }

            var viewModel = new BuildViewModel
            {
                BuildId = build.BuildId,
                BuildName = build.BuildName,
                PokemonId = build.PokemonId,
                UserId = build.UserId,
                PokemonUpdatedHP = build.PokemonUpdatedHP,
                PokemonUpdatedAttack = build.PokemonUpdatedAttack,
                PokemonUpdatedDefense = build.PokemonUpdatedDefense,
                PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack,
                PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense,
                PokemonUpdatedCDR = build.PokemonUpdatedCDR,
                CreatedAt = build.CreatedAt,
                UpdatedAt = build.UpdatedAt,
                Pokemon = new PokemonViewModel
                {
                    PokemonId = build.Pokemon.PokemonId,
                    PokemonName = build.Pokemon.PokemonName,
                    PokemonImage = build.Pokemon.PokemonImage
                }
            };

            return View(viewModel);
        }

        // GET: Build/Create
        public async Task<IActionResult> Create()
        {
            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "UserId", "UserName");

            return View();
        }

        // POST: Build/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BuildViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var build = new Build
                {
                    BuildName = viewModel.BuildName,
                    PokemonId = viewModel.PokemonId,
                    UserId = viewModel.UserId,
                    PokemonUpdatedHP = viewModel.PokemonUpdatedHP,
                    PokemonUpdatedAttack = viewModel.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = viewModel.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = viewModel.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = viewModel.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = viewModel.PokemonUpdatedCDR
                };

                await _buildService.CreateBuildAsync(build);
                return RedirectToAction(nameof(Index));
            }

            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "UserId", "UserName");

            return View(viewModel);
        }

        // GET: Build/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var build = await _buildService.GetBuildByIdAsync(id);
            if (build == null)
            {
                return NotFound();
            }

            var viewModel = new BuildViewModel
            {
                BuildId = build.BuildId,
                BuildName = build.BuildName,
                PokemonId = build.PokemonId,
                UserId = build.UserId,
                PokemonUpdatedHP = build.PokemonUpdatedHP,
                PokemonUpdatedAttack = build.PokemonUpdatedAttack,
                PokemonUpdatedDefense = build.PokemonUpdatedDefense,
                PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack,
                PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense,
                PokemonUpdatedCDR = build.PokemonUpdatedCDR
            };

            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "UserId", "UserName");

            return View(viewModel);
        }

        // POST: Build/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BuildViewModel viewModel)
        {
            if (id != viewModel.BuildId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var build = new Build
                {
                    BuildId = viewModel.BuildId,
                    BuildName = viewModel.BuildName,
                    PokemonId = viewModel.PokemonId,
                    UserId = viewModel.UserId,
                    PokemonUpdatedHP = viewModel.PokemonUpdatedHP,
                    PokemonUpdatedAttack = viewModel.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = viewModel.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = viewModel.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = viewModel.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = viewModel.PokemonUpdatedCDR
                };

                await _buildService.UpdateBuildAsync(id, build);
                return RedirectToAction(nameof(Index));
            }

            var pokemons = await _pokemonService.GetAllPokemonAsync();
            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "UserId", "UserName");

            return View(viewModel);
        }

        // GET: Build/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var build = await _buildService.GetBuildByIdAsync(id);
            if (build == null)
            {
                return NotFound();
            }

            var viewModel = new BuildViewModel
            {
                BuildId = build.BuildId,
                BuildName = build.BuildName,
                PokemonId = build.PokemonId,
                UserId = build.UserId,
                PokemonUpdatedHP = build.PokemonUpdatedHP,
                PokemonUpdatedAttack = build.PokemonUpdatedAttack,
                PokemonUpdatedDefense = build.PokemonUpdatedDefense,
                PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack,
                PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense,
                PokemonUpdatedCDR = build.PokemonUpdatedCDR,
                Pokemon = new PokemonViewModel
                {
                    PokemonId = build.Pokemon.PokemonId,
                    PokemonName = build.Pokemon.PokemonName
                }
            };

            return View(viewModel);
        }

        // POST: Build/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _buildService.DeleteBuildAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}