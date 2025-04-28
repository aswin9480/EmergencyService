using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllocateResourceAPI.Models;
using AllocateResourceAPI.Repositories;

namespace AllocateResourceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceAllocatedController : ControllerBase
    {
        private readonly IResourceAllocatedRepository _repository;

        public ResourceAllocatedController(IResourceAllocatedRepository repository)
        {
            _repository = repository;
        }

        // GET: api/ResourceAllocated
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceAllocated>>> GetResourceAllocations()
        {
            var resourceAllocations = await _repository.GetResourceAllocationsAsync();
            return Ok(resourceAllocations);
        }

        // GET: api/ResourceAllocated/5
        [HttpGet("{allocationId}")]
        public async Task<ActionResult<ResourceAllocated>> GetResourceAllocation(int allocationId)
        {
            var resourceAllocation = await _repository.GetResourceAllocationAsync(allocationId);

            if (resourceAllocation == null)
            {
                return NotFound();
            }

            return Ok(resourceAllocation);
        }

        // POST: api/ResourceAllocated
        [HttpPost]
        public async Task<ActionResult<ResourceAllocated>> PostResourceAllocation(ResourceAllocated resourceAllocated)
        {
            await _repository.AddResourceAllocationAsync(resourceAllocated);
            return CreatedAtAction("GetResourceAllocation", new { allocationId = resourceAllocated.AllocationId }, resourceAllocated);
        }

        // PUT: api/ResourceAllocated/5
        [HttpPut("{allocationId}")]
        public async Task<IActionResult> PutResourceAllocation(int allocationId, ResourceAllocated resourceAllocated)
        {
            if (allocationId != resourceAllocated.AllocationId)
            {
                return BadRequest();
            }

            await _repository.UpdateResourceAllocationAsync(resourceAllocated);
            return NoContent();
        }

        // DELETE: api/ResourceAllocated/5
        [HttpDelete("{allocationId}")]
        public async Task<IActionResult> DeleteResourceAllocation(int allocationId)
        {
            var resourceAllocation = await _repository.GetResourceAllocationAsync(allocationId);
            if (resourceAllocation == null)
            {
                return NotFound();
            }

            await _repository.DeleteResourceAllocationAsync(allocationId);
            return NoContent();
        }
    }
}
