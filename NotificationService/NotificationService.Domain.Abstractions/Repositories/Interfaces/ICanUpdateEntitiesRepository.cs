using NotificationService.Domain.Abstractions.DTO;

namespace NotificationService.Domain.Abstractions.Repositories.Interfaces
{
    /// <summary>
    /// Содержит методы, которые реализуют репозитории с возможностью изменения сущностей
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface ICanUpdateEntitiesRepository<T> : ICanCreateEntitiesRepository<T>, IRepository<T> 
        where T : IEntity
    {
        /// <summary>
        /// Изменяет в хранилище информацию о сущности
        /// </summary>
        /// <param name="entity">Изменяемые данные</param>
        Task UpdateAsync(T entity);
    }
}
