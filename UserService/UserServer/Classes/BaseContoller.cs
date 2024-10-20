using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserServer.WebHost.Classes
{
    /// <summary>
    /// Базовый класс для контруктора с логированием ошибок
    /// </summary>
    public class BaseContoller : ControllerBase
    {
        /// <summary>
        /// Логгер
        /// </summary>
        protected ILogger<BaseContoller>? logger;

        protected BaseContoller(ILogger<BaseContoller>? logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Информационные логи по юзеру
        /// </summary>
        /// <param name="message">Сообщение для логирования</param>
        /// <param name="userName">Имя юзера</param>
        protected void LogInformationByUser(string message, string? userName = null)
        {
            if (userName == null) 
            {
                userName = User.FindFirstValue(ClaimTypes.Email);
            }

            message = "Пользователь: {0} - " + message;

            logger?.LogInformation(message, userName);
        }

        protected ObjectResult IternalServerError(Exception ex, object? message = null)
        {
            logger?.LogError(ex.Message);

            var returnMessage = message ?? new { Message = ex.Message };

            return StatusCode(500, returnMessage);
        }

        protected BadRequestObjectResult BadRequestHttp(string message)
        {
            logger?.LogError(message);

            return BadRequest(message);
        }
    }
}
