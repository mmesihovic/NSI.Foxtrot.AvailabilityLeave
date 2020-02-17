using NSI.BLL.Services.Interfaces;
using NSI.DAL.Entities;
using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;

namespace NSI.BLL.Services {
    /// <summary>
    /// Service used for managing Event Types in Database
    /// </summary>
    public class EventTypeService : IEventTypeService {

        private readonly IEventTypeRepository eventTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventTypeRepository"></param>
        public EventTypeService(IEventTypeRepository eventTypeRepository){
            this.eventTypeRepository = eventTypeRepository;
        }

        /// <summary>
        /// Returns all event types in Database
        /// </summary>
        /// <returns></returns>
        public ICollection<EventType> GetAll(){
            return eventTypeRepository.GetAll();
        }
    }
}