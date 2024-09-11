using Microsoft.AspNetCore.Mvc;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BettorsController : ControllerBase
    {
        private readonly ILogger<BettorsController> _logger;
        private readonly BettorsService _service;

        public BettorsController(ILogger<BettorsController> logger
            , BettorsService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Добавить игрока
        /// </summary>
        /// <param name="request">Содержит никнейм игрока</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddBettorAsync([FromBody] BettorRequest request
            , CancellationToken ct)
        {
            try
            {
                var messengerId = await _service.AddBettorAsync(request, ct);
                return Ok(messengerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение игрока по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор игрока</param>
        /// <returns>BettorResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBettorAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetBettorAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех игроков
        /// </summary>
        /// <returns>List of BettorResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListBettorsAsync()
        {
            try
            {
                var result = await _service.GetListBettorsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление никнейма игрока
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>BettorResponse</returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateBettorAsync([FromBody] BettorUpdateRequest request)
        {
            try
            {
                var result = await _service.UpdateBettorAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="request">Идентификатор записи и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteBettorAsync([FromBody] DeleteRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteBettorAsync(request);
                return Ok(deletedCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаление нескольких записей
        /// </summary>
        /// <param name="request">Список идентификаторов записей и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        [HttpPost]
        [Route("delete/list")]
        public async Task<IActionResult> DeleteListBettorsAsync([FromBody] DeleteListRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteListBettorsAsync(request);
                return Ok(deletedCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
