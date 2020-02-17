using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using NSI.Test.Mocks;
using NSI.Test.Mocks.Repositories;
using NSI.DAL.Entities;
using NSI.BLL.Services;
using NSI.DataContracts.Models.Requests;
using Microsoft.Extensions.Configuration;

namespace NSI.Test.ServiceTests {

    public class LeaveBalanceTests {
        
        [Theory]
        [InlineData(1, 1, 1, 1, 1)]
        [InlineData(2, 2, 0, 0, 0)]
        public void GetAllTest(long ID, long ResourceID, long AvailableDays, long MonthlyGain, long YearlyGain){

            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();
            
            leaveBalanceRepository.MockGetAll(ID, ResourceID, AvailableDays, MonthlyGain, YearlyGain);

            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);
        
            List<LeaveBalance> leaveBalances = leaveService.GetAll().ToList();

            Assert.Single(leaveBalances);
            Assert.Equal(ID, leaveBalances[0].ID);
            Assert.Equal(ResourceID, leaveBalances[0].ResourceID);
            Assert.Equal(AvailableDays, leaveBalances[0].AvailableDays);
            Assert.Equal(MonthlyGain, leaveBalances[0].MonthlyGain);
            Assert.Equal(YearlyGain, leaveBalances[0].YearlyGain);
        }
        [Theory]
        [InlineData(1, 1, 20, 1)]
        public void GetAllTransactionsByLeaveBalanceIdTest(long balanceID, long ID, long Days, long ApprovedByID){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();

            leaveTransactionRepository.MockGetAllTransactionsByLeaveBalanceId(balanceID, ID, Days, ApprovedByID);

            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);
            
            List<LeaveTransaction> leaveTransactions = leaveService.GetAllTransactionsByLeaveBalanceId(balanceID).ToList();

            Assert.Single(leaveTransactions);
            Assert.Equal(ID, leaveTransactions[0].ID);
            Assert.Equal(Days, leaveTransactions[0].Days);
            Assert.Equal(ApprovedByID, leaveTransactions[0].ApprovedByID);
            Assert.Equal(balanceID, leaveTransactions[0].LeaveBalance.ID);
        }
        [Theory]
        [InlineData(1, 1, 1, 1, 1)]
        [InlineData(2, 2, 0, 0, 0)]
        public void GetByIdTest(long ID, long ResourceID, long AvailableDays, long MonthlyGain, long YearlyGain){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();

            leaveBalanceRepository.MockGetById(ID, ResourceID, AvailableDays, MonthlyGain, YearlyGain);

            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);

            LeaveBalance leaveBalance = leaveService.GetById(ID);
        
            Assert.NotNull(leaveBalance);
            Assert.Equal(ID, leaveBalance.ID);
            Assert.Equal(ResourceID, leaveBalance.ResourceID);
            Assert.Equal(AvailableDays, leaveBalance.AvailableDays);
            Assert.Equal(MonthlyGain, leaveBalance.MonthlyGain);
            Assert.Equal(YearlyGain, leaveBalance.YearlyGain);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1)]
        [InlineData(2, 2, 0, 0, 0)]
        public void GetByResourceIdTest(long ID, long ResourceID, long AvailableDays, long MonthlyGain, long YearlyGain){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();

            leaveBalanceRepository.MockGetByResourceId(ID, ResourceID, AvailableDays, MonthlyGain, YearlyGain);

            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);

            LeaveBalance leaveBalance = leaveService.GetByResourceId(ResourceID);
        
            Assert.NotNull(leaveBalance);
            Assert.Equal(ID, leaveBalance.ID);
            Assert.Equal(ResourceID, leaveBalance.ResourceID);
            Assert.Equal(AvailableDays, leaveBalance.AvailableDays);
            Assert.Equal(MonthlyGain, leaveBalance.MonthlyGain);
            Assert.Equal(YearlyGain, leaveBalance.YearlyGain);
        }
        [Theory]
        [InlineData(1, 20, 20, 20)]
        [InlineData(2, 0, 0, 0)]
        public void CreateFromConfigurationTest(long ResourceID, long configDays, long configMonthlyGain, long configYearlyGain){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration
                                            .MockLeaveBalanceConfiguration(configDays, configMonthlyGain, configYearlyGain);

            leaveBalanceRepository.MockGetByResourceIdNull();
            leaveBalanceRepository.MockCreate();

            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);
            LeaveBalance leaveBalance =  leaveService.CreateOrUpdate(new LeaveBalanceRequest { ResourceID = ResourceID });
            
            Assert.NotNull(leaveBalance);
            Assert.Equal(ResourceID, leaveBalance.ResourceID);
            Assert.Equal(configDays, leaveBalance.AvailableDays);
            Assert.Equal(configMonthlyGain, leaveBalance.MonthlyGain);
            Assert.Equal(configYearlyGain, leaveBalance.YearlyGain);
        }

        [Theory]
        [InlineData(1, 1, 20, 20)]
        public void UpdateTest(long ID, long ResourceID, long updatedMonthyGain, long updatedYearlyGain){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();
            
            leaveBalanceRepository.MockGetByResourceId(ID, ResourceID, 0, 0, 0);
            leaveBalanceRepository.MockUpdate();

            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);
            LeaveBalanceRequest request = new LeaveBalanceRequest {
                ResourceID = ResourceID,
                MonthlyGain = updatedMonthyGain,
                YearlyGain = updatedYearlyGain
            };

            LeaveBalance leaveBalance =  leaveService.CreateOrUpdate(request);
            
            Assert.NotNull(leaveBalance);
            Assert.Equal(ID, leaveBalance.ID);
            Assert.Equal(ResourceID, leaveBalance.ResourceID);
            Assert.Equal(updatedMonthyGain, leaveBalance.MonthlyGain);
            Assert.Equal(updatedYearlyGain, leaveBalance.YearlyGain);
        
        }
        
        [Theory]
        [InlineData(1)]
        public void ChangeNullTest(int LeaveBalanceID){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();
            
            leaveBalanceRepository.MockGetByResourceIdNull();
            
            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);
            
            ArgumentException exception = Assert.Throws<ArgumentException>(() => {
                                                leaveService.Change(new LeaveTransactionRequest {
                                                    LeaveBalanceID = LeaveBalanceID
                                                });
                                            });
            Assert.Equal("LeaveBalanceID", exception.ParamName);
            Assert.Equal("The Leave Balance with the specified id doesn't exist (Parameter 'LeaveBalanceID')", exception.Message);
        }

        [Theory]
        [InlineData(1, 1, 20, -30)]
        [InlineData(1, 1, 1, -2)]
        public void ChangeNegativeDaysTest(long ID, long ResourceID, long AvailableDays, int reservedDays){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();
            
            leaveBalanceRepository.MockGetById(ID, ResourceID, AvailableDays, 0, 0);
            
            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);
            
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => {
                                                        leaveService.Change(new LeaveTransactionRequest {
                                                            LeaveBalanceID = ID,
                                                            Days = reservedDays,
                                                            ApprovedByID = ResourceID
                                                        });
                                                    });

            Assert.Equal("Days", exception.ParamName);
            Assert.Equal("You don't have that many days. (Parameter 'Days')", exception.Message);
        }
        [Theory]
        [InlineData(1, 20, 30, 50)]
        [InlineData(1, 10, 10, 20)]
        public void ChangeSuccessTest(long ID, long AvailableDays, long reservedDays, long result){
            MockLeaveBalanceRepository leaveBalanceRepository = new MockLeaveBalanceRepository();
            MockLeaveTransactionRepository leaveTransactionRepository = new MockLeaveTransactionRepository();
            IConfiguration configuration = MockConfiguration.MockEmpthy();
            
            leaveBalanceRepository.MockGetById(ID, 1, AvailableDays, 0, 0);
            leaveBalanceRepository.MockUpdate();
            leaveTransactionRepository.MockCreate();

            LeaveService leaveService = new LeaveService(leaveBalanceRepository.Object, leaveTransactionRepository.Object, configuration);
  
            LeaveBalance leaveBalance = leaveService.Change(new LeaveTransactionRequest {
                                            LeaveBalanceID = ID,
                                            Days = reservedDays
                                        });
            Assert.NotNull(leaveBalance);
            Assert.Equal(ID, leaveBalance.ID);
            Assert.Equal(result, leaveBalance.AvailableDays);
            

            
        }
    }    

}