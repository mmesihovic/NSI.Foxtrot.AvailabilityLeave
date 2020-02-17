using Moq;
using NSI.DAL.Repository.Interfaces;
using NSI.DAL.Entities;
using System.Collections.Generic;

namespace NSI.Test.Mocks.Repositories {

    public class MockLeaveTransactionRepository : Mock<ILeaveTransactionRepository> {
        public MockLeaveTransactionRepository MockGetAllTransactionsByLeaveBalanceId(long balanceID, long ID, long Days, long ApprovedByID ){
            List<LeaveTransaction> leaveTransactions = new List<LeaveTransaction>();
            leaveTransactions.Add(new LeaveTransaction {
                ID = ID,
                Days = Days,
                LeaveBalance = new LeaveBalance { ID = balanceID },
                ApprovedByID = ApprovedByID
            });
            
            Setup( x => x.GetAllTransactionsByLeaveBalanceId( It.Is<long>( _id => _id == balanceID)))
            .Returns(leaveTransactions);

            return this;
        }
        public MockLeaveTransactionRepository MockCreate(){
            Setup( x => x.Create(It.IsAny<LeaveTransaction>())).Returns<LeaveTransaction>(leaveTransaction => leaveTransaction);
            return this;
        }

    }

}