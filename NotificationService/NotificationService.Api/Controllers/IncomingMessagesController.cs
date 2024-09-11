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

        /// <summary>
        /// Добавить сообщение в систему
        /// </summary>
        /// <param name="request">Данные сообщения</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Идентификатор добавленного сообщения</returns>
        [HttpPost]
        [Route("create")]
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

        /// <summary>
        /// Получение списка всех входящих сообщений
        /// </summary>
        /// <returns>List of IncomingMessageResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListMessagesAsync()
        {
            try
            {
                var result = await _messagesService.GetListMessagesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
