using NSI.DAL.Entities;

namespace NSI.DAL.Repository.Interfaces
{
    public interface ILeaveBalanceRepository : IGenericRepository<LeaveBalance>
    {
        LeaveBalance GetByResourceId(long id);
    }
}