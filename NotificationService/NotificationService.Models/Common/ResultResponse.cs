
namespace NotificationService.Models.Common
{
    public sealed class ResultResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
