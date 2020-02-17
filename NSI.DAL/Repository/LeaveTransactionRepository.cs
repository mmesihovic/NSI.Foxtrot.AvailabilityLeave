using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using NSI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace NSI.DAL.Repository
{
    public class LeaveTransactionRepository : ILeaveTransactionRepository
    {
        private readonly AvailabilityLeaveContext _AvailabilityLeaveContext;

        public LeaveTransactionRepository(AvailabilityLeaveContext availabilityLeaveContext)
        {
            _AvailabilityLeaveContext = availabilityLeaveContext;
        }

        public LeaveTransaction Create(LeaveTransaction leaveTransactionEntry)
        {
            _AvailabilityLeaveContext.LeaveTransactions.Add(leaveTransactionEntry);
            _AvailabilityLeaveContext.SaveChanges();
            return leaveTransactionEntry;
        }
        
        public ICollection<LeaveTransaction> GetAllTransactionsByLeaveBalanceId(long id){
           return  _AvailabilityLeaveContext.LeaveTransactions
                        .Include(_leaveTransaction => _leaveTransaction.LeaveBalance)
                        .Where(leaveTransactionEntry => !leaveTransactionEntry.IsDeleted && leaveTransactionEntry.LeaveBalance.ID == id)
                        .ToList();
        }
        public bool Delete(long id)
        {
            LeaveTransaction existingLeaveTransaction = _AvailabilityLeaveContext.LeaveTransactions.FirstOrDefault(leaveTransactionEntry => leaveTransactionEntry.ID == id && !leaveTransactionEntry.IsDeleted);
            if (existingLeaveTransaction != null) existingLeaveTransaction.IsDeleted = true;
            else return false;
            _AvailabilityLeaveContext.SaveChanges();
            return true;
        }

        public ICollection<LeaveTransaction> GetAll()
        {
            return _AvailabilityLeaveContext.LeaveTransactions.Where(leaveTransactionEntry => !leaveTransactionEntry.IsDeleted).ToList();
        }

        public LeaveTransaction GetById(long id)
        {
            return _AvailabilityLeaveContext.LeaveTransactions.FirstOrDefault(leaveTransactionEntry => leaveTransactionEntry.ID == id && !leaveTransactionEntry.IsDeleted);
        }

        public LeaveTransaction Update(LeaveTransaction leaveTransactionEntry)
        {
            LeaveTransaction currentValue = GetById(leaveTransactionEntry.ID);
            return DoUpdate(currentValue, leaveTransactionEntry);
        }

        private LeaveTransaction DoUpdate(LeaveTransaction oldValue, LeaveTransaction newValue)
        {
            oldValue.Days = newValue.Days;
            oldValue.LeaveBalance = newValue.LeaveBalance;
            oldValue.Days = newValue.Days;
            oldValue.ApprovedByID = newValue.ApprovedByID;

            oldValue.IsDeleted = newValue.IsDeleted;
            oldValue.ModifiedBy = newValue.ModifiedBy;
            oldValue.DateModified = newValue.DateModified;

            _AvailabilityLeaveContext.SaveChanges();
            return newValue;
        }
    }
}
