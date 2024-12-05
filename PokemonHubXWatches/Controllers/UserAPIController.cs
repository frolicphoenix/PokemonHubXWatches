using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;
        private readonly IBuildService _buildService;

        public UserAPIController(
            IUserService userService,
            IReservationService reservationService,
            IBuildService buildService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            _buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var userDTOs = users.Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    LastLogin = u.LastLogin
                });

                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    LastLogin = user.LastLogin
                };

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdUser = await _userService.CreateUserAsync(user);
                var userDTO = new UserDTO
                {
                    UserId = createdUser.UserId,
                    UserName = createdUser.UserName,
                    Email = createdUser.Email,
                    Role = createdUser.Role,
                    CreatedAt = createdUser.CreatedAt,
                    UpdatedAt = createdUser.UpdatedAt,
                    LastLogin = createdUser.LastLogin
                };

                return CreatedAtAction(nameof(GetUser), new { id = userDTO.UserId }, userDTO);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.UserId)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedUser = await _userService.UpdateUserAsync(id, user);
                if (updatedUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}/reservations")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetUserReservations(int id)
        {
            try
            {
                if (!await _userService.ValidateUserExistsAsync(id))
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var reservations = await _reservationService.GetReservationsByUserIdAsync(id);
                var reservationDTOs = reservations.Select(r => new ReservationDTO
                {
                    ReservationID = r.ReservationID,
                    ReservationDate = r.ReservationDate,
                    EndDate = r.EndDate,
                    Status = r.Status,
                    TotalPrice = r.TotalPrice,
                    UserID = r.UserId,
                    WatchID = r.WatchID,
                    WatchName = r.Watch.Name
                });

                return Ok(reservationDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}/builds")]
        public async Task<ActionResult<IEnumerable<BuildDTO>>> GetUserBuilds(int id)
        {
            try
            {
                if (!await _userService.ValidateUserExistsAsync(id))
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var builds = await _buildService.GetBuildsByUserIdAsync(id);
                var buildDTOs = builds.Select(b => new BuildDTO
                {
                    BuildId = b.BuildId,
                    PokemonId = b.PokemonId,
                    PokemonUpdatedHP = b.PokemonUpdatedHP,
                    PokemonUpdatedAttack = b.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = b.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = b.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = b.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = b.PokemonUpdatedCDR,
                    Pokemon = new PokemonDTO
                    {
                        PokemonId = b.Pokemon.PokemonId,
                        PokemonName = b.Pokemon.PokemonName,
                        PokemonImage = b.Pokemon.PokemonImage
                    }
                });

                return Ok(buildDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDTO>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return NotFound($"User with email {email} not found.");
                }

                var userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    LastLogin = user.LastLogin
                };

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}