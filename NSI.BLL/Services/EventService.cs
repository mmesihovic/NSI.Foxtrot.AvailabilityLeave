using NSI.BLL.Services.Interfaces;
using NSI.DAL.Entities;
using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;

namespace NSI.BLL.Services {
    /// <summary>
    /// Service used by Event Controller for API, manages data in DB via repositories and database context
    /// </summary>
    public class EventService : IEventService {

        private readonly IEventRepository eventRepository;
        private readonly IEventTypeRepository eventTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventRepository"></param>
        /// <param name="eventTypeRepository"></param>
        public EventService(IEventRepository eventRepository, IEventTypeRepository eventTypeRepository){
            this.eventRepository = eventRepository;
            this.eventTypeRepository = eventTypeRepository;
        }

        /// <summary>
        /// Get all events in Database
        /// </summary>
        /// <returns></returns>
        public ICollection<Event> GetAll(){
            return eventRepository.GetAll();
        }

        /// <summary>
        /// Get event by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Event GetById(long id){
            return eventRepository.GetById(id);
        }
        /// <summary>
        /// Create new Event in Database
        /// </summary>
        /// <param name="_event"></param>
        /// <param name="typeID"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public Event Create(Event _event, long? typeID, string typeName){
            EventType eventType = null;
            if(typeID.HasValue){
                eventType = eventTypeRepository.GetById(typeID.Value);
            } else {
                eventType = eventTypeRepository.GetByName(typeName);
            }
            if(eventType == null){
                throw new KeyNotFoundException("Either Type ID or Type Name was incorrect.");
            }
            _event.Type = eventType;
            return eventRepository.Create(_event);
        }
    }
}