
namespace NotificationService.Models.Common
{
    public sealed class CreateResponse
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
