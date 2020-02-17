using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using NSI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace NSI.DAL.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AvailabilityLeaveContext _AvailabilityLeaveContext;
        public ScheduleRepository(AvailabilityLeaveContext availabilityLeaveContext)
        {
            _AvailabilityLeaveContext = availabilityLeaveContext;
        }
        public ResourceSchedule Create(ResourceSchedule resourceScheduleEntry)
        {
            _AvailabilityLeaveContext.ResourceSchedules.Add(resourceScheduleEntry);
            _AvailabilityLeaveContext.SaveChanges();
            return resourceScheduleEntry;
        }
        public bool Delete(long id)
        {
            ResourceSchedule existingResourceSchedule = _AvailabilityLeaveContext.ResourceSchedules.FirstOrDefault(resourceScheduleEntry => resourceScheduleEntry.ID == id);
            if (existingResourceSchedule != null) existingResourceSchedule.IsDeleted = true;
            else return false;
            _AvailabilityLeaveContext.SaveChanges();
            return true;
        }
        public ICollection<ResourceSchedule> GetAll()
        {
            return _AvailabilityLeaveContext.ResourceSchedules
                    .Include(_schedule => _schedule.Event)
                        .ThenInclude(_event => _event.Type)
                    .Where(resourceScheduleEntry => !resourceScheduleEntry.IsDeleted)
                    .ToList();
        }
        public ResourceSchedule GetById(long id)
        {
            return _AvailabilityLeaveContext.ResourceSchedules
                    .Include(_schedule => _schedule.Event)
                        .ThenInclude(_event => _event.Type)
                    .FirstOrDefault(resourceScheduleEntry => resourceScheduleEntry.ID == id && !resourceScheduleEntry.IsDeleted);
        }
        public ICollection<ResourceSchedule> GetByIdAndTimeRange(long id, DateTime _from, DateTime _until)
        {
            return _AvailabilityLeaveContext.ResourceSchedules
                .Include(_schedule => _schedule.Event)
                    .ThenInclude(_event => _event.Type)
                .Where(resourceScheduleEntry => !resourceScheduleEntry.IsDeleted && resourceScheduleEntry.ResourceID == id && 
                        ((resourceScheduleEntry.FromDate <= _from && resourceScheduleEntry.UntilDate >= _from) 
                        || (resourceScheduleEntry.FromDate <= _until && resourceScheduleEntry.UntilDate >= _until))).ToList();
        }
        public ICollection<ResourceSchedule> GetAllByResourceID(long id)
        {
            return _AvailabilityLeaveContext.ResourceSchedules
                .Include(_schedule => _schedule.Event)
                    .ThenInclude(_event => _event.Type)
                .Where(resourceScheduleEntry => resourceScheduleEntry.ResourceID == id && !resourceScheduleEntry.IsDeleted).ToList();
        }
        public ResourceSchedule Update(ResourceSchedule resourceScheduleEntry)
        {
            ResourceSchedule currentValue = GetById(resourceScheduleEntry.ID);
            return DoUpdate(currentValue, resourceScheduleEntry);
        }
        private ResourceSchedule DoUpdate(ResourceSchedule oldValue, ResourceSchedule newValue)
        {
            oldValue.ApprovedByID = newValue.ApprovedByID;
            oldValue.FromDate = newValue.FromDate;
            oldValue.UntilDate = newValue.UntilDate;
            oldValue.Approved = newValue.Approved;
            oldValue.ApprovedByID = newValue.ApprovedByID;
            oldValue.RequestByID = newValue.RequestByID;
            oldValue.ResourceID = newValue.ResourceID;
            oldValue.Event = newValue.Event;

            oldValue.IsDeleted = newValue.IsDeleted;
            oldValue.ModifiedBy = newValue.ModifiedBy;
            oldValue.DateModified = newValue.DateModified;

            _AvailabilityLeaveContext.SaveChanges();
            return newValue;
        }
    }
}
