using NotificationService.Domain.Abstractions.DTO;
using NotificationService.Domain.Abstractions.Repositories.ModelRequests;

namespace NotificationService.Domain.Abstractions.Repositories.Interfaces
{
    /// <summary>
    /// Содержит методы, которые реализуют репозитории с возможностью удаления сущностей по идентификатору
    /// </summary>
    /// <typeparam name="T">Тип сущности с идентификаторами Guid</typeparam>
    public interface ICanDeleteEntitiesRepository<T> : IIdentifiableEntitiesRepository<T>, IRepository<T> 
        where T : IdentifiableEntity
    {
        /// <summary>
        /// Помечает сущность для удаления
        /// </summary>
        /// <param name="request">Идентификаторы удаляемых объектов и кем удаляются</param>
        /// <returns>Количество удаленных</returns>
        Task<int> DeleteAsync(DeleteRequest request);

        /// <summary>
        /// Помечает несколько сущностей для удаления
        /// </summary>
        /// <param name="request">Идентификаторы удаляемых объектов и кем удаляются</param>
        /// <returns>Количество удаленных</returns>
        Task<int> DeleteRangeAsync(DeleteListRequest request);
    }
}
