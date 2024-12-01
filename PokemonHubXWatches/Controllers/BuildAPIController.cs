using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildAPIController : ControllerBase
    {
        private readonly IBuildService _buildService;

        public BuildAPIController(IBuildService buildService)
        {
            _buildService = buildService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var builds = await _buildService.GetAllAsync();
            return Ok(builds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var build = await _buildService.GetByIdAsync(id);
            if (build == null)
                return NotFound();
            return Ok(build);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Build build)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _buildService.AddAsync(build);
            return CreatedAtAction(nameof(GetById), new { id = build.BuildId }, build);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Build build)
        {
            if (id != build.BuildId)
                return BadRequest("Build ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _buildService.UpdateAsync(build);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingBuild = await _buildService.GetByIdAsync(id);
            if (existingBuild == null)
                return NotFound();

            await _buildService.DeleteAsync(id);
            return NoContent();
        }
    }
}
