using NSI.DAL.Entities;

namespace NSI.DAL.Repository.Interfaces
{
    public interface IEventTypeRepository : IGenericRepository<EventType>
    {
        EventType GetByName(string name);
    }
}