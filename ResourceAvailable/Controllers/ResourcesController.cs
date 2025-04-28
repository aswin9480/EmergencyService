using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceAvailableAPI.Models;
using ResourceAvailableAPI.Repositories;

namespace ResourceAvailableAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceRepository _repository;

        public ResourcesController(IResourceRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Resources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
        {
            var resources = await _repository.GetResourcesAsync();
            return Ok(resources);
        }

        // GET: api/Resources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
        {
            var resource = await _repository.GetResourceAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // PUT: api/Resources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(int id, Resource resource)
        {
            if (id != resource.ResourceId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateResourceAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ResourceExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPut("allocate/{id}")]
        public async Task<IActionResult> AllocateResource(int id, [FromBody] int quantity)
        {
            try
            {
                await _repository.DecrementResourceQuantityAsync(id, quantity);
                return Ok(new { Message = "Resource allocated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        // POST: api/Resources
        [HttpPost]
        public async Task<ActionResult<Resource>> PostResource(Resource resource)
        {
            await _repository.AddResourceAsync(resource);
            return CreatedAtAction(nameof(GetResource), new { id = resource.ResourceId }, resource);
        }

        // DELETE: api/Resources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var resourceExists = await _repository.ResourceExistsAsync(id);
            if (!resourceExists)
            {
                return NotFound();
            }

            await _repository.DeleteResourceAsync(id);
            return NoContent();
        }
    }
}
