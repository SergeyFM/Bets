using Bets.Abstractions.Domain.Repositories.ModelRequests;
using NotificationService.Models;
using NotificationService.Models.Common;

namespace NotificationService.Client.Interfaces
{
    /// <summary>
    /// Содержит методы для взаимодействия с сервисом уведомлений
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Добавить сообщение в систему
        /// </summary>
        /// <param name="request">Данные сообщения</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Идентификатор добавленного сообщения</returns>
        Task<CreateResponse> AddMessageAsync(IncomingMessageRequest request, CancellationToken ct);

        /// <summary>
        /// Добавить несколько сообщений в систему
        /// </summary>
        /// <param name="request">Данные сообщений</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Идентификатор добавленного сообщения</returns>
        Task<AddRangeResponse> AddRangeMessagesAsync(List<IncomingMessageRequest> request, CancellationToken ct);

        /// <summary>
        /// Получение списка всех входящих сообщений
        /// </summary>
        /// <returns>List of IncomingMessageResponse</returns>
        Task<List<IncomingMessageResponse>> GetListMessagesAsync();

        /// <summary>
        /// Добавить источник сообщений
        /// </summary>
        /// <param name="request">Содержит идентификатор и краткое описание сервиса-источника сообщений</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        Task<CreateResponse> AddMessageSourceAsync(MessageSourcesRequest request, CancellationToken ct);

        /// <summary>
        /// Получение источника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор источника</param>
        /// <returns>MessageSourceResponse</returns>
        Task<MessageSourceResponse> GetMessageSourceAsync(Guid id);

        /// <summary>
        /// Получение списка всех источников
        /// </summary>
        /// <returns>List of MessageSourceResponse</returns>
        Task<List<MessageSourceResponse>> GetListMessageSourcesAsync();

        /// <summary>
        /// Обновление описания источника
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>MessageSourceResponse</returns>
        Task<MessageSourceResponse> UpdateMessageSourceAsync(MessageSourceUpdateRequest request);

        /// <summary>
        /// Удаление источника сообщений
        /// </summary>
        /// <param name="request">Идентификатор записи и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteMessageSourceAsync(DeleteRequest request);

        /// <summary>
        /// Удаление нескольких источников сообщений
        /// </summary>
        /// <param name="request">Список идентификаторов записей и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteListMessageSourcesAsync(DeleteListRequest request);


        /// <summary>
        /// Добавить мессенджер
        /// </summary>
        /// <param name="request">Содержит наименование мессенджера и кем добавляется запись</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        Task<CreateResponse> AddMessengerAsync(MessengerRequest request, CancellationToken ct);

        /// <summary>
        /// Получение мессенджера по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор мессенджера</param>
        /// <returns>MessengerResponse</returns>
        Task<MessengerResponse> GetMessengerAsync(Guid id);

        /// <summary>
        /// Получение списка всех мессенджеров
        /// </summary>
        /// <returns>List of MessengerResponse</returns>
        Task<List<MessengerResponse>> GetListMessengersAsync();

        /// <summary>
        /// Обновление мессенджера
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>MessengerResponse</returns>
        Task<MessengerResponse> UpdateMessengerAsync(MessengerUpdateRequest request);

        /// <summary>
        /// Удаление мессенджера
        /// </summary>
        /// <param name="request">Идентификатор записи и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteMessengerAsync(DeleteRequest request);

        /// <summary>
        /// Удаление нескольких мессенджеров
        /// </summary>
        /// <param name="request">Список идентификаторов записей и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteListMessengersAsync(DeleteListRequest request);

        /// <summary>
        /// Добавить игрока
        /// </summary>
        /// <param name="request">Содержит никнейм игрока</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        Task<CreateResponse> AddBettorAsync(BettorRequest request, CancellationToken ct);

        /// <summary>
        /// Получение игрока по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор игрока</param>
        /// <returns>BettorResponse</returns>
        Task<BettorResponse> GetBettorAsync(Guid id);

        /// <summary>
        /// Получение списка всех игроков
        /// </summary>
        /// <returns>List of BettorResponse</returns>
        Task<List<BettorResponse>> GetListBettorsAsync();

        /// <summary>
        /// Обновление никнейма игрока
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>BettorResponse</returns>
        Task<BettorResponse> UpdateBettorAsync(BettorUpdateRequest request);

        /// <summary>
        /// Удаление игрока
        /// </summary>
        /// <param name="request">Идентификатор записи и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteBettorAsync(DeleteRequest request);

        /// <summary>
        /// Удаление нескольких игроков
        /// </summary>
        /// <param name="request">Список идентификаторов записей и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteListBettorsAsync(DeleteListRequest request);

        /// <summary>
        /// Получение адреса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор игрока</param>
        /// <returns>BettorResponse</returns>
        Task<BettorAddressResponse> GetBettorAddressesAsync(Guid id);

        /// <summary>
        /// Получение списка всех адресов
        /// </summary>
        /// <returns>List of BettorAddressResponse</returns>
        Task<List<BettorAddressResponse>> GetListBettorAddressesAsync();

        /// <summary>
        /// Получение списка адресов конкретного игрока
        /// </summary>
        /// <returns>List of BettorAddressResponse</returns>
        Task<List<BettorAddressResponse>> GetListByBettorIdAsync(Guid bettorId);

        /// <summary>
        /// Получение адреса по умолчанию для конкретного игрока
        /// </summary>
        /// <returns>BettorAddressResponse с наименьшим Priority</returns>
        Task<BettorAddressResponse> GetDefaultByBettorIdAsync(Guid bettorId);
        /// <summary>
        /// Добавить адрес для связи с игроком
        /// </summary>
        /// <param name="request">Содержит информацию, необходимую для создания адреса для связи с игроком</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        Task<CreateResponse> AddBettorAddressesAsync(BettorAddressesRequest request, CancellationToken ct);

        /// <summary>
        /// Обновление непосредственно адреса
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>Кол-во обновленных записей</returns>
        Task<UpdateResponse> UpdateAddressAsync(AddressUpdateRequest request);

        /// <summary>
        /// Обновление записи справочника адресов
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>BettorAddressResponse</returns>
        Task<BettorAddressResponse> UpdateBettorAddressesAsync(BettorAddressUpdateRequest request);

        /// <summary>
        /// Обновление нескольких записей справочника адресов
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>BettorAddressResponse</returns>
        Task<List<BettorAddressResponse>> UpdateBettorAddressesAsync(IEnumerable<BettorAddressUpdateRequest> request);

        /// <summary>
        /// Назначение адреса дефолтным
        /// </summary>
        /// <param name="request">Идентификатор адреса и кто обновляет</param>
        /// <returns>BettorAddressResponse с наименьшим Priority</returns>
        Task<BettorAddressResponse> SetDefaultByBettorIdAsync(BettorAddressesSetDefaultRequest request);

        /// <summary>
        /// Удаление записи об адресе игрока
        /// </summary>
        /// <param name="request">Идентификатор записи и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteBettorAddressAsync(DeleteRequest request);

        /// <summary>
        /// Удаление нескольких записей об адресах игроков
        /// </summary>
        /// <param name="request">Список идентификаторов записей и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        Task<UpdateResponse> DeleteListBettorAddressesAsync(DeleteListRequest request);
    }
}
