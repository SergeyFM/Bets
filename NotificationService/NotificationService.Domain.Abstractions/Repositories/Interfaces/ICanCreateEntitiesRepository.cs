using NotificationService.Domain.Abstractions.DTO;

namespace NotificationService.Domain.Abstractions.Repositories.Interfaces
{
    /// <summary>
    /// Содержит методы, которые реализуют репозитории с возможностью добавления новых сущностей
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface ICanCreateEntitiesRepository<T> : IRepository<T> 
        where T : IEntity
    {
        /// <summary>
        /// Добавляет новую сущность в хранилище
        /// </summary>
        /// <param name="entity">Новые данные</param>
        Task AddAsync(T entity);
    }
}
