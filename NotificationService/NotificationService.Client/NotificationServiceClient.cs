using NotificationService.Client.Interfaces;
using NotificationService.Models;
using Bets.Abstractions.Client;
using System.Text.Json;
using NotificationService.Models.Common;
using Bets.Abstractions.Domain.Repositories.ModelRequests;

namespace NotificationService.Client
{
    public class NotificationServiceClient : ClientBase, INotificationService
    {
        private const string bettorAddressesEndpoint = "BettorAddresses/";
        private const string bettorsEndpoint = "Bettors/";
        private const string messengersEndpoint = "Messengers/";
        private const string messageSourcesEndpoint = "MessageSources/";
        private const string incomingMessagesEndpoint = " IncomingMessages/";

        public NotificationServiceClient(HttpClient httpClient, JsonSerializerOptions options) : base(httpClient, options) { }

        public async Task<CreateResponse> AddMessageAsync(IncomingMessageRequest request
            , CancellationToken ct)
            => await Post<IncomingMessageRequest, CreateResponse>(
                incomingMessagesEndpoint + "create",
                request
            );

        public async Task<AddRangeResponse> AddRangeMessagesAsync(List<IncomingMessageRequest> request
            , CancellationToken ct)
            => await Post<List<IncomingMessageRequest>, AddRangeResponse>(
                incomingMessagesEndpoint + "createRange",
                request
            );

        public async Task<List<IncomingMessageResponse>> GetListMessagesAsync()
            => await Get<List<IncomingMessageResponse>>(
                incomingMessagesEndpoint
            );

        public async Task<CreateResponse> AddMessageSourceAsync(MessageSourcesRequest request
            , CancellationToken ct)
            => await Post<MessageSourcesRequest, CreateResponse>(
                messageSourcesEndpoint + "create",
                request
            );

        public async Task<MessageSourceResponse> GetMessageSourceAsync(Guid id)
            => await Get<MessageSourceResponse>(
                messageSourcesEndpoint + "{id}"
            );

        public async Task<List<MessageSourceResponse>> GetListMessageSourcesAsync()
            => await Get<List<MessageSourceResponse>>(
                messageSourcesEndpoint
            );

        public async Task<MessageSourceResponse> UpdateMessageSourceAsync(MessageSourceUpdateRequest request)
            => await Post<MessageSourceUpdateRequest, MessageSourceResponse>(
                messageSourcesEndpoint + "update",
                request
            );

        public async Task<UpdateResponse> DeleteMessageSourceAsync(DeleteRequest request)
            => await Post<DeleteRequest, UpdateResponse>(
                messageSourcesEndpoint + "delete",
                request
            );

        public async Task<UpdateResponse> DeleteListMessageSourcesAsync(DeleteListRequest request)
            => await Post<DeleteListRequest, UpdateResponse>(
                messageSourcesEndpoint + "delete/list",
                request
            );

        public async Task<CreateResponse> AddMessengerAsync(MessengerRequest request
            , CancellationToken ct)
            => await Post<MessengerRequest, CreateResponse>(
                messengersEndpoint + "create",
                request
            );

        public async Task<MessengerResponse> GetMessengerAsync(Guid id)
            => await Get<MessengerResponse>(
                messengersEndpoint + "{id}"
            );

        public async Task<List<MessengerResponse>> GetListMessengersAsync()
            => await Get<List<MessengerResponse>>(
                messengersEndpoint
            );

        public async Task<MessengerResponse> UpdateMessengerAsync(MessengerUpdateRequest request)
            => await Post<MessengerUpdateRequest, MessengerResponse>(
                messengersEndpoint + "update",
                request
            );

        public async Task<UpdateResponse> DeleteMessengerAsync(DeleteRequest request)
            => await Post<DeleteRequest, UpdateResponse>(
                messengersEndpoint + "delete",
                request
            );

        public async Task<UpdateResponse> DeleteListMessengersAsync(DeleteListRequest request)
            => await Post<DeleteListRequest, UpdateResponse>(
                messengersEndpoint + "delete/list",
                request
            );

        public async Task<CreateResponse> AddBettorAsync(BettorRequest request
            , CancellationToken ct)
            => await Post<BettorRequest, CreateResponse>(
                bettorsEndpoint + "create",
                request
            );

        public async Task<BettorResponse> GetBettorAsync(Guid id)
            => await Get<BettorResponse>(
                bettorsEndpoint + "{id}"
            );

        public async Task<List<BettorResponse>> GetListBettorsAsync()
            => await Get<List<BettorResponse>>(
                bettorsEndpoint
            );

        public async Task<BettorResponse> UpdateBettorAsync(BettorUpdateRequest request)
            => await Post<BettorUpdateRequest, BettorResponse>(
                bettorsEndpoint + "update",
                request
            );

        public async Task<UpdateResponse> DeleteBettorAsync(DeleteRequest request)
            => await Post<DeleteRequest, UpdateResponse>(
                bettorsEndpoint + "delete",
                request
            );

        public async Task<UpdateResponse> DeleteListBettorsAsync(DeleteListRequest request)
            => await Post<DeleteListRequest, UpdateResponse>(
                bettorsEndpoint + "delete/list",
                request
            );

        public async Task<CreateResponse> AddBettorAddressesAsync(BettorAddressesRequest request
            , CancellationToken ct)
            => await Post<BettorAddressesRequest, CreateResponse>(
                bettorAddressesEndpoint + "create",
                request
            );

        public async Task<BettorAddressResponse> GetBettorAddressesAsync(Guid id)
            => await Get<BettorAddressResponse>(
                bettorAddressesEndpoint + "{id}"
            );

        public async Task<List<BettorAddressResponse>> GetListBettorAddressesAsync()
            => await Get<List<BettorAddressResponse>>(
                bettorAddressesEndpoint
            );

        public async Task<List<BettorAddressResponse>> GetListByBettorIdAsync(Guid bettorId)
            => await Get<List<BettorAddressResponse>>(
                bettorAddressesEndpoint + "getByBettorId/{bettorId}"
            );

        public async Task<BettorAddressResponse> GetDefaultByBettorIdAsync(Guid bettorId)
            => await Get<BettorAddressResponse>(
                bettorAddressesEndpoint + "getDefault/{bettorId}"
            );

        public async Task<UpdateResponse> UpdateAddressAsync(AddressUpdateRequest request)
            => await Post<AddressUpdateRequest, UpdateResponse>(
                bettorAddressesEndpoint + "updateAddress",
                request
            );

        public async Task<BettorAddressResponse> UpdateBettorAddressesAsync(BettorAddressUpdateRequest request)
            => await Post<BettorAddressUpdateRequest, BettorAddressResponse>(
                bettorAddressesEndpoint + "update",
                request
            );

        public async Task<List<BettorAddressResponse>> UpdateBettorAddressesAsync(IEnumerable<BettorAddressUpdateRequest> request)
            => await Post<IEnumerable<BettorAddressUpdateRequest>, List<BettorAddressResponse>>(
                bettorAddressesEndpoint + "updateList",
                request
            );

        public async Task<BettorAddressResponse> SetDefaultByBettorIdAsync(BettorAddressesSetDefaultRequest request)
            => await Post<BettorAddressesSetDefaultRequest, BettorAddressResponse>(
                bettorAddressesEndpoint + "setDefault",
                request
            );

        public async Task<UpdateResponse> DeleteBettorAddressAsync(DeleteRequest request)
            => await Post<DeleteRequest, UpdateResponse>(
                bettorAddressesEndpoint + "delete",
                request
            );

        public async Task<UpdateResponse> DeleteListBettorAddressesAsync(DeleteListRequest request)
            => await Post<DeleteListRequest, UpdateResponse>(
                bettorAddressesEndpoint + "delete/list",
                request
            );
    }
}
