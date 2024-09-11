using Bets.Abstractions.Domain.DTO;

namespace Bets.Abstractions.Domain.Repositories.Interfaces
{
    /// <summary>
    /// Содержит методы, которые реализуют репозитории сущностей с идентификаторами Guid
    /// </summary>
    /// <typeparam name="T">Тип сущности с идентификаторами Guid</typeparam>
    public interface IIdentifiableEntitiesRepository<T> : IRepository<T> 
        where T : IdentifiableEntity
    {
        /// <summary>
        /// Получение списка сущностей по идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы сущностей</param>
        Task<List<T>> GetListByIdsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        Task<T?> GetByIdAsync(Guid id);
    }
}
