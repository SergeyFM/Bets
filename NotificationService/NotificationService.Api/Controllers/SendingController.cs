using Microsoft.AspNetCore.Mvc;
using NotificationService.Services;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendingController : ControllerBase
    {
        private readonly ILogger<SendingController> _logger;
        private readonly SendingService _sendingService;

        public SendingController(ILogger<SendingController> logger
            , SendingService sendingService)
        {
            _logger = logger;
            _sendingService = sendingService;
        }

        /// <summary>
        /// Тестовый метод для проверки работоспособности механизма отправки сообщений
        /// </summary>
        /// <returns>Nothing or exeption</returns>
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendMessagesAsync()
        {
            try
            {
                await _sendingService.SendMessagesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
