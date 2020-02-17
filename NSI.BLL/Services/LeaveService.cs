using NSI.BLL.Services.Interfaces;
using NSI.DAL.Entities;
using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System;
using NSI.DataContracts.Models.Requests;
using Microsoft.Extensions.Configuration;

namespace NSI.BLL.Services {
    /// <summary>
    /// Service used by Availability Controller for API, manages data in DB via repositories and database context
    /// </summary>
    public class LeaveService : ILeaveService {

        private readonly ILeaveBalanceRepository leaveBalanceRepository;
        private readonly ILeaveTransactionRepository leaveTransactionRepository;
        private readonly IConfiguration configuration;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="leaveBalanceRepository"></param>
        /// <param name="leaveTransactionRepository"></param>
        /// <param name="configuration"></param>
        public LeaveService(ILeaveBalanceRepository leaveBalanceRepository, ILeaveTransactionRepository leaveTransactionRepository, IConfiguration configuration){
            this.leaveBalanceRepository = leaveBalanceRepository;
            this.leaveTransactionRepository = leaveTransactionRepository;
            this.configuration = configuration;
        }
        /// <summary>
        /// Get all Leave Balances in Database
        /// </summary>
        /// <returns></returns>
        public ICollection<LeaveBalance> GetAll(){
            return leaveBalanceRepository.GetAll();
        }
        /// <summary>
        /// Get all Transacations for given Leave Balance (provided ID)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<LeaveTransaction> GetAllTransactionsByLeaveBalanceId(long id){
            return leaveTransactionRepository.GetAllTransactionsByLeaveBalanceId(id);
        }
        /// <summary>
        /// Get leave balance by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LeaveBalance GetById(long id){
            return leaveBalanceRepository.GetById(id);
        }
        /// <summary>
        /// Get leave balance for given resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LeaveBalance GetByResourceId(long id){
            return leaveBalanceRepository.GetByResourceId(id);
        }
        /// <summary>
        /// Creates new balance or updates new balance if balance already exists for given resource
        /// </summary>
        /// <param name="leaveBalanceRequest"></param>
        /// <returns></returns>
        public LeaveBalance CreateOrUpdate(LeaveBalanceRequest leaveBalanceRequest){
            LeaveBalance existingLeaveBalance = leaveBalanceRepository.GetByResourceId(leaveBalanceRequest.ResourceID);
            if(existingLeaveBalance == null){
                // Handle Create
                long defaultAvailableDays = configuration.GetSection("LeaveBalance:Days").Get<long>(); 
                long defaultMonthlyGain = configuration.GetSection("LeaveBalance:MonthlyGain").Get<long>();
                long defulatYearlyGain  = configuration.GetSection("LeaveBalance:YearlyGain").Get<long>();
                
                leaveBalanceRequest.AvailableDays ??= defaultAvailableDays;
                leaveBalanceRequest.MonthlyGain ??= defaultMonthlyGain;
                leaveBalanceRequest.YearlyGain ??= defulatYearlyGain;

                LeaveBalance newLeaveBalance = new LeaveBalance{
                    ResourceID = leaveBalanceRequest.ResourceID,
                    AvailableDays = leaveBalanceRequest.AvailableDays.Value,
                    MonthlyGain = leaveBalanceRequest.MonthlyGain.Value,
                    YearlyGain = leaveBalanceRequest.YearlyGain.Value,
                };

                return leaveBalanceRepository.Create(newLeaveBalance);
            } else {
                // Handle Update
                if(leaveBalanceRequest.MonthlyGain.HasValue)
                     existingLeaveBalance.MonthlyGain = leaveBalanceRequest.MonthlyGain.Value;
                if(leaveBalanceRequest.YearlyGain.HasValue)
                    existingLeaveBalance.YearlyGain = leaveBalanceRequest.YearlyGain.Value;
                
                return leaveBalanceRepository.Update(existingLeaveBalance);

            }
        }
        /// <summary>
        /// Adds a change to leave balance in database (Transaction)
        /// </summary>
        /// <param name="leaveTransactionRequest"></param>
        /// <returns></returns>
        public LeaveBalance Change(LeaveTransactionRequest leaveTransactionRequest){
            LeaveBalance existingLeaveBalance = leaveBalanceRepository.GetById(leaveTransactionRequest.LeaveBalanceID);
            if(existingLeaveBalance == null)
                throw new ArgumentException("The Leave Balance with the specified id doesn't exist", "LeaveBalanceID");

            LeaveTransaction leaveTransaction = new LeaveTransaction { 
                Days = leaveTransactionRequest.Days,
                LeaveBalance = existingLeaveBalance,
                ApprovedByID = leaveTransactionRequest.ApprovedByID    
            };

            if(existingLeaveBalance.AvailableDays + leaveTransaction.Days < 0) {
                throw new ArgumentOutOfRangeException("Days", "You don't have that many days.");
            }
            leaveTransactionRepository.Create(leaveTransaction);
            existingLeaveBalance.AvailableDays += leaveTransactionRequest.Days;
            return leaveBalanceRepository.Update(existingLeaveBalance);

        }
    }
}