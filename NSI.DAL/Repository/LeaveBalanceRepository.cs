using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using NSI.DAL.Entities;

namespace NSI.DAL.Repository
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly AvailabilityLeaveContext _AvailabilityLeaveContext;

        public LeaveBalanceRepository(AvailabilityLeaveContext availabilityLeaveContext)
        {
            _AvailabilityLeaveContext = availabilityLeaveContext;
        }

        public LeaveBalance Create(LeaveBalance leaveBalanceEntry)
        {
            _AvailabilityLeaveContext.LeaveBalances.Add(leaveBalanceEntry);
            _AvailabilityLeaveContext.SaveChanges();
            return leaveBalanceEntry;
        }

        public bool Delete(long id)
        {
            LeaveBalance existingLeaveBalance = _AvailabilityLeaveContext.LeaveBalances.FirstOrDefault(leaveBalanceEntry => leaveBalanceEntry.ID == id && !leaveBalanceEntry.IsDeleted);
            if (existingLeaveBalance != null) existingLeaveBalance.IsDeleted = true;
            else return false;
            _AvailabilityLeaveContext.SaveChanges();
            return true;
        }

        public ICollection<LeaveBalance> GetAll()
        {
            return _AvailabilityLeaveContext.LeaveBalances.Where(leaveBalanceEntry => !leaveBalanceEntry.IsDeleted).ToList();
        }

        public LeaveBalance GetById(long id)
        {
            return _AvailabilityLeaveContext.LeaveBalances.FirstOrDefault(leaveBalanceEntry => leaveBalanceEntry.ID == id && !leaveBalanceEntry.IsDeleted);
        }

        public LeaveBalance GetByResourceId(long id){
            return _AvailabilityLeaveContext.LeaveBalances
                        .FirstOrDefault(leaveBalanceEntry => leaveBalanceEntry.ResourceID == id && !leaveBalanceEntry.IsDeleted);
        }

        public LeaveBalance Update(LeaveBalance leaveBalanceEntry)
        {
            LeaveBalance currentValue = GetById(leaveBalanceEntry.ID);
            return DoUpdate(currentValue, leaveBalanceEntry);
        }

        private LeaveBalance DoUpdate(LeaveBalance oldValue, LeaveBalance newValue)
        {
            oldValue.MonthlyGain = newValue.MonthlyGain;
            oldValue.YearlyGain = newValue.YearlyGain;
            oldValue.AvailableDays = newValue.AvailableDays;
            oldValue.ResourceID = newValue.ResourceID;

            oldValue.IsDeleted = newValue.IsDeleted;
            oldValue.DateModified = newValue.DateModified;

            _AvailabilityLeaveContext.SaveChanges();
            return newValue;
        }
    }
}
