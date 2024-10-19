namespace Bets.Abstractions.Model
{
    public sealed class ResultResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
