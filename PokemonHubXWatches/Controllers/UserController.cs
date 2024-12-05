using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;
        private readonly IBuildService _buildService;

        public UserController(
            IUserService userService,
            IReservationService reservationService,
            IBuildService buildService)
        {
            _userService = userService;
            _reservationService = reservationService;
            _buildService = buildService;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var viewModels = users.Select(u => new UserViewModel
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                LastLogin = u.LastLogin
            }).ToList();

            return View(viewModels);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var reservations = await _reservationService.GetReservationsByUserIdAsync(id);
            var builds = await _buildService.GetBuildsByUserIdAsync(id);

            var viewModel = new UserViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                LastLogin = user.LastLogin,
                Reservations = reservations.Select(r => new ReservationViewModel
                {
                    ReservationID = r.ReservationID,
                    ReservationDate = r.ReservationDate,
                    EndDate = r.EndDate,
                    Status = r.Status,
                    TotalPrice = r.TotalPrice
                }).ToList(),
                Builds = builds.Select(b => new BuildViewModel
                {
                    BuildId = b.BuildId,
                    BuildName = b.BuildName,
                    PokemonId = b.PokemonId
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    PasswordHash = viewModel.PasswordHash,
                    Role = viewModel.Role
                };

                await _userService.CreateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };

            return View(viewModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel viewModel)
        {
            if (id != viewModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserId = viewModel.UserId,
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    Role = viewModel.Role,
                    PasswordHash = viewModel.PasswordHash
                };

                await _userService.UpdateUserAsync(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                LastLogin = user.LastLogin
            };

            return View(viewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}