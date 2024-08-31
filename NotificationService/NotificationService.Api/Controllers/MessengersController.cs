using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessengersController : ControllerBase
    {
        private readonly ILogger<MessengersController> _logger;
        private readonly MessengersService _service;

        public MessengersController(ILogger<MessengersController> logger
            , MessengersService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessengerAsync([FromBody] MessengerRequest request
            , CancellationToken ct)
        {
            try
            {
                var messengerId = await _service.AddMessengerAsync(request, ct);
                return Ok(messengerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
