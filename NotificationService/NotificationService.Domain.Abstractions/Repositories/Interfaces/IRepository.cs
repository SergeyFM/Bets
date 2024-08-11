using NotificationService.Domain.Abstractions.DTO;

namespace NotificationService.Domain.Abstractions.Repositories.Interfaces
{
    /// <summary>
    /// Содержит методы, которые реализуют все репозитрии 
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий</typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Получить все сущности типа T
        /// </summary>
        Task<List<T>> GetAllAsync();
    }
}
