using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildAPIController : ControllerBase
    {
        private readonly IBuildService _buildService;
        private readonly IPokemonService _pokemonService;
        private readonly IHeldItemService _heldItemService;

        public BuildAPIController(
            IBuildService buildService,
            IPokemonService pokemonService,
            IHeldItemService heldItemService)
        {
            _buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
            _pokemonService = pokemonService ?? throw new ArgumentNullException(nameof(pokemonService));
            _heldItemService = heldItemService ?? throw new ArgumentNullException(nameof(heldItemService));
        }

        // GET: api/BuildAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildDTO>>> GetBuilds()
        {
            try
            {
                var builds = await _buildService.GetAllBuildsAsync();
                var buildDTOs = builds.Select(b => new BuildDTO
                {
                    BuildId = b.BuildId,
                    PokemonUpdatedHP = b.PokemonUpdatedHP,
                    PokemonUpdatedAttack = b.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = b.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = b.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = b.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = b.PokemonUpdatedCDR,
                    PokemonId = b.PokemonId,
                    Pokemon = new PokemonDTO
                    {
                        PokemonId = b.Pokemon.PokemonId,
                        PokemonName = b.Pokemon.PokemonName,
                        PokemonImage = b.Pokemon.PokemonImage
                    },
                    HeldItems = b.HeldItems.Select(hi => new HeldItemDTO
                    {
                        HeldItemId = hi.HeldItem.HeldItemId,
                        HeldItemName = hi.HeldItem.HeldItemName,
                        HeldItemImage = hi.HeldItem.HeldItemImage
                    }).ToList()
                });

                return Ok(buildDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/BuildAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildDTO>> GetBuild(int id)
        {
            try
            {
                var build = await _buildService.GetBuildByIdAsync(id);
                if (build == null)
                {
                    return NotFound($"Build with ID {id} not found.");
                }

                var buildDTO = new BuildDTO
                {
                    BuildId = build.BuildId,
                    PokemonUpdatedHP = build.PokemonUpdatedHP,
                    PokemonUpdatedAttack = build.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = build.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = build.PokemonUpdatedCDR,
                    PokemonId = build.PokemonId,
                    Pokemon = new PokemonDTO
                    {
                        PokemonId = build.Pokemon.PokemonId,
                        PokemonName = build.Pokemon.PokemonName,
                        PokemonImage = build.Pokemon.PokemonImage
                    },
                    HeldItems = build.HeldItems.Select(hi => new HeldItemDTO
                    {
                        HeldItemId = hi.HeldItem.HeldItemId,
                        HeldItemName = hi.HeldItem.HeldItemName,
                        HeldItemImage = hi.HeldItem.HeldItemImage
                    }).ToList()
                };

                return Ok(buildDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/BuildAPI
        [HttpPost]
        public async Task<ActionResult<BuildDTO>> CreateBuild([FromBody] Build build)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate Pokemon exists
                if (!await _pokemonService.ValidatePokemonExistsAsync(build.PokemonId))
                {
                    return BadRequest($"Pokemon with ID {build.PokemonId} does not exist.");
                }

                var createdBuild = await _buildService.CreateBuildAsync(build);
                var buildDTO = new BuildDTO
                {
                    BuildId = createdBuild.BuildId,
                    PokemonUpdatedHP = createdBuild.PokemonUpdatedHP,
                    PokemonUpdatedAttack = createdBuild.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = createdBuild.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = createdBuild.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = createdBuild.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = createdBuild.PokemonUpdatedCDR,
                    PokemonId = createdBuild.PokemonId
                };

                return CreatedAtAction(nameof(GetBuild), new { id = buildDTO.BuildId }, buildDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/BuildAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBuild(int id, [FromBody] Build build)
        {
            try
            {
                if (id != build.BuildId)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedBuild = await _buildService.UpdateBuildAsync(id, build);
                if (updatedBuild == null)
                {
                    return NotFound($"Build with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/BuildAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuild(int id)
        {
            try
            {
                var result = await _buildService.DeleteBuildAsync(id);
                if (!result)
                {
                    return NotFound($"Build with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/BuildAPI/5/helditems/3
        [HttpPost("{buildId}/helditems/{heldItemId}")]
        public async Task<ActionResult<BuildDTO>> AddHeldItemToBuild(int buildId, int heldItemId)
        {
            try
            {
                // Validate held item exists
                if (!await _heldItemService.ValidateHeldItemExistsAsync(heldItemId))
                {
                    return BadRequest($"Held item with ID {heldItemId} does not exist.");
                }

                var updatedBuild = await _buildService.AddHeldItemToBuildAsync(buildId, heldItemId);
                if (updatedBuild == null)
                {
                    return NotFound($"Build with ID {buildId} not found.");
                }

                var buildDTO = new BuildDTO
                {
                    BuildId = updatedBuild.BuildId,
                    PokemonUpdatedHP = updatedBuild.PokemonUpdatedHP,
                    PokemonUpdatedAttack = updatedBuild.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = updatedBuild.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = updatedBuild.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = updatedBuild.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = updatedBuild.PokemonUpdatedCDR,
                    PokemonId = updatedBuild.PokemonId,
                    HeldItems = updatedBuild.HeldItems.Select(hi => new HeldItemDTO
                    {
                        HeldItemId = hi.HeldItem.HeldItemId,
                        HeldItemName = hi.HeldItem.HeldItemName,
                        HeldItemImage = hi.HeldItem.HeldItemImage
                    }).ToList()
                };

                return Ok(buildDTO);
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

        // DELETE: api/BuildAPI/5/helditems/3
        [HttpDelete("{buildId}/helditems/{heldItemId}")]
        public async Task<IActionResult> RemoveHeldItemFromBuild(int buildId, int heldItemId)
        {
            try
            {
                var result = await _buildService.RemoveHeldItemFromBuildAsync(buildId, heldItemId);
                if (!result)
                {
                    return NotFound($"Build with ID {buildId} or held item with ID {heldItemId} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}