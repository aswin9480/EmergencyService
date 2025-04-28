using AggregatorService.Services;
using Microsoft.AspNetCore.Mvc;
using AggregatorService.Models;

namespace AggregatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAggregatorController : ControllerBase
    {
        private readonly DataAggregatorService _dataAggregatorService;

        public DataAggregatorController(DataAggregatorService dataAggregatorService)
        {
            _dataAggregatorService = dataAggregatorService;
        }

        [HttpGet("combineddata")]
        public async Task<ActionResult<List<CombinedIncidentResource>>> GetCombinedData()
        {
            try
            {
                var result = await _dataAggregatorService.GetCombinedData();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }
}
