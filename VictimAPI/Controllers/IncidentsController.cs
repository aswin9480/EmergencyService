using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictimAPI.Models;
using VictimAPI.Repositories;

namespace VictimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentRepository _repository;

        public IncidentsController(IIncidentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Incidents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentTbl>>> GetIncidents()
        {
            var incidents = await _repository.GetIncidentsAsync();
            return Ok(incidents);
        }

        // GET: api/Incidents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentTbl>> GetIncident(int id)
        {
            var incident = await _repository.GetIncidentAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            return incident;
        }

        // PUT: api/Incidents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncident(int id, IncidentTbl incident)
        {
            if (id != incident.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateIncidentAsync(incident);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.IncidentExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(incident);
        }

        // POST: api/Incidents
        [HttpPost]
        public async Task<ActionResult<IncidentTbl>> PostIncident(IncidentTbl incident)
        {
            await _repository.AddIncidentAsync(incident);
            return CreatedAtAction(nameof(GetIncident), new { id = incident.Id }, incident);
        }

        // DELETE: api/Incidents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(int id)
        {
            var incidentExists = await _repository.IncidentExistsAsync(id);
            if (!incidentExists)
            {
                return NotFound();
            }

            await _repository.DeleteIncidentAsync(id);
            return NoContent();
        }
    }
}
