using Bets.Abstractions.Model;
using BetsService.Models;
using BetsService.Services;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
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

        /// <summary>
        /// Добавить событие
        /// </summary>
        /// <param name="request">Содержит описание события, время его начала и время окончания приема ставок</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор нового события</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddEventAsync([FromBody] EventRequest request
            , CancellationToken ct)
        {
            try
            {
                var EventId = await _service.AddEventAsync(request, ct);
                return Ok(CreateResponse.CreateSuccessResponse(EventId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(CreateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Получение события по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        /// <returns>EventResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEventAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetEventAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех событий
        /// </summary>
        /// <returns>List of EventResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListEventsAsync()
        {
            try
            {
                var result = await _service.GetListEventsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление информации о событии
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>EventResponse</returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateEventAsync([FromBody] EventUpdateRequest request)
        {
            try
            {
                var result = await _service.UpdateEventAsync(request);
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
        public async Task<IActionResult> DeleteEventAsync([FromBody] DeleteRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteEventAsync(request);
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
        public async Task<IActionResult> DeleteListEventsAsync([FromBody] DeleteListRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteListEventsAsync(request);
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
