
namespace Bets.Abstractions.Model
{
    public sealed class AddRangeResponse
    {
        private AddRangeResponse(IEnumerable<Guid> ids, bool isSuccess, string? errorMessage)
        {
            Ids = ids;
            Result = new ResultResponse()
            {
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage
            };
        }

        public IEnumerable<Guid> Ids { get; set; }
        public ResultResponse? Result { get; set; }

        public static AddRangeResponse CreateSuccessResponse(IEnumerable<Guid> ids)
        {
            return new AddRangeResponse(ids, true, null);
        }

        public static AddRangeResponse CreateErrorResponse(string? errorMessage)
        {
            return new AddRangeResponse([], false, errorMessage);
        }
    }
}
