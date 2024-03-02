using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IControlRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ControlEntity entity);
    }
}

