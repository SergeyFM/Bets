
namespace NotificationService.Models.Common
{
    public sealed class UpdateResponse
    {
        //public UpdateResponse() { }

        private UpdateResponse(int updateCount, bool isSuccess, string? errorMessage)
        {
            UpdateCount = updateCount;
            Result = new ResultResponse()
            { 
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage 
            };
        }

        public int UpdateCount { get; set; }
        public ResultResponse? Result { get; set; }

        public static UpdateResponse CreateSuccessResponse(int updateCount)
        {
            return new UpdateResponse(updateCount, true, null);
        }

        public static UpdateResponse CreateErrorResponse(string? errorMessage)
        {
            return new UpdateResponse(default, false, errorMessage);
        }
    }
}
