using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IWatchService _watchService;

        public ReservationController(
            IReservationService reservationService,
            IUserService userService,
            IWatchService watchService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _watchService = watchService;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            var viewModels = reservations.Select(r => new ReservationViewModel
            {
                ReservationID = r.ReservationID,
                ReservationDate = r.ReservationDate,
                EndDate = r.EndDate,
                Status = r.Status,
                TotalPrice = r.TotalPrice,
                Notes = r.Notes,
                UserId = r.UserId,
                WatchID = r.WatchID,
                UserName = r.User.UserName,
                WatchName = r.Watch.Name,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();

            return View(viewModels);
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var viewModel = new ReservationViewModel
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                TotalPrice = reservation.TotalPrice,
                Notes = reservation.Notes,
                UserId = reservation.UserId,
                WatchID = reservation.WatchID,
                UserName = reservation.User.UserName,
                WatchName = reservation.Watch.Name,
                CreatedAt = reservation.CreatedAt,
                UpdatedAt = reservation.UpdatedAt
            };

            return View(viewModel);
        }

        // GET: Reservation/Create
        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAllUsersAsync();
            var watches = await _watchService.GetAvailableWatchesAsync();

            ViewBag.Users = new SelectList(users, "UserId", "UserName");
            ViewBag.Watches = new SelectList(watches, "WatchID", "Name");

            return View();
        }

        // POST: Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    ReservationDate = viewModel.ReservationDate,
                    EndDate = viewModel.EndDate,
                    Status = ReservationStatus.Pending,
                    TotalPrice = viewModel.TotalPrice,
                    Notes = viewModel.Notes,
                    UserId = viewModel.UserId,
                    WatchID = viewModel.WatchID
                };

                await _reservationService.CreateReservationAsync(reservation);
                return RedirectToAction(nameof(Index));
            }

            var users = await _userService.GetAllUsersAsync();
            var watches = await _watchService.GetAvailableWatchesAsync();

            ViewBag.Users = new SelectList(users, "UserId", "UserName");
            ViewBag.Watches = new SelectList(watches, "WatchID", "Name");

            return View(viewModel);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var viewModel = new ReservationViewModel
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                TotalPrice = reservation.TotalPrice,
                Notes = reservation.Notes,
                UserId = reservation.UserId,
                WatchID = reservation.WatchID
            };

            var users = await _userService.GetAllUsersAsync();
            var watches = await _watchService.GetAllWatchesAsync();

            ViewBag.Users = new SelectList(users, "UserId", "UserName", reservation.UserId);
            ViewBag.Watches = new SelectList(watches, "WatchID", "Name", reservation.WatchID);

            return View(viewModel);
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservationViewModel viewModel)
        {
            if (id != viewModel.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    ReservationID = viewModel.ReservationID,
                    ReservationDate = viewModel.ReservationDate,
                    EndDate = viewModel.EndDate,
                    Status = viewModel.Status,
                    TotalPrice = viewModel.TotalPrice,
                    Notes = viewModel.Notes,
                    UserId = viewModel.UserId,
                    WatchID = viewModel.WatchID
                };

                await _reservationService.UpdateReservationAsync(id, reservation);
                return RedirectToAction(nameof(Index));
            }

            var users = await _userService.GetAllUsersAsync();
            var watches = await _watchService.GetAllWatchesAsync();

            ViewBag.Users = new SelectList(users, "UserId", "UserName", viewModel.UserId);
            ViewBag.Watches = new SelectList(watches, "WatchID", "Name", viewModel.WatchID);

            return View(viewModel);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var viewModel = new ReservationViewModel
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                TotalPrice = reservation.TotalPrice,
                Notes = reservation.Notes,
                UserId = reservation.UserId,
                WatchID = reservation.WatchID,
                UserName = reservation.User.UserName,
                WatchName = reservation.Watch.Name
            };

            return View(viewModel);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}