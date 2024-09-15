using NotificationService.Client.Interfaces;
using NotificationService.Models;
using Bets.Abstractions.Client;
using System.Text.Json;
using NotificationService.Models.Common;

namespace NotificationService.Client
{
    public class NotificationAdminClient : ClientBase, INotificationAdmin
    {
        private readonly HttpClient _client;

        private const string bettorAddressesEndpoint = "BettorAddresses/";

        public NotificationAdminClient(HttpClient httpClient, JsonSerializerOptions options) : base(httpClient, options) { }

        public async Task<CreateResponse> AddBettorAddressesAsync(BettorAddressesRequest request
            , CancellationToken ct)
            => await Post<BettorAddressesRequest, CreateResponse>(
                bettorAddressesEndpoint + "create",
                request
            );
    }
}
