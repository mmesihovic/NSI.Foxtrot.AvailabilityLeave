using NSI.DAL.Entities;
using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace NSI.DAL.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly AvailabilityLeaveContext _AvailabilityLeaveContext;

        public EventRepository(AvailabilityLeaveContext availabilityLeaveContext)
        {
            _AvailabilityLeaveContext = availabilityLeaveContext;
        }
        public Event Create(Event eventEntry)
        {
            _AvailabilityLeaveContext.Events.Add(eventEntry);
            _AvailabilityLeaveContext.SaveChanges();
            return eventEntry;
        }

        public bool Delete(long id)
        {
            Event existingEvent = _AvailabilityLeaveContext.Events.FirstOrDefault(eventEntry => eventEntry.ID == id && !eventEntry.IsDeleted);
            if (existingEvent!= null) existingEvent.IsDeleted = true;
            else return false;
            //Add proper logic for non existant entry in Database
            _AvailabilityLeaveContext.SaveChanges();
            return true;
        }

        public ICollection<Event> GetAll()
        {
            return _AvailabilityLeaveContext.Events
                        .Include(_event => _event.Type)
                        .Where(eventEntry => !eventEntry.IsDeleted)
                        .ToList();
        }

        public Event GetById(long id)
        {
            return _AvailabilityLeaveContext.Events
                        .Include(_event => _event.Type)
                        .FirstOrDefault(eventEntry => eventEntry.ID == id && !eventEntry.IsDeleted);
        }

        public Event Update(Event eventEntry)
        {
            Event currentValue = GetById(eventEntry.ID);
            return DoUpdate(currentValue, eventEntry);
        }

        private Event DoUpdate(Event oldValue, Event newValue)
        {
            oldValue.Name = newValue.Name;
            oldValue.Type = newValue.Type;
            oldValue.IsDeleted = newValue.IsDeleted;
            oldValue.ModifiedBy = newValue.ModifiedBy;
            oldValue.DateModified = newValue.DateModified;

            _AvailabilityLeaveContext.SaveChanges();
            return newValue;
        }
    }
}
