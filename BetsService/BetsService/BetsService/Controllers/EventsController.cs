using BetsService.Models;
using BetsService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetsService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly EventsService _service;

        public EventsController(ILogger<EventsController> logger
            , EventsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddEventAsync([FromBody] EventRequest request
            , CancellationToken ct)
        {
            try
            {
                var bettorId = await _service.AddEventAsync(request, ct);
                return Ok(/*CreateResponse.CreateSuccessResponse(bettorId)*/);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(/*CreateResponse.CreateErrorResponse(ex.Message)*/);
            }
        }
    }
}
