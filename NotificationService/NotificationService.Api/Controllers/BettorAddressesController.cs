using Bets.Abstractions.Domain.Repositories.ModelRequests;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Models.Common;
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
                var addressId = await _service.AddBettorAddressesAsync(request, ct);
                return Ok(CreateResponse.CreateSuccessResponse(addressId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(CreateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Получение адреса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор игрока</param>
        /// <returns>BettorResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBettorAddressesAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetBettorAddressesAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех адресов
        /// </summary>
        /// <returns>List of BettorAddressResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListBettorAddressesAsync()
        {
            try
            {
                var result = await _service.GetListBettorAddressesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка адресов конкретного игрока
        /// </summary>
        /// <returns>List of BettorAddressResponse</returns>
        [HttpGet]
        [Route("getByBettorId/{bettorId}")]
        public async Task<IActionResult> GetListByBettorIdAsync([FromRoute] Guid bettorId)
        {
            try
            {
                var result = await _service.GetListByBettorIdAsync(bettorId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение адреса по умолчанию для конкретного игрока
        /// </summary>
        /// <returns>BettorAddressResponse с наименьшим Priority</returns>
        [HttpGet]
        [Route("getDefault/{bettorId}")]
        public async Task<IActionResult> GetDefaultByBettorIdAsync([FromRoute] Guid bettorId)
        {
            try
            {
                var result = await _service.GetDefaultByBettorIdAsync(bettorId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление непосредственно адреса
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>Кол-во обновленных записей</returns>
        [HttpPost]
        [Route("updateAddress")]
        public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressUpdateRequest request)
        {
            try
            {
                var updatedCount = await _service.UpdateAddressAsync(request);
                return Ok(UpdateResponse.CreateSuccessResponse(updatedCount));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(UpdateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Обновление записи справочника адресов
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>BettorAddressResponse</returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateBettorAddressesAsync([FromBody] BettorAddressUpdateRequest request)
        {
            try
            {
                var result = await _service.UpdateBettorAddressesAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление нескольких записей справочника адресов
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>BettorAddressResponse</returns>
        [HttpPost]
        [Route("updateList")]
        public async Task<IActionResult> UpdateBettorAddressesAsync([FromBody] IEnumerable<BettorAddressUpdateRequest> request)
        {
            try
            {
                var result = await _service.UpdateBettorAddressesAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Назначение адреса дефолтным
        /// </summary>
        /// <param name="request">Идентификатор адреса и кто обновляет</param>
        /// <returns>BettorAddressResponse с наименьшим Priority</returns>
        [HttpPost]
        [Route("setDefault")]
        public async Task<IActionResult> SetDefaultByBettorIdAsync([FromBody] BettorAddressesSetDefaultRequest request)
        {
            try
            {
                var result = await _service.SetDefaultByBettorIdAsync(request);
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
        public async Task<IActionResult> DeleteBettorAddressAsync([FromBody] DeleteRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteBettorAddressAsync(request);
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
        public async Task<IActionResult> DeleteListBettorAddressesAsync([FromBody] DeleteListRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteListBettorAddressesAsync(request);
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
