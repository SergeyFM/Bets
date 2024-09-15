using Bets.Abstractions.Domain.Repositories.ModelRequests;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageSourcesController : ControllerBase
    {
        private readonly ILogger<MessageSourcesController> _logger;
        private readonly MessageSourcesService _service;

        public MessageSourcesController(ILogger<MessageSourcesController> logger
            , MessageSourcesService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Добавить источник сообщений
        /// </summary>
        /// <param name="request">Содержит идентификатор и краткое описание сервиса-источника сообщений</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddMessageSourceAsync([FromBody] MessageSourcesRequest request
            , CancellationToken ct)
        {
            try
            {
                var messengerId = await _service.AddMessageSourceAsync(request, ct);
                return Ok(messengerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение источника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор источника</param>
        /// <returns>MessageSourceResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMessageSourceAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetMessageSourceAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех источников
        /// </summary>
        /// <returns>List of MessageSourceResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListMessageSourcesAsync()
        {
            try
            {
                var result = await _service.GetListMessageSourcesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление описания источника
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>MessageSourceResponse</returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateMessageSourceAsync([FromBody] MessageSourceUpdateRequest request)
        {
            try
            {
                var result = await _service.UpdateMessageSourceAsync(request);
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
        public async Task<IActionResult> DeleteMessageSourceAsync([FromBody] DeleteRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteMessageSourceAsync(request);
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
        public async Task<IActionResult> DeleteListMessageSourcesAsync([FromBody] DeleteListRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteListMessageSourcesAsync(request);
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
