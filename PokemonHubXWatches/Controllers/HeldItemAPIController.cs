using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeldItemAPIController : ControllerBase
    {
        private readonly IHeldItemService _heldItemService;

        public HeldItemAPIController(IHeldItemService heldItemService)
        {
            _heldItemService = heldItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var heldItems = await _heldItemService.GetAllAsync();
            return Ok(heldItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var heldItem = await _heldItemService.GetByIdAsync(id);
            if (heldItem == null)
                return NotFound();
            return Ok(heldItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HeldItem heldItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _heldItemService.AddAsync(heldItem);
            return CreatedAtAction(nameof(GetById), new { id = heldItem.HeldItemId }, heldItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HeldItem heldItem)
        {
            if (id != heldItem.HeldItemId)
                return BadRequest("HeldItem ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _heldItemService.UpdateAsync(heldItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHeldItem = await _heldItemService.GetByIdAsync(id);
            if (existingHeldItem == null)
                return NotFound();

            await _heldItemService.DeleteAsync(id);
            return NoContent();
        }
    }
}
