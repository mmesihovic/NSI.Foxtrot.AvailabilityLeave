using NSI.DAL.Entities;
using System.Collections.Generic;
namespace NSI.BLL.Services.Interfaces
{
    public interface IEventTypeService {
        ICollection<EventType> GetAll();
    }
}