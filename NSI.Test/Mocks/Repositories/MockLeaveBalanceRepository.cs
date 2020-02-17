using Moq;
using NSI.DAL.Repository.Interfaces;
using NSI.DAL.Entities;
using System.Collections.Generic;

namespace NSI.Test.Mocks.Repositories {

    public class MockLeaveBalanceRepository : Mock<ILeaveBalanceRepository> {

        public MockLeaveBalanceRepository MockGetAll(long ID, long ResourceID, long AvailableDays, long MonthlyGain, long YearlyGain){
            List<LeaveBalance> leaveBalances = new List<LeaveBalance>();
            leaveBalances.Add(
                new LeaveBalance {
                    ID = ID,
                    ResourceID = ResourceID,
                    AvailableDays = AvailableDays,
                    MonthlyGain = MonthlyGain,
                    YearlyGain = YearlyGain
                }
            );

            Setup( x => x.GetAll() ).Returns(leaveBalances);
            return this;
        }

        public MockLeaveBalanceRepository MockGetById(long ID, long ResourceID, long AvailableDays, long MonthlyGain, long YearlyGain){
            Setup( x => x.GetById( It.Is<long>( id => id == ID)))
                .Returns(
                    new LeaveBalance {
                        ID = ID,
                        ResourceID = ResourceID,
                        AvailableDays = AvailableDays,
                        MonthlyGain = MonthlyGain,
                        YearlyGain = YearlyGain
                    }
                );
            return this;
        }
        public MockLeaveBalanceRepository MockGetByResourceId(long ID, long ResourceID, long AvailableDays, long MonthlyGain, long YearlyGain){
            Setup( x => x.GetByResourceId( It.Is<long>( id => id == ResourceID)))
                .Returns(
                    new LeaveBalance {
                        ID = ID,
                        ResourceID = ResourceID,
                        AvailableDays = AvailableDays,
                        MonthlyGain = MonthlyGain,
                        YearlyGain = YearlyGain
                    }
                );
            return this;
        }
        public MockLeaveBalanceRepository MockGetByResourceIdNull(){
            Setup( x => x.GetByResourceId(It.IsAny<long>())).Returns<LeaveBalance>( null );
            return this;
        }
        public MockLeaveBalanceRepository MockCreate(){
            Setup( x => x.Create(It.IsAny<LeaveBalance>())).Returns<LeaveBalance>(leaveBalance => leaveBalance);
            return this;
        }
        public MockLeaveBalanceRepository MockUpdate(){
            Setup( x => x.Update(It.IsAny<LeaveBalance>())).Returns<LeaveBalance>(leaveBalance => leaveBalance);
            return this;     
        }
    }

}