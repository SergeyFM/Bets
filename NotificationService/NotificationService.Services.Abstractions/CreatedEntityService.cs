using NotificationService.DataAccess.Abstractions.EF.Repositories;
using NotificationService.Domain.Abstractions.DTO;

namespace NotificationService.Services.Abstractions
{
    public class CreatedEntityService
    {
        private readonly CreatedEntityRepository<CreatedEntity> _repository;

        public CreatedEntityService(CreatedEntityRepository<CreatedEntity> repository)
        {
            _repository = repository;
        }
    }
}
