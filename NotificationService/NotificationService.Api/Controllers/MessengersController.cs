using Microsoft.AspNetCore.Mvc;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using NotificationService.Models;
using NotificationService.Services;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Добавить мессенджер
        /// </summary>
        /// <param name="request">Содержит наименование мессенджера и кем добавляется запись</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        [HttpPost]
        [Route("create")]
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

        /// <summary>
        /// Получение мессенджера по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор мессенджера</param>
        /// <returns>MessengerResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMessengerAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetMessengerAsync(id);
                return Ok(result);
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
        /// <returns>List of MessengerResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListMessengersAsync()
        {
            try
            {
                var result = await _service.GetListMessengersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление мессенджера
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>MessengerResponse</returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateMessengerAsync([FromBody] UpdateMessengerRequest request)
        {
            try
            {
                var result = await _service.UpdateMessengerAsync(request);
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
        /// <returns>NoContent</returns>
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteMessengerAsync([FromBody] DeleteRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteMessengerAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("delete/list")]
        public async Task<IActionResult> DeleteListMessengersAsync([FromBody] DeleteListRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteListMessengersAsync(request);
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
