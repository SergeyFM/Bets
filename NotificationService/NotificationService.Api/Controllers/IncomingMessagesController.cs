using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomingMessagesController : ControllerBase
    {
        private readonly ILogger<IncomingMessagesController> _logger;
        private readonly IncomingMessagesService _messagesService;

        public IncomingMessagesController(ILogger<IncomingMessagesController> logger
            , IncomingMessagesService messagesService)
        {
            _logger = logger;
            _messagesService = messagesService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessageAsync([FromBody] IncomingMessageRequest request
            , CancellationToken ct)
        {
            try
            {
                var messageId = await _messagesService.AddMessageAsync(request, ct);
                return Ok(messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
