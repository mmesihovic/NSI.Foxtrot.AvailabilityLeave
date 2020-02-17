using NSI.DAL.Entities;
using System.Collections.Generic;
using NSI.DataContracts.Models.Requests;

namespace NSI.BLL.Services.Interfaces
{
    public interface ILeaveService {

        ICollection<LeaveBalance> GetAll();
        ICollection<LeaveTransaction> GetAllTransactionsByLeaveBalanceId(long id);
        LeaveBalance GetById(long id);
        LeaveBalance GetByResourceId(long id);
        LeaveBalance CreateOrUpdate(LeaveBalanceRequest leaveBalance);
        LeaveBalance Change(LeaveTransactionRequest leaveTransactionRequest);
    }
}