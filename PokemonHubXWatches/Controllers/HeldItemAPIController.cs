using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeldItemAPIController : ControllerBase
    {
        private readonly IHeldItemService _heldItemService;

        /// <summary>
        /// Initializes a new instance of the HeldItemAPIController.
        /// </summary>
        /// <param name="heldItemService">The HeldItem service instance.</param>
        public HeldItemAPIController(IHeldItemService heldItemService)
        {
            _heldItemService = heldItemService ?? throw new ArgumentNullException(nameof(heldItemService));
        }

        // GET: api/HeldItemAPI
        /// <summary>
        /// Gets all held items from the database.
        /// </summary>
        /// <returns>A list of HeldItemDTOs.</returns>
        /// <example>GET api/helditemapi</example>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeldItemDTO>>> GetHeldItems()
        {
            try
            {
                var heldItems = await _heldItemService.GetAllHeldItemsAsync();
                var heldItemDTOs = heldItems.Select(h => new HeldItemDTO
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
                    HeldItemImage = h.HeldItemImage
                });

                return Ok(heldItemDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/HeldItemAPI/5
        /// <summary>
        /// Gets a specific held item by ID.
        /// </summary>
        /// <param name="id">The ID of the held item.</param>
        /// <returns>A HeldItemDTO if found.</returns>
        /// <example>GET api/helditemapi/5</example>
        [HttpGet("{id}")]
        public async Task<ActionResult<HeldItemDTO>> GetHeldItem(int id)
        {
            try
            {
                var heldItem = await _heldItemService.GetHeldItemByIdAsync(id);
                if (heldItem == null)
                {
                    return NotFound($"Held item with ID {id} not found.");
                }

                var heldItemDTO = new HeldItemDTO
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

                return Ok(heldItemDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/HeldItemAPI
        /// <summary>
        /// Creates a new held item.
        /// </summary>
        /// <param name="heldItem">The held item data to create.</param>
        /// <returns>The created HeldItemDTO.</returns>
        /// <example>POST api/helditemapi</example>
        [HttpPost]
        public async Task<ActionResult<HeldItemDTO>> CreateHeldItem([FromBody] HeldItem heldItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdHeldItem = await _heldItemService.CreateHeldItemAsync(heldItem);
                var heldItemDTO = new HeldItemDTO
                {
                    HeldItemId = createdHeldItem.HeldItemId,
                    HeldItemName = createdHeldItem.HeldItemName,
                    Description = createdHeldItem.Description,
                    Category = createdHeldItem.Category,
                    HeldItemHP = createdHeldItem.HeldItemHP,
                    HeldItemAttack = createdHeldItem.HeldItemAttack,
                    HeldItemDefense = createdHeldItem.HeldItemDefense,
                    HeldItemSpAttack = createdHeldItem.HeldItemSpAttack,
                    HeldItemSpDefense = createdHeldItem.HeldItemSpDefense,
                    HeldItemCDR = createdHeldItem.HeldItemCDR,
                    HeldItemImage = createdHeldItem.HeldItemImage
                };

                return CreatedAtAction(nameof(GetHeldItem), new { id = heldItemDTO.HeldItemId }, heldItemDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/HeldItemAPI/5
        /// <summary>
        /// Updates an existing held item.
        /// </summary>
        /// <param name="id">The ID of the held item to update.</param>
        /// <param name="heldItem">The updated held item data.</param>
        /// <returns>No content if successful.</returns>
        /// <example>PUT api/helditemapi/5</example>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHeldItem(int id, [FromBody] HeldItem heldItem)
        {
            try
            {
                if (id != heldItem.HeldItemId)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedHeldItem = await _heldItemService.UpdateHeldItemAsync(id, heldItem);
                if (updatedHeldItem == null)
                {
                    return NotFound($"Held item with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/HeldItemAPI/5
        /// <summary>
        /// Deletes a held item.
        /// </summary>
        /// <param name="id">The ID of the held item to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <example>DELETE api/helditemapi/5</example>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeldItem(int id)
        {
            try
            {
                var result = await _heldItemService.DeleteHeldItemAsync(id);
                if (!result)
                {
                    return NotFound($"Held item with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/HeldItemAPI/5/builds
        /// <summary>
        /// Gets all builds that use a specific held item.
        /// </summary>
        /// <param name="id">The ID of the held item.</param>
        /// <returns>A list of BuildDTOs that use the specified held item.</returns>
        /// <example>
        /// GET api/helditemapi/5/builds
        /// </example>
        [HttpGet("{id}/builds")]
        public async Task<ActionResult<IEnumerable<BuildDTO>>> GetBuildsForHeldItem(int id)
        {
            try
            {
                if (!await _heldItemService.ValidateHeldItemExistsAsync(id))
                {
                    return NotFound($"Held item with ID {id} not found.");
                }

                var builds = await _heldItemService.GetBuildsForHeldItemAsync(id);
                var buildDTOs = builds.Select(b => new BuildDTO
                {
                    BuildId = b.BuildId,
                    PokemonUpdatedHP = b.PokemonUpdatedHP,
                    PokemonUpdatedAttack = b.PokemonUpdatedAttack,
                    PokemonUpdatedDefense = b.PokemonUpdatedDefense,
                    PokemonUpdatedSpAttack = b.PokemonUpdatedSpAttack,
                    PokemonUpdatedSpDefense = b.PokemonUpdatedSpDefense,
                    PokemonUpdatedCDR = b.PokemonUpdatedCDR,
                    PokemonId = b.PokemonId
                });

                return Ok(buildDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}