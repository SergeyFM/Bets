using Bets.Abstractions.Model;
using BetsService.Models;
using BetsService.Services;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using Microsoft.AspNetCore.Mvc;

namespace BetsService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventOutcomesController : ControllerBase
    {
        private readonly ILogger<EventOutcomesController> _logger;
        private readonly EventOutcomesService _service;

        public EventOutcomesController(ILogger<EventOutcomesController> logger
            , EventOutcomesService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Добавить исход события
        /// </summary>
        /// <param name="request">Содержит описание исхода события</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор нового исхода</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddEventOutcomeAsync([FromBody] EventOutcomeRequest request
            , CancellationToken ct)
        {
            try
            {
                var EventId = await _service.AddEventOutcomeAsync(request, ct);
                return Ok(CreateResponse.CreateSuccessResponse(EventId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(CreateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Получение исхода по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор исхода</param>
        /// <returns>EventOutcomeResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEventAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetEventOutcomeAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех исходов
        /// </summary>
        /// <returns>List of EventOutcomeResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListEventOutcomesAsync()
        {
            try
            {
                var result = await _service.GetListEventOutcomesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление информации об исходе
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>EventOutcomeResponse</returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateEventAsync([FromBody] EventOutcomeUpdateRequest request)
        {
            try
            {
                var result = await _service.UpdateEventOutcomeAsync(request);
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
        public async Task<IActionResult> DeleteEventOutcomeAsync([FromBody] DeleteRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteEventOutcomeAsync(request);
                return Ok(UpdateResponse.CreateSuccessResponse(deletedCount));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(UpdateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Удаление нескольких записей
        /// </summary>
        /// <param name="request">Список идентификаторов записей и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        [HttpPost]
        [Route("delete/list")]
        public async Task<IActionResult> DeleteListEventOutcomesAsync([FromBody] DeleteListRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteListEventOutcomesAsync(request);
                return Ok(UpdateResponse.CreateSuccessResponse(deletedCount));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(UpdateResponse.CreateErrorResponse(ex.Message));
            }
        }
    }
}
