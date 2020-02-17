using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using NSI.DAL.Entities;

namespace NSI.DAL.Repository
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly AvailabilityLeaveContext _AvailabilityLeaveContext;

        public EventTypeRepository(AvailabilityLeaveContext availabilityLeaveContext)
        {
            _AvailabilityLeaveContext = availabilityLeaveContext;
        }

        public EventType Create(EventType eventTypeEntry)
        {
            _AvailabilityLeaveContext.EventTypes.Add(eventTypeEntry);
            _AvailabilityLeaveContext.SaveChanges();
            return eventTypeEntry;
        }

        public bool Delete(long id)
        {
            EventType existingEventType = _AvailabilityLeaveContext.EventTypes.FirstOrDefault(eventTypeEntry => eventTypeEntry.ID == id && !eventTypeEntry.IsDeleted);
            if (existingEventType != null) existingEventType.IsDeleted = true;
            else return false;
            //Add proper logic for non existant entry in Database
            _AvailabilityLeaveContext.SaveChanges();
            return true;
        }

        public ICollection<EventType> GetAll()
        {
            return _AvailabilityLeaveContext.EventTypes.Where(eventTypeEntry => !eventTypeEntry.IsDeleted).ToList();
        }

        public EventType GetById(long id)
        {
            return _AvailabilityLeaveContext.EventTypes.FirstOrDefault(eventTypeEntry => eventTypeEntry.ID == id && !eventTypeEntry.IsDeleted);
        }
        public EventType GetByName(string name){
            return _AvailabilityLeaveContext.EventTypes.FirstOrDefault(eventTypeEntry => eventTypeEntry.Name == name && !eventTypeEntry.IsDeleted);
        }
        public EventType Update(EventType eventTypeEntry)
        {
            EventType currentValue = GetById(eventTypeEntry.ID);
            return DoUpdate(currentValue, eventTypeEntry);
        }

        private EventType DoUpdate(EventType oldValue, EventType newValue)
        {
            oldValue.Name = newValue.Name;
            oldValue.IsDeleted = newValue.IsDeleted;
            oldValue.DateModified = newValue.DateModified;

            _AvailabilityLeaveContext.SaveChanges();
            return newValue;
        }
    }
}
