using NSI.DAL.Entities;
using System.Collections.Generic;
namespace NSI.BLL.Services.Interfaces
{
    public interface IEventService {
        ICollection<Event> GetAll();
        Event Create(Event _event, long? typeID, string typeName);
        Event GetById(long id);
    }

}