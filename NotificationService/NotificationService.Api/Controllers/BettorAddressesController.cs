using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BettorAddressesController : ControllerBase
    {
        private readonly ILogger<BettorAddressesController> _logger;
        private readonly BettorAddressesService _service;

        public BettorAddressesController(ILogger<BettorAddressesController> logger
            , BettorAddressesService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Добавить адрес для связи с игроком
        /// </summary>
        /// <param name="request">Содержит информацию, необходимую для создания адреса для связи с игроком</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddBettorAddressesAsync([FromBody] BettorAddressesRequest request
            , CancellationToken ct)
        {
            try
            {
                var messengerId = await _service.AddBettorAddressesAsync(request, ct);
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
