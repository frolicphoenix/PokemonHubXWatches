using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class HeldItemController : Controller
    {
        private readonly IHeldItemService _heldItemService;

        public HeldItemController(IHeldItemService heldItemService)
        {
            _heldItemService = heldItemService;
        }

        // GET: HeldItem
        public async Task<IActionResult> Index()
        {
            var heldItems = await _heldItemService.GetAllHeldItemsAsync();
            var viewModels = heldItems.Select(h => new HeldItemViewModel
            {
                HeldItemId = h.HeldItemId,
                HeldItemName = h.HeldItemName,
                Description = h.Description,
                Category = h.Category,
                HeldItemHP = h.HeldItemHP,
                HeldItemAttack = h.HeldItemAttack,
                HeldItemDefense = h.HeldItemDefense,
                HeldItemSpAttack = h.HeldItemSpAttack,
                HeldItemSpDefense = h.HeldItemSpDefense,
                HeldItemCDR = h.HeldItemCDR,
                HeldItemImage = h.HeldItemImage,
                CreatedAt = h.CreatedAt,
                UpdatedAt = h.UpdatedAt
            }).ToList();

            return View(viewModels);
        }

        // GET: HeldItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var heldItem = await _heldItemService.GetHeldItemByIdAsync(id);
            if (heldItem == null)
            {
                return NotFound();
            }

            var viewModel = new HeldItemViewModel
            {
                HeldItemId = heldItem.HeldItemId,
                HeldItemName = heldItem.HeldItemName,
                Description = heldItem.Description,
                Category = heldItem.Category,
                HeldItemHP = heldItem.HeldItemHP,
                HeldItemAttack = heldItem.HeldItemAttack,
                HeldItemDefense = heldItem.HeldItemDefense,
                HeldItemSpAttack = heldItem.HeldItemSpAttack,
                HeldItemSpDefense = heldItem.HeldItemSpDefense,
                HeldItemCDR = heldItem.HeldItemCDR,
                HeldItemImage = heldItem.HeldItemImage,
                CreatedAt = heldItem.CreatedAt,
                UpdatedAt = heldItem.UpdatedAt
            };

            return View(viewModel);
        }

        // GET: HeldItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HeldItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HeldItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var heldItem = new HeldItem
                {
                    HeldItemName = viewModel.HeldItemName,
                    Description = viewModel.Description,
                    Category = viewModel.Category,
                    HeldItemHP = viewModel.HeldItemHP,
                    HeldItemAttack = viewModel.HeldItemAttack,
                    HeldItemDefense = viewModel.HeldItemDefense,
                    HeldItemSpAttack = viewModel.HeldItemSpAttack,
                    HeldItemSpDefense = viewModel.HeldItemSpDefense,
                    HeldItemCDR = viewModel.HeldItemCDR,
                    HeldItemImage = viewModel.HeldItemImage
                };

                await _heldItemService.CreateHeldItemAsync(heldItem);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: HeldItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var heldItem = await _heldItemService.GetHeldItemByIdAsync(id);
            if (heldItem == null)
            {
                return NotFound();
            }

            var viewModel = new HeldItemViewModel
            {
                HeldItemId = heldItem.HeldItemId,
                HeldItemName = heldItem.HeldItemName,
                Description = heldItem.Description,
                Category = heldItem.Category,
                HeldItemHP = heldItem.HeldItemHP,
                HeldItemAttack = heldItem.HeldItemAttack,
                HeldItemDefense = heldItem.HeldItemDefense,
                HeldItemSpAttack = heldItem.HeldItemSpAttack,
                HeldItemSpDefense = heldItem.HeldItemSpDefense,
                HeldItemCDR = heldItem.HeldItemCDR,
                HeldItemImage = heldItem.HeldItemImage
            };

            return View(viewModel);
        }

        // POST: HeldItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HeldItemViewModel viewModel)
        {
            if (id != viewModel.HeldItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var heldItem = new HeldItem
                {
                    HeldItemId = viewModel.HeldItemId,
                    HeldItemName = viewModel.HeldItemName,
                    Description = viewModel.Description,
                    Category = viewModel.Category,
                    HeldItemHP = viewModel.HeldItemHP,
                    HeldItemAttack = viewModel.HeldItemAttack,
                    HeldItemDefense = viewModel.HeldItemDefense,
                    HeldItemSpAttack = viewModel.HeldItemSpAttack,
                    HeldItemSpDefense = viewModel.HeldItemSpDefense,
                    HeldItemCDR = viewModel.HeldItemCDR,
                    HeldItemImage = viewModel.HeldItemImage
                };

                await _heldItemService.UpdateHeldItemAsync(id, heldItem);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: HeldItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var heldItem = await _heldItemService.GetHeldItemByIdAsync(id);
            if (heldItem == null)
            {
                return NotFound();
            }

            var viewModel = new HeldItemViewModel
            {
                HeldItemId = heldItem.HeldItemId,
                HeldItemName = heldItem.HeldItemName,
                Description = heldItem.Description,
                Category = heldItem.Category,
                HeldItemHP = heldItem.HeldItemHP,
                HeldItemAttack = heldItem.HeldItemAttack,
                HeldItemDefense = heldItem.HeldItemDefense,
                HeldItemSpAttack = heldItem.HeldItemSpAttack,
                HeldItemSpDefense = heldItem.HeldItemSpDefense,
                HeldItemCDR = heldItem.HeldItemCDR,
                HeldItemImage = heldItem.HeldItemImage
            };

            return View(viewModel);
        }

        // POST: HeldItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _heldItemService.DeleteHeldItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}