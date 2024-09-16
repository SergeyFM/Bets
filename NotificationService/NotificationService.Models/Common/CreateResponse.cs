
namespace NotificationService.Models.Common
{
    public sealed class CreateResponse
    {
        //public CreateResponse() { }

        private CreateResponse(Guid id, bool isSuccess, string? errorMessage)
        {
            Id = id;
            Result = new ResultResponse()
            {
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage
            };
        }

        public Guid Id { get; set; }
        public ResultResponse? Result { get; set; }

        public static CreateResponse CreateSuccessResponse(Guid id)
        {
            return new CreateResponse(id, true, null);
        }

        public static CreateResponse CreateErrorResponse(string? errorMessage)
        {
            return new CreateResponse(default, false, errorMessage);
        }
    }
}
