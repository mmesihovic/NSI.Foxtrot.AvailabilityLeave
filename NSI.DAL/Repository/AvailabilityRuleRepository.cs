using NSI.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using NSI.DAL.Entities;

namespace NSI.DAL.Repository
{
    public class AvailabilityRuleRepository: IAvailabilityRuleRepository {

        private readonly AvailabilityLeaveContext _AvailabilityLeaveContext;

        public AvailabilityRuleRepository(AvailabilityLeaveContext availabilityLeaveContext){
            _AvailabilityLeaveContext = availabilityLeaveContext;
        }
        public AvailabilityRule Create(AvailabilityRule availabilityRuleEntry){
            _AvailabilityLeaveContext.AvailabilityRules.Add(availabilityRuleEntry);
            _AvailabilityLeaveContext.SaveChanges();
            return availabilityRuleEntry;
        }
        public AvailabilityRule GetById(long id){
           return  _AvailabilityLeaveContext.AvailabilityRules.FirstOrDefault(availabilityRuleEntry => availabilityRuleEntry.ID == id && !availabilityRuleEntry.IsDeleted);
 
        }
        public ICollection<AvailabilityRule> GetAll(){
            return _AvailabilityLeaveContext.AvailabilityRules.Where(availabilityRuleEntry => !availabilityRuleEntry.IsDeleted).ToList();
        }
        public ICollection<AvailabilityRule> GetAllByResourceId(long id){
            return _AvailabilityLeaveContext.AvailabilityRules
                        .Where(availabilityRuleEntry => !availabilityRuleEntry.IsDeleted && availabilityRuleEntry.ResourceID == id)
                        .ToList();
        }
        public AvailabilityRule Update(AvailabilityRule availabilityRuleEntry){
            AvailabilityRule currentValue = GetById(availabilityRuleEntry.ID);
            return DoUpdate(currentValue, availabilityRuleEntry);
        }

        private AvailabilityRule DoUpdate(AvailabilityRule oldValue, AvailabilityRule newValue)
        {
            oldValue.FromTime = newValue.FromTime;
            oldValue.ToTime = newValue.ToTime;
            oldValue.WeekDay = newValue.WeekDay;
            oldValue.Available = newValue.Available;

            oldValue.IsDeleted = newValue.IsDeleted;
            oldValue.ModifiedBy = newValue.ModifiedBy;
            oldValue.DateModified = newValue.DateModified;

            _AvailabilityLeaveContext.SaveChanges();
            return newValue;
        }
        public bool Delete(long id) {
            AvailabilityRule existingAvailabilityRule = GetById(id);
            if(existingAvailabilityRule != null) existingAvailabilityRule.IsDeleted = true;
            else return false;
            _AvailabilityLeaveContext.SaveChanges();
            return true;
        }

    }
}