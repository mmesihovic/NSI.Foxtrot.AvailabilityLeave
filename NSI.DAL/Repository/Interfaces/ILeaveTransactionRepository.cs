using NSI.DAL.Entities;
using System.Collections.Generic;

namespace NSI.DAL.Repository.Interfaces
{
    public interface ILeaveTransactionRepository : IGenericRepository<LeaveTransaction>
    {
        ICollection<LeaveTransaction> GetAllTransactionsByLeaveBalanceId(long id);
    }
}